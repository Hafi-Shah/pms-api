using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pms_api.Data;
using pms_api.Models.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace pms_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserDetailsByRoleController : Controller
    {
        [HttpGet("GetDataByRole")]
        public IActionResult GetDataByRole(int id, string role)
        {
            try
            {
                if (role == "company")
                {
                    GetDataByRoleCompanyRes response = null;
                    using (SqlConnection connection = DatabaseConnection.getConnection())
                    using (SqlCommand command = new SqlCommand("GET_DATA_BY_ROLE", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", id);
                        command.Parameters.AddWithValue("@Role", role);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                response = new GetDataByRoleCompanyRes
                                {
                                    UserId = reader.GetInt32(reader.GetOrdinal("COMPANY_ID")),
                                    CompanyName = reader.GetString(reader.GetOrdinal("COMPANY_NAME")),
                                    CompanyType = reader.GetString(reader.GetOrdinal("COMPANY_TYPE")),
                                    CompanyLocation = reader.GetString(reader.GetOrdinal("COMPANY_LOCATION")),
                                    CompanyDescription = reader.GetString(reader.GetOrdinal("COMPANY_DESCRIPTION")),
                                    CountryName = reader.GetString(reader.GetOrdinal("COUNTRY_NAME")),
                                    CompanyEmail = reader.GetString(reader.GetOrdinal("COMPANY_EMAIL")),
                                    ContactNum = reader.GetString(reader.GetOrdinal("CONTACT")),
                                    Password = reader.GetString(reader.GetOrdinal("COMPANY_PASSWORD")),
                                    ProfilePic = reader.GetString(reader.GetOrdinal("PROFILE_PIC"))
                                };
                            }
                        }
                    }
                    return Ok(response);
                }
                else if (role == "user")
                {
                    List<GetDataByRoleUserRes> userResponses = new List<GetDataByRoleUserRes>();
                    using (SqlConnection connection = DatabaseConnection.getConnection())
                    using (SqlCommand command = new SqlCommand("GET_DATA_BY_ROLE", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", id);
                        command.Parameters.AddWithValue("@Role", role);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int userId = reader.GetInt32(reader.GetOrdinal("USER_ID"));
                                GetDataByRoleUserRes existingUserResponse = userResponses.FirstOrDefault(u => u.UserId == userId);

                                if (existingUserResponse == null)
                                {
                                    existingUserResponse = new GetDataByRoleUserRes
                                    {
                                        UserId = userId,
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
                                    };
                                    userResponses.Add(existingUserResponse);
                                }

                                // Assuming there is a column named "SKILL" for each skill record
                                string skill = reader["SKILL"].ToString();
                                if (!string.IsNullOrEmpty(skill))
                                {
                                    existingUserResponse.UserSkills.Add(skill);
                                }
                            }
                        }
                    }
                    return Ok(userResponses);
                }
                else
                {
                    return BadRequest("Invalid role specified.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred in retrieving data by role: " + ex.Message);
            }
        }
    }
}
