using Microsoft.AspNetCore.Mvc;
using pms_api.Data;
using pms_api.Models.Requests;
using pms_api.Models.Response;
using System;
using System.Data;
using System.Data.SqlClient;

namespace pms_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRegistrationController : ControllerBase
    {
        [HttpPost("RegisterUser")]
        public IActionResult RegisterUser(RegUserReq request)
        {
            try
            {
                int userId = 0;
                RegUserReq response = new RegUserReq();


                using (SqlConnection connection = DatabaseConnection.getConnection())
                {
                    using (SqlCommand command = new SqlCommand("PRC_REGISTER_USER", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@FirstName", request.FirstName);
                        command.Parameters.AddWithValue("@LastName", request.LastName);
                        command.Parameters.AddWithValue("@Email", request.Email);
                        command.Parameters.AddWithValue("@Password", request.Password);
                        command.Parameters.AddWithValue("@Contact", request.Contact);
                        command.Parameters.AddWithValue("@Dob", request.Dob);
                        command.Parameters.AddWithValue("@UserTypeId", request.UserTypeId);
                        command.Parameters.AddWithValue("@YearlyExp", request.YearlyExp);
                        command.Parameters.AddWithValue("@GenderId", request.GenderId);
                        command.Parameters.AddWithValue("@City", request.City);
                        command.Parameters.AddWithValue("@About", request.About);
                        command.Parameters.AddWithValue("@CoverImg", request.CoverImg);
                        command.Parameters.AddWithValue("@ProfilePic", request.ProfilePic);
                        command.Parameters.AddWithValue("@MaritalStatusId", request.maritalStatusId);
                      
                        // Add output parameter for the last inserted ID
                        SqlParameter outputParameter = new SqlParameter("@LastUserID", SqlDbType.Int);
                        outputParameter.Direction = ParameterDirection.Output;
                        command.Parameters.Add(outputParameter);
                        command.ExecuteNonQuery();
                        // Retrieve the value of the output parameter
                        userId = Convert.ToInt32(outputParameter.Value);
                       

                    }
                    foreach (var item in request.Skills)
                    {
                        using (SqlCommand command = new SqlCommand("PRC_ADD_USER_SKILL", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@UserId", userId);
                            command.Parameters.AddWithValue("@Role", request.Role);
                            command.Parameters.AddWithValue("@SkillId", item);

                            command.ExecuteNonQuery();
                        }
                    }
                }

                return Ok(new { success = true, message = "User Registered Successfully" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred in RegisterUser." + ex.Message);
            }
        }

            [HttpGet("GetUserTypes")]
            public IActionResult GetUserTypes()
            {
                try
                {
                    List<GetStatusTypes> userTypes = new List<GetStatusTypes>();

                    using (SqlConnection connection = DatabaseConnection.getConnection())
                    {
                        if (connection != null)
                        {
                            using (SqlCommand command = new SqlCommand("PRC_GET_USER_TYPES", connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        GetStatusTypes userType = new GetStatusTypes
                                        {
                                            Id = Convert.ToInt32(reader["Id"]),
                                            Name = reader["Name"].ToString()
                                        };
                                        userTypes.Add(userType);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.Error.WriteLine("Create Db Connection");
                        }
                    }

                    return Ok(userTypes);
                }
                catch (Exception ex)
                {             
                    return StatusCode(500, "An error occurred while retrieving user types." + ex.Message);
                }
            }

        [HttpGet("GetUserGender")]
        public IActionResult GetGenderType()
        {
            try
            {
                List<GetGenderTypes> userTypes = new List<GetGenderTypes>();

                using (SqlConnection connection = DatabaseConnection.getConnection())
                {
                    if (connection != null)
                    {
                        using (SqlCommand command = new SqlCommand("PRC_GET_GENDER_TYPES", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    GetGenderTypes genderType = new GetGenderTypes
                                    {
                                        Id = Convert.ToInt32(reader["Id"]),
                                        Name = reader["Name"].ToString()
                                    };
                                    userTypes.Add(genderType);
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.Error.WriteLine("Create Db Connection");
                    }
                }

                return Ok(userTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving gender types." + ex.Message);
            }
        }

        [HttpGet("GetUserStatus")]
        public IActionResult GetUserStatus()
        {
            try
            {
                List<GetStatusTypes> userTypes = new List<GetStatusTypes>();

                using (SqlConnection connection = DatabaseConnection.getConnection())
                {
                    if (connection != null)
                    {
                        using (SqlCommand command = new SqlCommand("PRC_GET_MARITAL_TYPES", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    GetStatusTypes statusType = new GetStatusTypes
                                    {
                                        Id = Convert.ToInt32(reader["Id"]),
                                        Name = reader["Name"].ToString()
                                    };
                                    userTypes.Add(statusType);
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.Error.WriteLine("Create Db Connection");
                    }
                }

                return Ok(userTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving status types." + ex.Message);
            }
        }

    }
}
