using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pms_api.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace pms_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DeleteController : ControllerBase
    {
        [HttpPost]
        public IActionResult DeleteUserOrCompany([FromQuery] int userId, string role, string password)
        {
            try
            {
                using (SqlConnection connection = DatabaseConnection.getConnection())
                {
                    if (connection != null)
                    {
                        using (SqlCommand command = new SqlCommand("DELETE_USER_OR_COMPANY", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@UserId", userId);
                            command.Parameters.AddWithValue("@Role", role);
                            command.Parameters.AddWithValue("@PasswordToDelete", password);

                            command.ExecuteNonQuery();
                        }
                    }
                }

                return Ok(new { success = true, message = "Account Deleted Successful" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An error occurred while deleting data " + ex.Message });
            }
        }
    }
}
