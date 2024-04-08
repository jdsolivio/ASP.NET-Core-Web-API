using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        public async Task<ServiceResponseModel<IEnumerable<dynamic>>> CredInsert(string title, string description, bool IsCompleted)
        {
            var response = new ServiceResponseModel<IEnumerable<dynamic>>();

            try
            {
                long Unqid = IDGenerator.GenerateID();

                using var conn = new SqlConnection(_configuration.GetConnectionString("JDataBase"));
                {
                    var param = new DynamicParameters();
                    param.Add("title", title);
                    param.Add("description", description);
                    param.Add("IsCompleted", IsCompleted);
                    param.Add("UniqueID", Unqid);
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

        public async Task<ServiceResponseModel<IEnumerable<dynamic>>> UserCredRegister(string Name, string LastName, string MiddleInitial, DateTime BirthDate, string Email, string Username, string Password, bool TermsCondition)
        {
            var response = new ServiceResponseModel<IEnumerable<dynamic>>();
            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("JDataBase"));
                {
                    var param = new DynamicParameters();
                    param.Add("name", Name);
                    param.Add("lastName", LastName);
                    param.Add("middleInitial", MiddleInitial);
                    param.Add("birthDate", BirthDate);
                    param.Add("email", Email);
                    param.Add("username", Username);
                    param.Add("password", Password);
                    param.Add("termsCondition", TermsCondition);
					param.Add("retVal", dbType: DbType.Int32, direction: ParameterDirection.Output);

					conn.Open();
					var res1 = await conn.QueryAsync("[dbo].[InsertUserTodoItem]", param, commandType: CommandType.StoredProcedure);
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

        public async Task<IEnumerable<UserCred>> getAllUserCreds()
        {
            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("JDataBase"));
                {
                    conn.Open();
                    var res4 = await conn.QueryAsync<UserCred>("[dbo].[GetAllUserRegistrationTodoItems]", commandType: CommandType.StoredProcedure);
                    conn.Close();
                    return res4;
                }
            }
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}
        public async Task<IEnumerable<TodoModel>> GetOneData(int uniqueID)
        {
            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("JDataBase"));
                {
                    await conn.OpenAsync();
                    var res5 = await conn.QueryAsync<TodoModel>("[dbo].[GetSingleTodoItem]", new { UniqueID = uniqueID }, commandType: CommandType.StoredProcedure);
                    await conn.CloseAsync();
                    return res5;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

		public async Task<IEnumerable<UserCred>> getUserPass(string username, string password)
		{
			try
			{
				using var conn = new SqlConnection(_configuration.GetConnectionString("JDataBase"));
				{
					conn.Open();
					var res4 = await conn.QueryAsync<UserCred>("[dbo].[GetUsernamePassword]",new {Username = username, Password = password }, commandType: CommandType.StoredProcedure);
					conn.Close();
					return res4;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}


		public async Task<ServiceResponseModel<IEnumerable<dynamic>>> UpdateData(int id, string title, string description, bool isCompleted)
        {
            var response = new ServiceResponseModel<IEnumerable<dynamic>>();

            try
            {
                int existingUniqueID;
                using (var conn = new SqlConnection(_configuration.GetConnectionString("JDataBase")))
                {
                    conn.Open();
                    existingUniqueID = await conn.QueryFirstOrDefaultAsync<int>("SELECT UniqueID FROM [dbo].[TodoItems] WHERE Id = @Id", new { Id = id });
                }
                using (var conn = new SqlConnection(_configuration.GetConnectionString("JDataBase")))
                {
                    var param = new DynamicParameters();
                    param.Add("Id", id);
                    param.Add("Title", title);
                    param.Add("Description", description);
                    param.Add("IsCompleted", isCompleted);
                    param.Add("UniqueID", existingUniqueID);

                    conn.Open();
                    var res2 = await conn.QueryAsync("[dbo].[UpdateTodoItems]", param, commandType: CommandType.StoredProcedure);
                    conn.Close();

                    response.HttpCode = ServiceResponseStatusCode.Success;
                    response.UserMessage = "Success";
                    response.ResponseCode = ServiceResponseStatusCode.Success;
                    response.Data = res2;
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

		public async Task<ServiceResponseModel<IEnumerable<dynamic>>> UpdateUserData(int Id, string Name, string LastName, string MiddleInitial, DateTime BirthDate, string Email, string Username, string Password, bool TermsCondition)
        {
            var response = new ServiceResponseModel<IEnumerable<dynamic>>();
            try
            {
				using (var conn = new SqlConnection(_configuration.GetConnectionString("JDataBase")))
				{
					conn.Open();
				}
				using (var conn = new SqlConnection(_configuration.GetConnectionString("JDataBase")))
				{
					var param = new DynamicParameters();
					param.Add("name", Name);
					param.Add("lastName", LastName);
					param.Add("middleInitial", MiddleInitial);
					param.Add("birthDate", BirthDate);
					param.Add("email", Email);
					param.Add("username", Username);
					param.Add("password", Password);
					param.Add("termsCondition", TermsCondition);

					conn.Open();
					var res2 = await conn.QueryAsync("[dbo].[UpdateTodoItems]", param, commandType: CommandType.StoredProcedure);
					conn.Close();

					response.HttpCode = ServiceResponseStatusCode.Success;
					response.UserMessage = "Success";
					response.ResponseCode = ServiceResponseStatusCode.Success;
					response.Data = res2;
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

		public async Task<ServiceResponseModel<IEnumerable<dynamic>>> DeleteItem(int id)
        {
            var response = new ServiceResponseModel<IEnumerable<dynamic>>();

            try
            {
                using var conn = new SqlConnection(_configuration.GetConnectionString("JDataBase"));
                {
                    var param = new DynamicParameters();
                    param.Add("Id", id);

                    conn.Open();
                    var res4 = await conn.QueryAsync("[dbo].[DeleteTodoItem]", param, commandType: CommandType.StoredProcedure);
                    conn.Close();

                    response.HttpCode = ServiceResponseStatusCode.Success;
                    response.UserMessage = "Success";
                    response.ResponseCode = ServiceResponseStatusCode.Success;
                    response.Data = res4;
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

    }
}
    
    