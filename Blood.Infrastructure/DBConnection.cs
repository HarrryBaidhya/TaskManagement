using System.Configuration;
using System.Data.SqlClient;

namespace Blood.Infrastructure
{
    public class DBConnection
    {
        public static SqlConnection Connection()
        {
            SqlConnection con = new SqlConnection();

            SqlConnection.ClearAllPools();
            con = new SqlConnection();
            try
            {
                con = new SqlConnection(Convert.ToString(ConfigurationManager.ConnectionStrings["ProductContextConnection"].ConnectionString));

                if (con.State.ToString() == "Open")
                {
                    con.Close();
                }
                if (con.State.ToString() == "Closed")
                {
                    con.Open();
                }
                return con;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}