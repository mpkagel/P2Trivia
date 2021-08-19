using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Library.Models;

namespace BLL.Library.IRepositories
{
    public interface IUsersRepo
    {
        int GetUserId(string name);
        UsersModel GetUserById(int id);
        Task<UsersModel> GetUserByName(string name);
        bool CheckUserByName(string name);
        List<UsersModel> GetAllUsers();
        Task<UsersModel> AddAsync(UsersModel user);
        Task<UsersModel> DeleteAsync(UsersModel user);
        Task<UsersModel> EditUserAsync(UsersModel user);
    }
}
