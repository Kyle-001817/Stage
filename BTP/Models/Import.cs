using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Globalization;
using System.Text;

namespace BTP.Models
{
    public class Import
    {
        public void ImportCsvToDatabase<T>(K_Context _context, string table, IFormFile file, Func<CsvReader, T> mapFunc, CsvConfiguration? csvConfig = null) where T : class
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Le fichier est vide ou n'existe pas.");
            }

            if (csvConfig == null)
            {
                csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";",
                    BadDataFound = null,
                    MissingFieldFound = null
                };
            }

            using (var reader = new StreamReader(file.OpenReadStream(), Encoding.UTF8))
            using (var csv = new CsvReader(reader, csvConfig))
            {
                csv.Read();
                csv.ReadHeader();

                var entities = new List<T>();

                while (csv.Read())
                {
                    var entity = mapFunc(csv);
                    entities.Add(entity);
                }

                // Bulk insert using raw SQL
                foreach (var entity in entities)
                {
                    var sql = GetInsertSql<T>(table);
                    var parameters = GetSqlParameters<T>(entity);
                    _context.Database.ExecuteSqlRaw(sql, parameters);
                }
            }
        }

        public string GetInsertSql<T>(string table)
        {
            var tableName = table;
            var properties = typeof(T).GetProperties();
            var valueParams = string.Join(", ", properties.Select(p => $"@{p.Name.ToLower()}"));
            return $"INSERT INTO {tableName} VALUES ({valueParams})";
        }

        private NpgsqlParameter[] GetSqlParameters<T>(T entity)
        {
            var properties = typeof(T).GetProperties();
            var parameters = new List<NpgsqlParameter>();
            foreach (var property in properties)
            {
                parameters.Add(new NpgsqlParameter($"@{property.Name.ToLower()}", property.GetValue(entity) ?? DBNull.Value));
            }

            return parameters.ToArray();
        }
    }
}
