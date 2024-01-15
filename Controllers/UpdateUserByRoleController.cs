using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pms_api.Data;
using pms_api.Models.Response;
using System;
using System.Data;
using System.Data.SqlClient;


namespace pms_api.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UpdateUserByRoleController : Controller
    {


        [HttpPut("UpdateUserData")]
        public dynamic UpdateDataByRoleUser(int id, string role, UpdateDataByRoleUserRes userModel)
        {
            try
            {
                using (SqlConnection connection = DatabaseConnection.getConnection())
                {
                    using (SqlCommand command = new SqlCommand("UPDATE_USER_DATA", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserId", id);
                        command.Parameters.AddWithValue("@Role", role);

                        // Add other parameters from the request object based on role
                        if (role == "user")
                        {

                            command.Parameters.AddWithValue("@FirstName", userModel.FirstName);
                            command.Parameters.AddWithValue("@LastName", userModel.LastName);
                            command.Parameters.AddWithValue("@Email", userModel.Email);
                            command.Parameters.AddWithValue("@Password", userModel.Password);
                           


                            // Add other user-specific parameters here
                        }


                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        // You can return a success message or whatever is appropriate for your application
                        return Ok("User Data updated successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the update process
                return StatusCode(500, "An error occurred while updating data by role: " + ex.Message);
            }
        }


      
        [HttpPut("UpdateCompanyData")]
        public dynamic UpdateDataByRoleCompany(string role, UpdateDataByRoleCompanyRes companyModel)
        {
            try
            {
                RegisterCompanyResponse response = new RegisterCompanyResponse();
                bool isSuccess = false;
                string resMessage = "Updated successfully";
                using (SqlConnection connection = DatabaseConnection.getConnection())
                {
                    using (SqlCommand command = new SqlCommand("UPDATE_COMPANY_DATA", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Role", role);

                        // Add other parameters from the request object based on role
                        if (role == "company")
                        {
                            command.Parameters.AddWithValue("@UserId", companyModel.UserId);
                            command.Parameters.AddWithValue("@CompanyName", companyModel.CompanyName);
                            command.Parameters.AddWithValue("@CompanyDescription", companyModel.CompanyDescription);
                            command.Parameters.AddWithValue("@CompanyType", companyModel.CompanyType);
                            command.Parameters.AddWithValue("@CompanyLocation", companyModel.CompanyLocation);
                            command.Parameters.AddWithValue("@Country", companyModel.CountryName);
                            command.Parameters.AddWithValue("@CompanyEmail", companyModel.CompanyEmail);
                            command.Parameters.AddWithValue("@CompanyPassword", companyModel.CompanyPassword);
                            command.Parameters.AddWithValue("@ProfilePic", companyModel.ProfilePic);
                            if (command.ExecuteNonQuery() > 0)
                            {
                                resMessage = "Company updated successfully";
                                isSuccess = true;
                            }
                            else
                            {
                                resMessage = "Error while updating record.";
                                isSuccess = false;
                            }
                        }

                        response.isSuccess = isSuccess;
                        response.Message = resMessage;
                        return Ok(response);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the update process
                return StatusCode(500, "An error occurred while updating data by role: " + ex.Message);
            }
        }
    }
}
