using System.Text;

namespace DataCleaner;

internal class Program
{
    private static Repository _repository;
    internal static readonly string[] separator = ["\r\n", "\r", "\n"];

    private static async Task Main(string[] args)
    {
        _repository = new Repository();

        int count = 0;

        var discharges = await _repository.GetAll();

        foreach (var discharge in discharges)
        {
            var note = await _repository.GetNote(discharge.SubjectId, discharge.AdmissionId);

            if (note == null)
                continue;

            count++;

            await ProcessNote(note);

            if (count % 100 == 0)
                Console.WriteLine($"Processed {count} ...");
        }
    }

    private static async Task ProcessNote(DischargeNote note)
    {
        var lines = note.Text.Split(separator, StringSplitOptions.RemoveEmptyEntries);

        var sb = new StringBuilder();

        string lastCategory = null;

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var category = GetCategory(line, lastCategory);

            if (!string.Equals(category, lastCategory)) // category change
            {
                lastCategory = category;
                continue;
            }

            if (category != null && line.StartsWith(category, StringComparison.InvariantCultureIgnoreCase))
                continue;

            if (category != null)
            {
                var clean = CleanLine(line);

                if (clean != null)
                    sb.Append(' ').Append(clean);
            }
        }

        await _repository.Add(new DischargeNoteLine
        {
            SubjectId = note.SubjectId,
            AdmissionId = note.AdmissionId,
            Category = "---",
            Text = sb.ToString().Trim(),
            IcdCode = note.IcdCode
        });
    }

    private static string GetCategory(string line, string lastCategory)
    {
        line = line?.Trim() ?? string.Empty;

        if (line.EndsWith(':'))
        {
            if (line.EndsWith("History:", StringComparison.InvariantCultureIgnoreCase) ||
                line.EndsWith("Diagnoses:", StringComparison.InvariantCultureIgnoreCase) ||
                line.EndsWith("Diagnosis:", StringComparison.InvariantCultureIgnoreCase))
                return line[..^1].ToUpper();

            return null;
        }

        return lastCategory;
    }

    private static string CleanLine(string line)
    {
        line = line.Trim();

        if (string.Equals(line, "___"))
            return null;

        if (string.Equals(line, "Secondary Diagnosis", StringComparison.InvariantCultureIgnoreCase))
            return null;

        if (string.Equals(line, "Secondary Diagnoses", StringComparison.InvariantCultureIgnoreCase))
            return null;

        if (string.Equals(line, "Primary Diagnosis", StringComparison.InvariantCultureIgnoreCase))
            return null;

        if (string.Equals(line, "Primary Diagnoses", StringComparison.InvariantCultureIgnoreCase))
            return null;

        if (line[0] == '=')
        {
            for (int i = 1; i < line.Length; i++)
            {
                if (line[i] != '=')
                    break;

                return null;
            }
        }

        if (line.StartsWith("-"))
        {
            if (line.Length == 2)
                return null;

            return line.Substring(1).Trim();
        }

        return line;
    }
}