using MonitoringTool.Core.Checks;

namespace MonitoringTool.Core.Tests;

public sealed class CheckParsingTests : IDisposable
{
    private readonly string directory = Directory.CreateTempSubdirectory("mt-checks-").FullName;

    public void Dispose() => Directory.Delete(this.directory, recursive: true);

    [Fact]
    public void Metadata_is_read_from_header_comments()
    {
        const string script = """
            # @name: Disk queue
            # @description: Average disk queue length.
            # @interval: 120
            # @timeout: 20
            Get-Counter '\PhysicalDisk(_Total)\Avg. Disk Queue Length'
            """;

        var check = CheckScriptParser.Parse("disk-queue", script, "x.ps1");

        Assert.Equal("Disk queue", check.Name);
        Assert.Equal("Average disk queue length.", check.Description);
        Assert.Equal(120, check.IntervalSeconds);
        Assert.Equal(20, check.TimeoutSeconds);
        Assert.Equal(script, check.Script);
    }

    [Fact]
    public void Missing_metadata_falls_back_to_defaults_and_out_of_range_is_clamped()
    {
        var plain = CheckScriptParser.Parse("my-check", "$true", "x.ps1");
        Assert.Equal("my-check", plain.Name);
        Assert.Equal(CheckScriptParser.DefaultIntervalSeconds, plain.IntervalSeconds);
        Assert.Equal(CheckScriptParser.DefaultTimeoutSeconds, plain.TimeoutSeconds);

        var extreme = CheckScriptParser.Parse("x", "# @interval: 1\n# @timeout: 99999\n$true", "x.ps1");
        Assert.Equal(CheckScriptParser.MinIntervalSeconds, extreme.IntervalSeconds);
        Assert.Equal(CheckScriptParser.MaxTimeoutSeconds, extreme.TimeoutSeconds);
    }

    [Theory]
    [InlineData("# @interval: five minutes\n$true")]
    [InlineData("   \n  ")]
    public void Invalid_scripts_are_rejected(string script) =>
        Assert.Throws<FormatException>(() => CheckScriptParser.Parse("x", script, "x.ps1"));

    [Fact]
    public void Library_loads_folders_in_order_later_overrides_earlier_and_skips_templates()
    {
        var builtIn = Directory.CreateDirectory(Path.Combine(this.directory, "builtin")).FullName;
        var user = Directory.CreateDirectory(Path.Combine(this.directory, "user")).FullName;
        File.WriteAllText(Path.Combine(builtIn, "pending-reboot.ps1"), "# @name: Built-in reboot\n$true");
        File.WriteAllText(Path.Combine(builtIn, "services.ps1"), "# @name: Services\n$true");
        File.WriteAllText(Path.Combine(builtIn, "_template.ps1"), "# @name: Template\n$true");
        File.WriteAllText(Path.Combine(user, "pending-reboot.ps1"), "# @name: My reboot\n$true");
        File.WriteAllText(Path.Combine(user, "broken.ps1"), "# @timeout: soon\n$true");

        var library = CheckLibrary.Load([builtIn, user, Path.Combine(this.directory, "missing")]);

        Assert.Equal(["My reboot", "Services"], library.Checks.Select(c => c.Name));
        var error = Assert.Single(library.Errors);
        Assert.Contains("broken.ps1", error, StringComparison.Ordinal);
    }

    [Fact]
    public void Worst_status_wins_and_problem_messages_are_combined()
    {
        var (status, message, value) = CheckOutputInterpreter.Interpret(
        [
            new CheckOutputRecord("Ok", "C: fine", 1),
            new CheckOutputRecord("Warning", "cert A expires in 20 days", 20),
            new CheckOutputRecord("Critical", "cert B expired", -3),
            new CheckOutputRecord("critical", "cert C expired", -1),
        ], []);

        Assert.Equal(CheckStatus.Critical, status);
        Assert.Equal("2 issues: cert B expired; cert C expired", message);
        Assert.Equal(-3, value);
    }

    [Theory]
    [InlineData(null, CheckStatus.Ok)]
    [InlineData("OK", CheckStatus.Ok)]
    [InlineData("True", CheckStatus.Ok)]
    [InlineData("warn", CheckStatus.Warning)]
    [InlineData("Failed", CheckStatus.Critical)]
    [InlineData("False", CheckStatus.Critical)]
    [InlineData("banana", CheckStatus.Unknown)]
    public void Status_text_is_parsed_leniently(string? raw, CheckStatus expected) =>
        Assert.Equal(expected, CheckOutputInterpreter.ParseStatus(raw));

    [Fact]
    public void No_output_is_ok_but_errors_without_output_are_unknown()
    {
        Assert.Equal(CheckStatus.Ok, CheckOutputInterpreter.Interpret([], []).Status);

        var (status, message, _) = CheckOutputInterpreter.Interpret([], ["Access is denied"]);
        Assert.Equal(CheckStatus.Unknown, status);
        Assert.Contains("Access is denied", message, StringComparison.Ordinal);
    }

    [Fact]
    public void Plain_strings_are_informational()
    {
        var (status, message, _) = CheckOutputInterpreter.Interpret([new CheckOutputRecord(null, "42 users connected", null)], []);
        Assert.Equal(CheckStatus.Ok, status);
        Assert.Equal("42 users connected", message);
    }
}
