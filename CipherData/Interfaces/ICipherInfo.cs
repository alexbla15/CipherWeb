namespace CipherData.Interfaces
{
    public interface ICipherInfo
    {
        Task<int> GetLastReportId();

        Task<Report> GetReport(int id);

        Task<List<Report>> GetAllUpdatedReports();

        Task InsertReport(Report new_report);

        Task<bool> ExistsInDb(Report new_report, bool CheckTitle = true);

        Task AddToFavourites(int ReportId, string UserName);
    }
}