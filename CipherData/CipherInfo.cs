using CipherData.Models;
using CipherData;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.VisualBasic;

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

        public Task<List<Models.StorageSystem>> GetSystems()
        {
            string sql = "SELECT * FROM StorageSystem";

            return _db.LoadData<Models.StorageSystem, dynamic>(sql, new { });
        }

        public Task<List<Category>> GetSubCategories()
        {
            string sql = "SELECT * FROM SubCategory";

            return _db.LoadData<Category, dynamic>(sql, new { });
        }

        public Task<List<Vessel>> GetVessels()
        {
            string sql = "SELECT * FROM Vessels";

            return _db.LoadData<Vessel, dynamic>(sql, new { });
        }
    }
}
