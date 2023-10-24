using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pms_api.Data;
using pms_api.Models.Response;
using System.Data;
using System.Data.SqlClient;

namespace pms_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SkillsController : Controller
    {
        [HttpGet("GetSkillsName")]
        public IActionResult getSkillsName()
        {
            try
            {
                List<GetSkillsName> list = new List<GetSkillsName>();

                using (SqlConnection connection = DatabaseConnection.getConnection())
                {
                    using (SqlCommand command = new SqlCommand("PRC_GET_SKILLS_NAME", connection)) 
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GetSkillsName skill = new GetSkillsName
                                {
                                    Id = Convert.ToInt32(reader["Id"].ToString()),
                                    Name = reader["Name"].ToString()

                                };
                                list.Add(skill);
                            }
                        }
                    }
                    return Ok(list);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving Skill Names" + ex.Message);
            }
        }
        
    }
}
