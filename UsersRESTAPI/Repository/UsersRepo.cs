using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data.SqlClient;
using UsersRESTAPI.Models;
using System.Data;
using UsersRESTAPI.Services;
using System.Threading.Tasks;

namespace UsersRESTAPI.Repository
{
    public class UsersRepo : IUsersRepo
    {
        string ConStr = null;
        public UsersRepo(string _ConStr) => ConStr = _ConStr;
        public async Task Add(UserModel user)
        {
            string passHash;
            if (user.password != null)
                passHash = PasswordHashing.HashPassword(user.password);
            else
            {
                throw new InvalidExpressionException("Password empty");
            }
            using (IDbConnection db = new SqlConnection(ConStr))
            {
                await db.QueryAsync<UserModelForAuth>($"INSERT INTO UsersStore (Lin, FirstName,LastName,BirthDate,Password) VALUES('{user.Lin}', '{user.FirstName}','{user.LastName}','{user.BirthDate}','{passHash}')");
            }
        }
        public async Task Delete(uint id)
        {
            using (IDbConnection db = new SqlConnection(ConStr))
            {
                await db.QueryAsync<UserModelWithId>($"Delete * FROM UsersStore where id={id}");
            }

        }
        public async Task<List<UserModelWithId>> GetById(uint id)
        {
            using (IDbConnection db = new SqlConnection(ConStr))
            {
                var query = $"select Id,LIn,FirstName,LastName,BirthDate from UsersStore where id ='{id}'";
                return db.Query<UserModelWithId>(query).ToList();
            }
        }
        public async Task<List<UserModelWithId>> GetUsers()
        {
            using (IDbConnection db = new SqlConnection(ConStr))
            {
                var query = "select Id,LIn,FirstName,LastName,BirthDate from UsersStore";

                return db.Query<UserModelWithId>(query).ToList();

            }
        }
        public async Task Update(UserModel user, uint id)
        {
            using (IDbConnection db = new SqlConnection(ConStr))
            {
                await db.QueryAsync<UserModelWithId>($"Update UsersStore set Lin='{user.Lin}',FirstName='{user.FirstName}',LastName='{user.LastName}',BirthDate='{user.BirthDate}' where id = {id} ");
            }
        }

        public async Task<string> PasswordComparison(UserModelForAuth user)
        {
            using (IDbConnection db = new SqlConnection(ConStr))
            {
                string QueryLine = $"select Password from UsersStore where Lin ='{user.LIN}'";
                var Query = db.ExecuteScalar(QueryLine);
                if (Query == null)
                    return null;
                else
                    return Query.ToString();
            }
            
        }

    }
}
