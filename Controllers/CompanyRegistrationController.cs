using System.Buffers.Text;
using System.Data;
using System.Data.SqlClient;

using Microsoft.AspNetCore.Mvc;
using pms_api.Data;
using pms_api.Models.Requests;
using pms_api.Models.Response;

namespace pms_api
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyRegistrationController : ControllerBase
    {
        [HttpPost("RegisterCompany")]
        public IActionResult RegisterCompany(RegisterCompanyReq request)
        {
            try
            {
                RegisterCompanyResponse response = new RegisterCompanyResponse();
                bool isSuccess = false;
                string resMessage = "Company registered successfully";

                using (SqlConnection connection = DatabaseConnection.getConnection())
                {
                    using (SqlCommand command = new SqlCommand("PRC_REGISTER_COMPANY", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@CompanyName", request.CompanyName);
                        command.Parameters.AddWithValue("@CompanyTypeId", request.CompanyTypeId);
                        command.Parameters.AddWithValue("@Email", request.Email);
                        command.Parameters.AddWithValue("@Password", request.Password);
                        command.Parameters.AddWithValue("@ContactNum", request.ContactNum);
                        command.Parameters.AddWithValue("@Description", request.Description);
                        command.Parameters.AddWithValue("@Location", request.Location);
                        command.Parameters.AddWithValue("@CountryId", request.CountryId);
                        command.Parameters.AddWithValue("@ProfilePic", request.ProfilePic);

                        if (command.ExecuteNonQuery() > 0)
                        {
                            resMessage = "Company registered successfully";
                            isSuccess = true;
                        }
                        else
                        {
                            resMessage = "Error inserting record.";
                            isSuccess = false;
                        }

                    }
                }
                response.isSuccess = isSuccess;
                response.Message = resMessage;
                return Ok(response);


            }
            catch (Exception ex)
            {
                // Log the exception or perform additional error handling as needed                
                return StatusCode(500, "An error occurred in RegisterCompany." + ex.Message);
            }
        }

        [HttpGet("GetCompanyTypes")]
        public IActionResult GetCompanyTypes()
        {
            try
            {
                List<GetCompanyTypesRes> companyTypes = new List<GetCompanyTypesRes>();

                using (SqlConnection connection = DatabaseConnection.getConnection())
                {
                    if (connection != null)
                    {
                        using (SqlCommand command = new SqlCommand("PRC_GET_COMPANY_TYPES", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    GetCompanyTypesRes companyType = new GetCompanyTypesRes
                                    {
                                        Id = Convert.ToInt32(reader["Id"]),
                                        Name = reader["Name"].ToString()
                                    };
                                    companyTypes.Add(companyType);
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.Error.WriteLine("Create Db Connection");
                    }
                }

                return Ok(companyTypes);
            }
            catch (Exception ex)
            {
                // Log the exception or perform additional error handling as needed                
                return StatusCode(500, "An error occurred while retrieving company types." + ex.Message);
            }
        }
    }
}
