using Microsoft.Data.SqlClient;
using System.Data;

namespace PROG3050_HMJJ.Areas.Admin.Models
{
    public class Reportdbo
    {
        private readonly IConfiguration _configuration;

        public Reportdbo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection GetConnection()
        {
            string connectionString = _configuration.GetConnectionString("GameStoreCNN");
            return new SqlConnection(connectionString);
        }

        public SqlConnection GetConnection2()
        {
            string connectionString = "Server = localhost; Database = Games; Trusted_Connection = True; MultipleActiveResultSets = true; TrustServerCertificate = True";
            return new SqlConnection(connectionString);
        }

        public DataTable GetMemberDetailsrecord()
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT  ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS Sr_No,[FirstName],[LastName],[Gender],format(cast([DOB] as date), 'yyyy-MM-dd') As 'Date of Birth',[RecievePromotions] As 'Promotions' FROM [Profiles]" +
                "WHERE FirstName IS NOT NULL AND LastName IS NOT NULL AND Gender IS NOT NULL AND DOB IS NOT NULL", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt; 
            }
        }

        public DataTable GetMemberListsrecord()
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS Sr_No,FirstName,LastName " +
                "FROM Profiles " +
                "WHERE FirstName IS NOT NULL AND LastName IS NOT NULL", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
        public DataTable GetGamesDeatilsrecord()
        {
            using (SqlConnection connection = GetConnection2())
            {
                SqlCommand cmd = new SqlCommand("SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS Sr_No, [Title],[Description],[Price],[Publisher],[ReleaseYear] As 'Year',[GameGenre] As 'Genre',[GamePlatform] As 'Platform' FROM [Games].[dbo].[GameDataTransferObjects]", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataTable GetGamesListsrecord()
        {
            using (SqlConnection connection = GetConnection2())
            {
                SqlCommand cmd = new SqlCommand("SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS Sr_No,[Title] FROM [Games].[dbo].[Games]", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
    }
}
