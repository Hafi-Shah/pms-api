using Microsoft.AspNetCore.Mvc;
using pms_api.Data;
using pms_api.Models.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace pms_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewUserController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                GetDataByRoleUserRes userResponse = null; // Change to a single object

                using (SqlConnection connection = DatabaseConnection.getConnection())
                using (SqlCommand command = new SqlCommand("PRC_VIEW_USER", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (userResponse == null)
                            {
                                userResponse = new GetDataByRoleUserRes
                                {
                                    UserId = reader.GetInt32(reader.GetOrdinal("USER_ID")),
                                    FirstName = reader.GetString(reader.GetOrdinal("FIRST_NAME")),
                                    LastName = reader.GetString(reader.GetOrdinal("LAST_NAME")),
                                    Dob = reader.GetDateTime(reader.GetOrdinal("DOB")),
                                    Email = reader.GetString(reader.GetOrdinal("EMAIL")),
                                    ContactNum = reader.GetString(reader.GetOrdinal("CONTACT_NO")),
                                    TotalExperience = (int)reader.GetDecimal(reader.GetOrdinal("TOTAL_EXPERIENCE")),
                                    About = reader.GetString(reader.GetOrdinal("ABOUT")),
                                    ProfilePic = reader.GetString(reader.GetOrdinal("PROFILE_PIC")),
                                    CoverPic = reader.GetString(reader.GetOrdinal("COVER_PIC")),
                                    City = reader.GetString(reader.GetOrdinal("CITY")),
                                    Gender = reader.GetString(reader.GetOrdinal("GENDER")),
                                    MartialStatus = reader.GetString(reader.GetOrdinal("MARITAL_STATUS")),
                                    UserType = reader.GetString(reader.GetOrdinal("USER_TYPE")),
                                    UserSkills = new List<string>(),
                                    Password = reader.GetString(reader.GetOrdinal("USER_PASSWORD"))
                                };
                            }

                            // Assuming there is a column named "SKILL" for each skill record
                            string skill = reader["SKILL"].ToString();
                            if (!string.IsNullOrEmpty(skill))
                            {
                                userResponse.UserSkills.Add(skill);
                            }
                        }
                    }
                }

                return Ok(userResponse); // Return a single object
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while processing your request. {ex.Message}");
            }
        }
    }
}
