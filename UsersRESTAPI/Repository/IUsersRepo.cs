using System.Collections.Generic;
using UsersRESTAPI.Models;

namespace UsersRESTAPI.Repository
{
        public interface IUsersRepo
    {
        public List<UserModelWithId> GetUsers();
        public UserModelWithId GetById(uint id);
        public string Delete(uint id);
        public string Update(UserModel user,uint id);
        public string Add(UserModel user);

    }
}
