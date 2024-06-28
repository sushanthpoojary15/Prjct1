using System.ComponentModel.DataAnnotations;

namespace H2H.Physiotherapy.Common.Models.DTO
{
    public class RolesModelDTO
    {
        public string RoleId { get; set; }

        [Required(ErrorMessage = "Role Name is required")]
        public string Name { get; set; }
    }
}
