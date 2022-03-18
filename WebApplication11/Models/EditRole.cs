using System.ComponentModel.DataAnnotations;

namespace WebApplication11.Models
{
    public class EditRole
    {
       
        public EditRole()
        {
            Users = new List<string>();
        }

        public string Id { get; set; }  
        [Required (ErrorMessage = "Role Name is Required")]
        public string RoleName { get; set; }
        public List<string> Users { get; set; }

    }
}
