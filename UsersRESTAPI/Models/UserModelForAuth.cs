using System.ComponentModel.DataAnnotations;

namespace UsersRESTAPI.Models
{
    public class UserModelForAuth
    {
        [Required]
        public string LIN { get; set; }


        [Required]
        public string Password { get; set; }

    }
}
