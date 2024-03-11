using Dapper;
using Npgsql;

namespace DataCleaner;

public class Repository
{
    private readonly NpgsqlConnection connection;
    private readonly string CONNECTION_STRING = "Host=localhost;Port=5432;Database=mimic;User ID=postgres;Password=postgres;";

    public Repository()
    {
        connection = new NpgsqlConnection(CONNECTION_STRING);
        connection.Open();
    }

    public async Task<DischargeNote> GetNote(int subjectId, int admissionId)
    {
        string commandText = $"select subject_id as SubjectId, hadm_id AS AdmissionId, text, icd_code AS IcdCode from capstone.discharge_note where subject_id={subjectId} and hadm_id={admissionId};";
      
        var notes = await connection.QueryAsync<DischargeNote>(commandText);

        return (notes?.Any() ?? false) ? notes.ElementAt(0) : null;
    }

    public async Task<IEnumerable<DischargeNote>> GetAll()
    {
        string commandText = $"select subject_id as SubjectId, hadm_id AS AdmissionId, icd_code AS IcdCode from capstone.discharge_note;";

        var notes = await connection.QueryAsync<DischargeNote>(commandText);

        return notes;
    }

    public async Task Add(DischargeNoteLine line)
    {
        if (string.IsNullOrWhiteSpace(line.Text))
            return;

        string sql = $"INSERT INTO capstone.discharge_note_line (subject_id, hadm_id, category, text, icd_code) VALUES (@SubjectId, @AdmissionId, @Category, @Text, @IcdCode)";

        var parameters = new
        {
            line.SubjectId,
            line.AdmissionId,
            line.Category,
            line.Text,
            line.IcdCode
        };

        await connection.ExecuteAsync(sql, parameters);
    }
}

public class DischargeNote
{
    public int SubjectId { get; set; }
    public int AdmissionId { get; set; }
    public string Text { get; set; }
    public string IcdCode { get; set; }
}

public class DischargeNoteLine
{
    public int LineId { get; set; }
    public int SubjectId { get; set; }
    public int AdmissionId { get; set; }
    public string Category { get; set; }
    public string Text { get; set; }
    public string IcdCode { get; set; }
}