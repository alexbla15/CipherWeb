using System.Text.Json;

namespace CipherData.ApiMode
{
    public class CipherInfo : ICipherInfo
    {
        private readonly ISqlDataAcess _db;

        public CipherInfo(ISqlDataAcess db)
        {
            _db = db;
        }

        public async Task<Report> GetReport(int id)
        {
            string sql = "EXEC GetReport @Id=@SetId";

            // Assuming _db.LoadData returns the result as a List<int>, 
            // and you want to return the first (and only) element.
            var results = await _db.LoadData<dynamic, dynamic>(sql, new { SetId = id });

            if (results != null && results.Any())
            {
                var result = results.First();

                Report report = new()
                {
                    Id = result.Id,
                    Title = result.Title,
                    Creator = result.Creator,
                    CreationDate = result.CreationDate,
                    ObjectFactory = ICipherClass.FromJson<ObjectFactory>(result.ObjectFactory),
                    //ObjectType = new_report.ObjectType.Name,
                    Parameters = JsonSerializer.Deserialize<List<ReportParameter>>(result.Parameters),
                    Version = result.Version
                };

                return report;
            }

            return new Report();
        }

        public async Task<List<Report>> GetAllUpdatedReports()
        {
            List<Report> reports = new();

            string sql = "EXEC GetAllUpdatedReports";

            // Assuming _db.LoadData returns the result as a List<int>, 
            // and you want to return the first (and only) element.
            var results = await _db.LoadData<dynamic, dynamic>(sql, new { });

            if (results != null && results.Any())
            {
                foreach (var res in results)
                {
                    Report report = new()
                    {
                        Id = res.Id,
                        Title = res.Title,
                        Creator = res.Creator,
                        CreationDate = res.CreationDate,
                        ObjectFactory = ICipherClass.FromJson<ObjectFactory>(res.ObjectFactory),
                        //ObjectType = new_report.ObjectType.Name,
                        Parameters = JsonSerializer.Deserialize<List<ReportParameter>>(res.Parameters),
                        Version = res.Version
                    };
                    reports.Add(report);
                }
            }
            return reports;
        }

        public Task<int> GetLastReportId()
        {
            string sql = "EXEC GetLastReportId";

            // Assuming _db.LoadData returns the result as a List<int>, 
            // and you want to return the first (and only) element.
            return _db.LoadData<int, dynamic>(sql, new { })
                      .ContinueWith(task => task.Result.FirstOrDefault());
        }

        public Task InsertReport(Report new_report)
        {
            string sql = "INSERT INTO Reports (Id, Title, Creator, CreationDate, ObjectFactory, ObjectType, Path, Parameters, Version) " +
                "VALUES (@Id, @Title, @Creator, @CreationDate, @ObjectFactory , @ObjectType, @Path, @Parameters, @Version)";

            var parameters = new
            {
                new_report.Id,
                new_report.Title,
                new_report.Creator,
                new_report.CreationDate,
                ObjectFactory = new_report.ObjectFactory.ToJson(),
                ObjectType = new_report.ObjectType.Name,
                Path = new_report.Path(),
                Parameters = JsonSerializer.Serialize(new_report.Parameters),
                new_report.Version
            };

            return _db.SaveData(sql, parameters);
        }

        public Task<bool> ExistsInDb(Report new_report, bool CheckTitle = true)
        {
            string sql = "SELECT COUNT(1) FROM Reports WHERE Title = @Title";
            if (!CheckTitle) sql = "SELECT COUNT(1) FROM Reports WHERE Id = @Id";

            return _db.LoadData<int, dynamic>(sql, new { new_report.Title, new_report.Id })
                      .ContinueWith(task => task.Result.FirstOrDefault() > 0);
        }

        public Task AddToFavourites(int ReportId, string UserName)
        {
            string sql = "INSERT INTO ReportFavourites (ReportId, User) " +
                "VALUES (@ReportId, @User)";

            var parameters = new
            {
                ReportId,
                UserName
            };

            return _db.SaveData(sql, parameters);
        }
    }
}
