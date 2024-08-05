    using Model;
    using System.Data.SqlClient;

    namespace Dal
    {
        public class LoginDal
        {
            public void RegisterUser(LoginModel user)
            {
                using (SqlConnection conn = new SqlConnection(DBHelper.GetConnection()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Users (Username, Password) VALUES (@Username, @Password)", conn);
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.ExecuteNonQuery();
                }
            }

            public LoginModel? LoginUser(string username, string password)
            {
                using (SqlConnection conn = new SqlConnection(DBHelper.GetConnection()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Username = @Username AND Password = @Password", conn);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return new LoginModel
                        {
                            Id = (int)reader["Id"],
                            Username = (string)reader["Username"],
                            Password = (string)reader["Password"]
                        };
                    }
                    return null;
                }
            }
        }
    }
