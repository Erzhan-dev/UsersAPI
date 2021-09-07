using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data.SqlClient;
using UsersRESTAPI.Models;
using System.Data;
using System.Text.RegularExpressions;

namespace UsersRESTAPI.Repository
{
    public class UsersRepo : IUsersRepo
    {
        string _ConStr=null;
        public UsersRepo(string ConStr) 
        {
            _ConStr = ConStr;
        }
        public string Add(UserModel user)
        {
            Regex rgxLin = new Regex(@"^[\d]{12}$");
            Regex rgxName = new Regex(@"^([А-Я]{1}[а-яё]{1,23}|[A-Z]{1}[a-z]{1,23})$");

            if (!rgxLin.IsMatch(user.Lin))
            {
                return "Uncorrect Lin";
            }

                else if (!rgxName.IsMatch(user.FirstName) && !rgxName.IsMatch(user.LastName))
                {
                    return "Uncorrect FisrtName or LastName";
                }

                    else
                    {
                        try
                        {
                            using (IDbConnection db = new SqlConnection(_ConStr))
                            {
                                db.Query<UserModelWithId>($"INSERT INTO UsersStore (Lin, FirstName,LastName,BirthDate) VALUES('{user.Lin}', '{user.FirstName}','{user.LastName}','{user.BirthDate}')");
                                return "success";
                            }
                        }
                        catch (Exception ex)
                            {
                                return ex.Message;
                            }
                    }
        }

        public string Delete(uint id)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_ConStr))
                {
                    db.Query<UserModelWithId>($"Delete * FROM UsersStore where id={id}");
                    return "success";
                }
            }
            catch (Exception ex) {
                return ex.Message;
            }
        }

        public UserModelWithId GetById(uint id)
        {
                using (IDbConnection db = new SqlConnection(_ConStr))
                {
                    return db.Query<UserModelWithId>($"SELECT * FROM UsersStore where id={id}").FirstOrDefault();
                }
        }

        public List<UserModelWithId> GetUsers()
        {
            using (IDbConnection db = new SqlConnection(_ConStr))
            {
                return db.Query<UserModelWithId>("SELECT * FROM UsersStore").ToList();
            }
        }

        public string Update(UserModel user, uint id)
        {
            Regex rgxLin = new Regex(@"^[\d]{12}$");
            Regex rgxName = new Regex(@"^([А-Я]{1}[а-яё]{1,23}|[A-Z]{1}[a-z]{1,23})$");

            if (!rgxLin.IsMatch(user.Lin))
            {
                return "Uncorrect Lin";
            }

            else if (!rgxName.IsMatch(user.FirstName) && !rgxName.IsMatch(user.LastName))
            {
                return "Uncorrect FisrtName or LastName";
            }

            else
            {
                try
                {
                    using (IDbConnection db = new SqlConnection(_ConStr))
                    {
                        db.Query<UserModelWithId>($"Update UsersStore set Lin='{user.Lin}',FirstName='{user.FirstName}',LastName='{user.LastName}',BirthDate='{user.BirthDate}' where id = {id} ");
                        return "success";
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
    }
}
