using Model;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Dal
{
    public class LoginDal
    {
        public void RegisterUser(LoginModel user)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnection()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Users (Username, Password) VALUES (@Username, @Password)", conn))
                {
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public LoginModel? LoginUser(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnection()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Username = @Username AND Password = @Password", conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
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

        public async Task<bool> LogoutUser(string username)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DBHelper.GetConnection()))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Users WHERE Username = @Username", conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        int rowsAffected = await cmd.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException)
            {
                // Log exception
                return false;
            }
        }
    }
}
