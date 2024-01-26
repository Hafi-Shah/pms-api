using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pms_api.Data;
using pms_api.Models.Response;
using System;
using System.Data;
using System.Data.SqlClient;


namespace pms_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UpdateUserByRoleController : Controller
    {


        [HttpPut("UpdateUserData")]
        public dynamic UpdateDataByRoleUser(string role, UpdateDataByRoleUserRes userModel)
        {
            try
            {
               
                using (SqlConnection connection = DatabaseConnection.getConnection())
                {
                    using (SqlCommand command = new SqlCommand("UPDATE_USER_DATA", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Role", role);

                        // Add other parameters from the request object based on role
                        if (role == "user")
                        {
                            command.Parameters.AddWithValue("@UserId", userModel.UserId);
                            command.Parameters.AddWithValue("@FirstName", userModel.FirstName);
                            command.Parameters.AddWithValue("@LastName", userModel.LastName);
                            command.Parameters.AddWithValue("@Email", userModel.Email);
                            command.Parameters.AddWithValue("@Password", userModel.Password);
                            command.Parameters.AddWithValue("@Contact", userModel.Contact);
                            command.Parameters.AddWithValue("@Dob", userModel.Dob);
                            command.Parameters.AddWithValue("@UserTypeId", userModel.UserTypeId);
                            command.Parameters.AddWithValue("@YearlyExp", userModel.YearlyExp);
                            command.Parameters.AddWithValue("@GenderId", userModel.GenderId);
                            command.Parameters.AddWithValue("@MaritalStatusId", userModel.maritalStatusId);
                            command.Parameters.AddWithValue("@City", userModel.City);
                            command.Parameters.AddWithValue("@About", userModel.About);
                            command.Parameters.AddWithValue("@CoverImg", userModel.CoverImg);
                            command.Parameters.AddWithValue("@ProfilePic", userModel.ProfilePic);
                            command.ExecuteNonQuery();
                        }

                    }
                    if (userModel.Skills != null && userModel.Skills.Count > 0)
                    {
                        using (SqlCommand command = new SqlCommand("PRC_DELETE_USER_SKILL", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@UserId", userModel.UserId);
                            command.Parameters.AddWithValue("@Role", role);
                            command.ExecuteNonQuery();
                        }
                    }
                    foreach (var item in userModel.Skills)
                    {
                        using (SqlCommand command = new SqlCommand("PRC_ADD_USER_SKILL", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@UserId", userModel.UserId);
                            command.Parameters.AddWithValue("@Role", role);
                            command.Parameters.AddWithValue("@SkillId", item);

                            command.ExecuteNonQuery();

                        }
                    }

                }
                return Ok(new { success = true, message = "User update Successfully" });
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
