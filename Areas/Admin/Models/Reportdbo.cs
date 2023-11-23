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
                SqlCommand cmd = new SqlCommand("SELECT [FirstName],[LastName],[Date of Birth],[Gender],CASE WHEN [FirstName] IS NULL AND [LastName] IS NULL THEN NULL ELSE [Promotions] END AS 'Promotions' FROM ( SELECT [FirstName],[LastName],FORMAT(CAST([DOB] AS date), 'yyyy-MM-dd') AS 'Date of Birth',[Gender],[RecievePromotions] AS 'Promotions' FROM [Profiles] WHERE FirstName IS NOT NULL AND LastName IS NOT NULL UNION ALL SELECT NULL AS 'FirstName', NULL AS 'LastName', NULL AS 'Date of Birth', 'Total Count' AS 'Gender', CAST(COUNT(*) AS VARCHAR) AS 'Promotions' FROM [Profiles]) AS SubQuery", connection);
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
                SqlCommand cmd = new SqlCommand("SELECT FirstName,LastName FROM Profiles WHERE FirstName IS NOT NULL AND LastName IS NOT NULL UNION ALL SELECT 'Total Count' AS 'FirstName',CAST(COUNT(*) AS VARCHAR) AS 'LastName' FROM Profiles", connection);
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
                SqlCommand cmd = new SqlCommand("SELECT G.Title AS 'Title',G.Description,G.Price AS 'Price($)',Gen.GenreName AS 'Genre',G.ReleaseYear AS 'Year',G.Publisher,Plat.Name AS 'Platform' FROM [dbo].[Games] G INNER JOIN [dbo].[Genres] Gen ON G.GenreID = Gen.GenreID INNER JOIN [dbo].[Platforms] Plat ON G.PlatformID = Plat.PlatformID UNION ALL SELECT NULL AS 'Title', NULL AS 'Description',NULL AS 'Price', NULL AS 'Genre', NULL AS 'Year', 'Total Count' AS 'Publisher', CAST(COUNT(*) AS VARCHAR) AS 'Platform' FROM [dbo].[Games]", connection);
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
                SqlCommand cmd = new SqlCommand("SELECT [Title] FROM [Games].[dbo].[Games] UNION ALL SELECT '**--Total Count of CVGS Games is ' + CAST(COUNT(*) AS VARCHAR) AS 'Title' FROM [Games].[dbo].[Games]", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
    }
}
