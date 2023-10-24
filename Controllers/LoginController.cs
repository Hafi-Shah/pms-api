using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using pms_api.Data;
using pms_api.Models.Requests;
using pms_api.Models.Response;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace pms_api.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPost]
        public IActionResult Login(LoginReq loginReqModel)
        {
            LoginRes loginRes = new LoginRes();

            try
            {


                using (SqlConnection connection = DatabaseConnection.getConnection())
                {
                    using (SqlCommand command = new SqlCommand("PRC_LOGIN", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Email", loginReqModel.Email);
                        command.Parameters.AddWithValue("@Password", loginReqModel.Password);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                loginRes.UserId = Convert.ToInt32(reader["ID"]);
                                loginRes.Role = reader["USER_TYPE"].ToString();
                                loginRes.Token = GenerateToken(Convert.ToInt32(reader["ID"]));
                            }
                        }
                    }

                    if (loginRes != null && !string.IsNullOrWhiteSpace(loginRes.Token))
                    {
                        return Ok(loginRes);
                    }
                    else
                    {
                        return StatusCode(400, new { Message = "User not foud with email " + loginReqModel.Email });
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred in Login: {ex.Message}");
            }
        }

        private string GenerateToken(int userId)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    _configuration["JwtSettings:Issuer"],
                    _configuration["JwtSettings:Audience"],
                    null,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: signIn
                );
            string Token = new JwtSecurityTokenHandler().WriteToken(token);
            return Token;
        }
    }
}
