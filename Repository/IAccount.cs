using Microsoft.AspNetCore.Mvc;
using System.Collections;
using WebOopPrac_Api.Models;
using static WebOopPrac_Api.Models.ServiceResponse;
using static WebOopPrac_Api.Models.TodoModel;

namespace WebOopPrac_Api.Repository
{
    public interface IAccount
    {
        Task<ServiceResponseModel<IEnumerable<dynamic>>> CredInsert(string title, string description, bool IsCompleted);
        Task<IEnumerable<TodoModel>> GetTodoModel();
        Task<IEnumerable<UserCred>> getAllUserCreds();
		Task<IEnumerable<TodoModel>> GetOneData(int uniqueID);
        Task<IEnumerable<UserCred>> getUserPass(string username, string password);
		Task<ServiceResponseModel<IEnumerable<dynamic>>> UpdateData(int id, string title, string description, bool isCompleted);
        Task<ServiceResponseModel<IEnumerable<dynamic>>> UpdateUserData(int Id, string Name, string LastName, string MiddleInitial, DateTime BirthDate, string Email, string Username, string Password, bool TermsCondition);
		Task<ServiceResponseModel<IEnumerable<dynamic>>> DeleteItem(int id);
        Task<ServiceResponseModel<IEnumerable<dynamic>>> UserCredRegister(string Name, string LastName, string MiddleInitial, DateTime BirthDate, string Email, string Username, string Password, bool TermsCondition);

	}
}
