using System.Data.SqlClient;

namespace Dal
{
    public static class DBHelper
    {
        public static string GetConnection()
        {
            return "Data Source=.;Initial Catalog=signup;Integrated Security=True";

        }
    }
}



