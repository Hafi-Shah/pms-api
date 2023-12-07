using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pms_api.Data;
using pms_api.Models.Response;
using System.Data;
using System.Data.SqlClient;


namespace pms_api.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserDetailsByRoleController : Controller
    {
        [HttpGet("GetDataByRole")]
        public dynamic GetDataByRole(int id, string role)
        {
            dynamic response = null;
            try
            {
                using (SqlConnection connection = DatabaseConnection.getConnection())
                {
                    using (SqlCommand command = new SqlCommand("GET_DATA_BY_ROLE", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", id);
                        command.Parameters.AddWithValue("@Role", role);

                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (reader.Read())
                            {
                                if (role == "company")
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

                                if (role == "user")
                                {
                                    response = new GetDataByRoleUserRes
                                    {
                                        UserId = reader.GetInt32(reader.GetOrdinal("COMPANY_ID")),
                                        FirstName = reader.GetString(reader.GetOrdinal("FIRS_NAME")),
                                        LastName = reader.GetString(reader.GetOrdinal("LAST_NAME")),
                                        Title = reader.GetString(reader.GetOrdinal("LAST_NAME")),
                                        Gender = reader.GetString(reader.GetOrdinal("GENDER")),
                                        ProfilePic = reader.GetString(reader.GetOrdinal("PROFILE_PIC")),
                                        CoverPic = reader.GetString(reader.GetOrdinal("COVER_PIC")),
                                        MartialStatus = reader.GetString(reader.GetOrdinal("MARTIAL_STATUS")),
                                        Dob = reader.GetString(reader.GetOrdinal("DOB")),
                                        Email = reader.GetString(reader.GetOrdinal("EMAIL")),
                                        TotalExperience = reader.GetString(reader.GetOrdinal("TOTAL_EXPERIENCE")),
                                        About = reader.GetString(reader.GetOrdinal("ABOUT")),
                                        CurrentCompany = reader.GetString(reader.GetOrdinal("CURRENT_COMPANY")),
                                        ContactNum = reader.GetString(reader.GetOrdinal("CONTACT_NO")),
                                        
                                    };
                                }

                            }
                            return Ok(response);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                               
                return StatusCode(500, "An error occurred in retriving data by role" + ex.Message);
            }
        }
    }
}
