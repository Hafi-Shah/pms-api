using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pms_api.Data;
using pms_api.Models.Requests;
using System;
using System.Data;
using System.Data.SqlClient;

namespace pms_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        [HttpPost("AddCompanyAnnouncement")]
        public IActionResult AddCompanyAnnouncement(int userId, string role,CompanyAnnouncementReq request)
        {
            try
            {
                int announcementId = 0;

                using (SqlConnection connection = DatabaseConnection.getConnection())
                {
                    using (SqlCommand command = new SqlCommand("PRC_ADD_COMPANY_ANNOUNCEMENT", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@Role", role);
                        command.Parameters.AddWithValue("@Expirence", request.Exp);
                        command.Parameters.AddWithValue("@Jd", request.Jd);
                        command.Parameters.AddWithValue("@IsAutoApply", request.IsAutoApply);

                        // Add output parameter for the last inserted ID
                        SqlParameter outputParameter = new SqlParameter("@LastAnnouncementId", SqlDbType.Int);
                        outputParameter.Direction = ParameterDirection.Output;
                        command.Parameters.Add(outputParameter);

                        command.ExecuteNonQuery();

                        // Retrieve the value of the output parameter
                        announcementId = Convert.ToInt32(outputParameter.Value);
                    }

                    foreach (var item in request.SkillId)
                    {
                        using (SqlCommand command = new SqlCommand("PRC_ADD_COMPANY_ANNOUNCEMENT_SKILL", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@UserId", userId);
                            command.Parameters.AddWithValue("@Role", role);
                            command.Parameters.AddWithValue("@SkillId", item);
                            command.Parameters.AddWithValue("@AnnouncementId", announcementId);
                            command.ExecuteNonQuery();
                        }
                    }
                }

                CompanyAnnouncementReq response = new CompanyAnnouncementReq
                {
                    Exp = request.Exp,
                    IsAutoApply = request.IsAutoApply,
                    Jd = request.Jd,
                    SkillId = request.SkillId,
                };

                // Return the response with the actual data
                return Ok(new { success = true, message = "Job Announce Successfully", data = response });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred in announcement." + ex.Message);
            }
        }
    }
}
