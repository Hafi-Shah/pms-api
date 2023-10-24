using System.Data.SqlClient;
using System.Data;
namespace pms_api.Data
{
    public class DatabaseConnection
    {
        public static SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection(@"Server=.\SQLEXPRESS;Database=PMSDB;Trusted_Connection=True;");
            {
                connection.Open();
                if (connection.State == ConnectionState.Open){
                    return connection;
                } else{
                    return null;
                }
            }
        }
    }
}


