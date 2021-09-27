using System.Collections.Generic;
using UsersRESTAPI.Models;
using System.Threading.Tasks;

namespace UsersRESTAPI.Repository
{
        public interface IUsersRepo
    {
        public Task< List<UserModelWithId>> GetUsers();
        public Task<List<UserModelWithId>> GetById(uint id);
        public Task Delete(uint id);
        public Task Update(UserModel user,uint id);
        public Task Add(UserModel user);
        public Task<string> PasswordComparison(UserModelForAuth user);
    }
}
