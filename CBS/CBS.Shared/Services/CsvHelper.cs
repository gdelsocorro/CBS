namespace CBS.Shared.Services;

public static class CsvHelper
{
    public static string Build(IEnumerable<string> headers, IEnumerable<IEnumerable<string>> rows)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine(string.Join(",", headers.Select(Esc)));
        foreach (var row in rows)
            sb.AppendLine(string.Join(",", row.Select(Esc)));
        return sb.ToString().TrimEnd();
    }

    public static List<Dictionary<string, string>> Parse(string content)
    {
        var result = new List<Dictionary<string, string>>();
        var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        if (lines.Length < 2) return result;
        var headers = SplitLine(lines[0]);
        for (int i = 1; i < lines.Length; i++)
        {
            var vals = SplitLine(lines[i]);
            if (vals.Count == 0) continue;
            var row = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            for (int j = 0; j < headers.Count && j < vals.Count; j++)
                row[headers[j].Trim()] = vals[j].Trim();
            result.Add(row);
        }
        return result;
    }

    private static string Esc(string v)
    {
        if (v.Contains(',') || v.Contains('"') || v.Contains('\n'))
            return $"\"{v.Replace("\"", "\"\"")}\"";
        return v;
    }

    private static List<string> SplitLine(string line)
    {
        var fields = new List<string>();
        var sb = new System.Text.StringBuilder();
        bool inQ = false;
        line = line.TrimEnd('\r');
        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (inQ)
            {
                if (c == '"' && i + 1 < line.Length && line[i + 1] == '"') { sb.Append('"'); i++; }
                else if (c == '"') inQ = false;
                else sb.Append(c);
            }
            else
            {
                if (c == '"') inQ = true;
                else if (c == ',') { fields.Add(sb.ToString()); sb.Clear(); }
                else sb.Append(c);
            }
        }
        fields.Add(sb.ToString());
        return fields;
    }
}
