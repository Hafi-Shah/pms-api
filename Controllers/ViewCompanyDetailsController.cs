using Microsoft.AspNetCore.Mvc;
using pms_api.Data;
using pms_api.Models.Response;
using System.Data;
using System.Data.SqlClient;


namespace pms_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ViewCompanyDetailsController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                using (SqlConnection connection = DatabaseConnection.getConnection())
                {

                    using (SqlCommand command = new SqlCommand("PRC_VIEW_COMPANY", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@CompanyID", id);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.SingleRow))
                        {
                            if (reader.Read())
                            {
                                var companyDetail = new GetCompanyDetailsRes
                                {
                                    CompanyId = reader.GetInt32(reader.GetOrdinal("COMPANY_ID")),
                                    CompanyName = reader.GetString(reader.GetOrdinal("COMPANY_NAME")),
                                    CompanyType = reader.GetString(reader.GetOrdinal("COMPANY_TYPE")),
                                    CompanyLocation = reader.GetString(reader.GetOrdinal("COMPANY_LOCATION")),
                                    CompanyDescription = reader.GetString(reader.GetOrdinal("COMPANY_DESCRIPTION")),
                                    CountryName = reader.GetString(reader.GetOrdinal("COUNTRY_NAME")),
                                    CompanyEmail = reader.GetString(reader.GetOrdinal("COMPANY_EMAIL")),
                                    ContactNum = reader.GetString(reader.GetOrdinal("CONTACT")),
                                    ProfilePic = reader.GetString(reader.GetOrdinal("PROFILE_PIC"))
                                };
                                connection.Close();
                                return Ok(companyDetail);
                            }
                            else
                            {
                                return NotFound();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred in retrieving CompanyDetails: {ex.Message}");
            }
        }
    }
}
