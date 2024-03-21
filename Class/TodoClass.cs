using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using WebOopPrac_Api.Models;
using WebOopPrac_Api.Repository;
using static WebOopPrac_Api.Models.ServiceResponse;
using static WebOopPrac_Api.Models.TodoModel;

namespace WebOopPrac_Api.Class
{
    public class TodoClass : IAccount
    {
        private readonly IConfiguration _configuration;
        public TodoClass(IConfiguration configuration)
        {
            _configuration = configuration;
        }
       public async Task<ServiceResponseModel<IEnumerable<dynamic>>> CredInsert(string title,string description, bool IsCompleted)
        {
            var response = new ServiceResponseModel<IEnumerable<dynamic>>();

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("JDataBase"));
                {
                    var param = new DynamicParameters();
                    param.Add("title", title);
                    param.Add("description", description);
                    param.Add("IsCompleted", IsCompleted);
                    param.Add("retVal", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    conn.Open();
                    var res1 = await conn.QueryAsync("[dbo].[InsertTodoItems]", param, commandType: CommandType.StoredProcedure);
                    conn.Close();

                    int res = param.Get<int>("retVal");
                    response.HttpCode = res == 200 ? ServiceResponseStatusCode.Success : ServiceResponseStatusCode.InternalError;
                    response.UserMessage = res == 200 ? "Success" : "Failed to Register!";
                    response.ResponseCode = res == 200 ? ServiceResponseStatusCode.Success : ServiceResponseCode.SqlError;
                    response.Data = res1;

                }
            }
            catch (SqlException ee) 
            {
            response.HttpCode = ServiceResponseStatusCode.InternalError;
                response.ResponseCode = ServiceResponseCode.SqlError;
                response.DeveloperMessage = ee.Message;
                response.UserMessage = "It seems something went wrong on our end. Please try again";
            }
            catch (Exception ee)
            {
                response.HttpCode = ServiceResponseStatusCode.InternalError;
                response.ResponseCode = ServiceResponseCode.ProcessException;
                response.DeveloperMessage = ee.Message;
                response.UserMessage = "Oops! Something went wrong on our end. Please try again.";
            }
            return response;
        }

        public async Task<IEnumerable<TodoModel>> GetTodoModel()
        {
            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("JDataBase"));
                {
                    conn.Open();
                    var res3 = await conn.QueryAsync<TodoModel>("[dbo].[GetAllTodoItems]", commandType: CommandType.StoredProcedure);
                    conn.Close();
                    return res3;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
