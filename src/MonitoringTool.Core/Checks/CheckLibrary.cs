namespace MonitoringTool.Core.Checks;

/// <summary>Result of scanning check folders: the usable checks plus one message per file that could not be loaded.</summary>
public sealed record CheckLibrary(IReadOnlyList<CheckDefinition> Checks, IReadOnlyList<string> Errors)
{
    public static CheckLibrary Empty { get; } = new([], []);

    /// <summary>
    /// Loads every <c>*.ps1</c> from <paramref name="directories"/> in order. A later directory
    /// overrides an earlier one by file name, so a user can replace a built-in check by saving a
    /// file with the same name in their own folder. Files starting with '_' are ignored (templates).
    /// </summary>
    public static CheckLibrary Load(IEnumerable<string> directories)
    {
        ArgumentNullException.ThrowIfNull(directories);

        var byId = new Dictionary<string, CheckDefinition>(StringComparer.OrdinalIgnoreCase);
        var errors = new List<string>();

        foreach (var dir in directories.Where(Directory.Exists))
        {
            foreach (var file in Directory.EnumerateFiles(dir, "*.ps1").Order(StringComparer.OrdinalIgnoreCase))
            {
                var id = Path.GetFileNameWithoutExtension(file);
                if (id.StartsWith('_'))
                {
                    continue;
                }

                try
                {
                    byId[id] = CheckScriptParser.Parse(id, File.ReadAllText(file), file);
                }
                catch (Exception ex) when (ex is FormatException or IOException or UnauthorizedAccessException)
                {
                    errors.Add($"Check '{Path.GetFileName(file)}' was not loaded: {ex.Message}");
                }
            }
        }

        var checks = byId.Values.OrderBy(c => c.Name, StringComparer.CurrentCultureIgnoreCase).ToList();
        return new CheckLibrary(checks, errors);
    }
}
