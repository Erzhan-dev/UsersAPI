using System.ComponentModel.DataAnnotations;

namespace UsersRESTAPI.Models
{
    public class UserModelWithId : UserModel
    {
        [Required]
        public uint Id { get; }

    }
}
