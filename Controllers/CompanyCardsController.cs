using Microsoft.AspNetCore.Mvc;
using pms_api.Data;
using pms_api.Models.Response;
using System.Data.SqlClient;
using System.Data;

namespace pms_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyCardsController : Controller
    {
        [HttpGet("GetCompanyCards")]
        public IActionResult CompanyCards()
        {
            try
            {
                List<GetCompanyCardsRes> obj = new List<GetCompanyCardsRes>();
                

                using (SqlConnection connection = DatabaseConnection.getConnection())
                {


                    using (SqlCommand command = new SqlCommand("PRC_GET_COMPANY_CARDS", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GetCompanyCardsRes companyCards = new GetCompanyCardsRes
                                {
                                    CompnanyId = Convert.ToInt32(reader["COMPANY_ID"]),
                                    CompanyName = reader["COMPANY_NAME"].ToString(),
                                    CompanyType = reader["COMPANY_TYPE"].ToString(),
                                    Country = reader["COUNTRY_NAME"].ToString(),
                                    ProfilePic = reader["PROFILE_PIC"].ToString(),
                                };
                                obj.Add(companyCards);
                            }
                        }
                    }

                    return Ok(obj);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred in RegisterCompany." + ex.Message);
            }
        }
    }
}
