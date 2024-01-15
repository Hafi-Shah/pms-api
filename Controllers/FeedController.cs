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
    public class FeedController : ControllerBase
    {
        [HttpGet("GetFeedData")]
        public dynamic GetFeedRes()
        {
            try
            {
                List<GetFeedData> feeds = new List<GetFeedData>();
                List<GetFeedDataRes> feedResponses = new List<GetFeedDataRes>();

                using (SqlConnection connection = DatabaseConnection.getConnection())
                {
                    if (connection != null)
                    {
                        using (SqlCommand command = new SqlCommand("PRC_GET_FEED", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int companyId = Convert.ToInt32(reader["COMPANY_ID"]);
                                    GetFeedDataRes existingFeedResponse = feedResponses.FirstOrDefault(feed => feed.companyId == companyId);

                                    if (existingFeedResponse == null)
                                    {
                                        existingFeedResponse = new GetFeedDataRes()
                                        {
                                            companyId = companyId,
                                            companyName = reader["COMPANY_NAME"].ToString(),
                                            companyType = reader["COMPANY_TYPE"].ToString(),
                                            profilePic = reader["PROFILE_PIC"].ToString(),
                                            country = reader["COUNTRY"].ToString(),
                                            jobDescription = reader["JOB_DESCRIPTION"].ToString(),
                                            yearOfExp = Convert.ToInt32(reader["YEARS_OF_EXP"]),
                                        };
                                        feedResponses.Add(existingFeedResponse);
                                    }

                                    // Assuming there is a column named "SKILL" for each skill record
                                    string skill = reader["SKILL"].ToString();
                                    if (!string.IsNullOrEmpty(skill))
                                    {
                                        existingFeedResponse.skills.Add(skill);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.Error.WriteLine("Create Db Connection");
                    }
                }
                return Ok(feedResponses);
            }
            catch (Exception ex)
            {
                // Log the exception or perform additional error handling as needed
                Console.Error.WriteLine("An error occurred: " + ex.Message);
                return StatusCode(500, "An error occurred while retrieving Feed Data.");
            }
        }
    }

}

