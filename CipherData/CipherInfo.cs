using CipherData.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace CipherData
{
    public class CipherInfo : ICipherInfo
    {
        private readonly ISqlDataAcess _db;

        public CipherInfo(ISqlDataAcess db)
        {
            _db = db;
        }

        public Task<List<Event>> GetEvents()
        {
            string sql = "SELECT * FROM EventsView";

            return _db.LoadData<Event, dynamic>(sql, new { });
        }

        public Task<List<StorageSystem>> GetSystems()
        {
            string sql = "SELECT * FROM StorageSystem";

            return _db.LoadData<StorageSystem, dynamic>(sql, new { });
        }

        public Task<List<SubCategory>> GetSubCategories()
        {
            string sql = "SELECT * FROM SubCategory";

            return _db.LoadData<SubCategory, dynamic>(sql, new { });
        }

        public Task<List<Vessel>> GetVessels()
        {
            string sql = "SELECT * FROM Vessels";

            return _db.LoadData<Vessel, dynamic>(sql, new { });
        }

        //public Task<List<Movement>> GetTodayMovements()
        //{
        //    string formattedDate = DateTime.Today.ToString("dd/MM/yyyy");
        //    string sql = $"SELECT * FROM AllMovements WHERE Date LIKE '{formattedDate}'";
        //    return _db.LoadData<Movement, dynamic>(sql, new { });
        //}

        //public Task<List<int>> GetFirstYear()
        //{
        //    string sql = $"SELECT TOP 1 Date From AllMovements ORDER BY Date";
        //    return _db.LoadData<int, dynamic>(sql, new { });
        //}

        //public Task<List<SummaryModel>> GetLastSummaryRow()
        //{
        //    string sql = $"SELECT TOP 1 * FROM Summary ORDER BY Id DESC";
        //    return _db.LoadData<SummaryModel, object>(sql,  new { });
        //}

        //public async Task<List<string>> GetDistinctProperties(string property)
        //{
        //    string sql = "EXEC DistinctValues @column = @Property";
        //    return await _db.LoadData<string, dynamic>(sql, new { Property = property });
        //}

        //public async Task<List<int>> GetIds()
        //{
        //    // Load all existing Ids from the database
        //    string sql = "SELECT Id FROM Movements";
        //    return await _db.LoadData<int, dynamic>(sql, new { });
        //}

        //public async Task<int> GetNextId()
        //{
        //    List<int> existingIds = await GetIds();

        //    // Find the maximum Id
        //    int maxId = existingIds.Count > 0 ? existingIds.Max() : 0;

        //    // Increment maxId by 1 to get the new Id
        //    return maxId + 1;
        //}

        //public Task Remove(MovementModel mov)
        //{
        //    string sql = $"DELETE FROM dbo.Movements WHERE Id = {mov.Id}";

        //    return _db.SaveData(sql, new { });
        //}
    }
}
