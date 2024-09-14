using crud_operation.Iservice;
using crud_operation.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Data;

namespace crud_operation.Service
{
    public class AccountService : IAccountService
    {
        private readonly string _connectionString;
        private readonly ILogger<AccountService> _logger;


        public AccountService(IConfiguration configuration, ILogger<AccountService> logger)

        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
        }





        public List<acountsInfo> SearchAccountNameById(int AccountId)
        {
            List<acountsInfo> employees = new List<acountsInfo>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "dbo.GetAccountInfo";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@AccountId", AccountId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            acountsInfo employee = new acountsInfo
                            {
                                AccountId = reader.GetInt32(reader.GetOrdinal("AccountId")),
                                Name = reader.GetString(reader.GetOrdinal("AccountName")),
                            };

                            employees.Add(employee);
                        }
                    }
                }
            }
            return employees;
        }










    }
}
