using System.ComponentModel.DataAnnotations;

namespace WebApplication11.Models
{
    public class UserRoleModel
    {
        [Key]
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSelected { get; set; }
    }
}
