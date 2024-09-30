using CipherData.Models;
using System.Data.SqlClient;
using System.Text.Json;
using System.Xml.Linq;

namespace CipherData
{
    public class CipherInfo : ICipherInfo
    {
        private readonly ISqlDataAcess _db;

        public CipherInfo(ISqlDataAcess db)
        {
            _db = db;
        }

        public Task<int> GetLastReportId()
        {
            string sql = "SELECT COALESCE(MAX(Id), 0) FROM Reports";

            // Assuming _db.LoadData returns the result as a List<int>, 
            // and you want to return the first (and only) element.
            return _db.LoadData<int, dynamic>(sql, new { })
                      .ContinueWith(task => task.Result.FirstOrDefault());
        }

        public Task InsertReport(Report new_report)
        {
            string sql = "INSERT INTO Reports (Id, Title, Creator, CreationDate, ObjectFactory, ObjectType, Path, Parameters) " +
                "VALUES (@Id, @Title, @Creator, @CreationDate, @ObjectFactory , @ObjectType, @Path, @Parameters)";

            var parameters = new
            {
                Id = new_report.Id,
                Title = new_report.Title,
                Creator = new_report.Creator,
                CreationDate = new_report.CreationDate,
                ObjectFactory = new_report.ObjectFactory.ToJson(),
                ObjectType = new_report.ObjectType.Name,
                Path = new_report.Path(),
                Parameters = JsonSerializer.Serialize(new_report.Parameters)
            };

            return _db.SaveData(sql, parameters);
        }


        public Task<bool> ExistsInDb(Report new_report)
        {
            string sql = "SELECT COUNT(1) FROM Reports WHERE Title = @Title";

            return _db.LoadData<int, dynamic>(sql, new { new_report.Title })
                      .ContinueWith(task => task.Result.FirstOrDefault() > 0);
        }
    }
}
