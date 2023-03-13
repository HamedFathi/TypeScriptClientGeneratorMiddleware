using System.Text.RegularExpressions;

namespace TypeScriptClientGeneratorMiddleware;

public static class GeneratedSourceProcessor
{
    public static string ExtractInterfacesAndEnums(string source)
    {
        var list = new List<string>();
        var interfaces = new List<string>();
        var matches = Regex.Matches(source, @"export\s+(interface|enum)\s+(.+)[^\s](.*?\n)*?}");
        foreach (Match match in matches)
        {
            interfaces.Add(match.Groups[2].Value.Trim());
            list.Add(match.Value);
        }
        var result = string.Join("\n", list);
        foreach (var interfaceName in interfaces)
        {
            if (interfaceName.StartsWith("I"))
            {
                var className = interfaceName.Substring(1);
                result = result.Replace($" {className}", $" {interfaceName}");
            }
        }
        return result;
    }
}