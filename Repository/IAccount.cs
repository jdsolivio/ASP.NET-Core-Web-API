using WebOopPrac_Api.Models;
using static WebOopPrac_Api.Models.ServiceResponse;
using static WebOopPrac_Api.Models.TodoModel;

namespace WebOopPrac_Api.Repository
{
    public interface IAccount
    {
        Task<ServiceResponseModel<IEnumerable<dynamic>>> CredInsert(string title, string description, bool IsCompleted);
        Task<IEnumerable<TodoModel>> GetTodoModel();
    }
}
