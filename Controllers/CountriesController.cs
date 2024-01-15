using Microsoft.AspNetCore.Mvc;
using pms_api.Data;
using pms_api.Models.Response;
using System.Data.SqlClient;
using System.Data;

namespace pms_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : Controller
    {
        [HttpGet("GetCountries")]
        public IActionResult GetCountriesRes()
        {
            try
            {
                List<GetCountries> countries = new List<GetCountries>();

                using (SqlConnection connection = DatabaseConnection.getConnection())
                {
                    if (connection != null)
                    {
                        using (SqlCommand command = new SqlCommand("PRC_GET_COUNTRY", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    GetCountries Country = new GetCountries()
                                    {
                                        Id = Convert.ToInt32(reader["Id"]),
                                        Name = reader["Name"].ToString()
                                    };
                                    countries.Add(Country);
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.Error.WriteLine("Create Db Connection");
                    }
                }

                return Ok(countries);
            }
            catch (Exception ex)
            {
                // Log the exception or perform additional error handling as needed
                Console.Error.WriteLine("An error occurred: " + ex.Message);
                return StatusCode(500, "An error occurred while retrieving country names.");
            }
        }
    }
}
