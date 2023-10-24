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

        [HttpPut("UpdateDataByRoleUser")]
        public dynamic UpdateDataByRoleUser(int id, string role, UpdateDataByRoleUserRes userModel)
        {
            try
            {
                using (SqlConnection connection = DatabaseConnection.getConnection())
                {
                    using (SqlCommand command = new SqlCommand("UPDATE_DATA_BY_ROLE", connection))
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
                            command.Parameters.AddWithValue("@Title", userModel.Title);
                            command.Parameters.AddWithValue("@Dob", userModel.Dob);
                            command.Parameters.AddWithValue("@TotalExp", userModel.Email);
                            command.Parameters.AddWithValue("@About", userModel.About);
                            command.Parameters.AddWithValue("@Gender", userModel.Gender);
                            command.Parameters.AddWithValue("@MartialStatus", userModel.MartialStatus);
                            command.Parameters.AddWithValue("@UserType", userModel.UserType);
                            //command.Parameters.AddWithValue("@UserSkills", userModel.UserSkills);
                           // command.Parameters.AddWithValue("@CoverPic", userModel.CoverPic);


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


        //[Route("api/[controller]")]
        [HttpPut("UpdateDataByRoleCompany")]
        public dynamic UpdateDataByRoleCompany( string role, UpdateDataByRoleCompanyRes companyModel)
        {
            try
            {
                using (SqlConnection connection = DatabaseConnection.getConnection())
                {
                    using (SqlCommand command = new SqlCommand("UPDATE_DATA_BY_ROLE", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        //command.Parameters.AddWithValue("@UserId", id);
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
                            command.Parameters.AddWithValue("@CompanyPassword", companyModel.Password);
                            command.Parameters.AddWithValue("@ProfilePic", companyModel.ProfilePic);

                        }

                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        // You can return a success message or whatever is appropriate for your application
                        return Ok("Company Data updated successfully");
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
