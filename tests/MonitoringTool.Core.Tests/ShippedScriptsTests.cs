using System.Management.Automation.Language;
using MonitoringTool.Core.Checks;

namespace MonitoringTool.Core.Tests;

/// <summary>
/// Guards the PowerShell that ships with the app. The scripts only run on Windows servers, so without
/// this a syntax error would only surface at runtime on someone's server.
/// </summary>
public sealed class ShippedScriptsTests
{
    private static readonly string AppDirectory = Path.Combine(FindRepositoryRoot(), "src", "MonitoringTool.App");

    public static TheoryData<string> AllScripts()
    {
        var data = new TheoryData<string>();
        foreach (var file in Directory.EnumerateFiles(Path.Combine(AppDirectory, "Checks"), "*.ps1")
                     .Concat(Directory.EnumerateFiles(Path.Combine(AppDirectory, "Scripts"), "*.ps1"))
                     .Order(StringComparer.Ordinal))
        {
            data.Add(Path.GetRelativePath(AppDirectory, file));
        }

        return data;
    }

    [Theory]
    [MemberData(nameof(AllScripts))]
    public void Script_has_no_syntax_errors(string relativePath)
    {
        Parser.ParseFile(Path.Combine(AppDirectory, relativePath), out _, out var errors);

        Assert.True(errors.Length == 0, string.Join(Environment.NewLine, errors.Select(e => $"{e.Extent.StartLineNumber}: {e.Message}")));
    }

    [Fact]
    public void Built_in_checks_load_with_metadata()
    {
        var library = CheckLibrary.Load([Path.Combine(AppDirectory, "Checks")]);

        Assert.Empty(library.Errors);
        Assert.Equal(
            ["expiring-certificates", "failed-scheduled-tasks", "pending-reboot", "recent-system-errors", "stopped-auto-services"],
            library.Checks.Select(c => c.Id).Order(StringComparer.Ordinal));
        Assert.All(library.Checks, c =>
        {
            Assert.NotEqual(c.Id, c.Name);
            Assert.NotEmpty(c.Description);
        });
    }

    private static string FindRepositoryRoot()
    {
        for (var dir = new DirectoryInfo(AppContext.BaseDirectory); dir is not null; dir = dir.Parent)
        {
            if (File.Exists(Path.Combine(dir.FullName, "MonitoringTool.slnx")))
            {
                return dir.FullName;
            }
        }

        throw new InvalidOperationException("Repository root (MonitoringTool.slnx) not found.");
    }
}
