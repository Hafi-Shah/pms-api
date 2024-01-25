using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pms_api.Data;
using pms_api.Models.Response;
using System.Data.SqlClient;
using System.Data;

namespace pms_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCardsController : ControllerBase
    {
        [HttpGet("GetUserCards")]
        public dynamic UserCards()
        {
            try
            {
                List<GetUserCards> obj = new List<GetUserCards>();


                using (SqlConnection connection = DatabaseConnection.getConnection())
                {


                    using (SqlCommand command = new SqlCommand("PRC_GET_USER_CARDS", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GetUserCards userCards = new GetUserCards
                                {
                                    UserId = Convert.ToInt32(reader["USER_ID"]),
                                    FirstName = reader["FIRST_NAME"].ToString(),
                                    LastName = reader["LAST_NAME"].ToString(),
                                    TotalExp = Convert.ToInt32(reader["TOTAL_EXP"]),
                                    UserType = reader["USER_TYPE"].ToString(),
                                    ProfilePic = reader["PROFILE_PIC"].ToString(),
                                    CoverImg = reader["COVER_IMAGE"].ToString(),

                                };
                                obj.Add(userCards);
                            }
                        }
                    }

                    return Ok(obj);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred in fetching user cards detail." + ex.Message);
            }
        }
    }
}
