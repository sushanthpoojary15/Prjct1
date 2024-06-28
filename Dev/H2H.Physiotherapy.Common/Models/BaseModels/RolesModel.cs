using System;
using System.ComponentModel.DataAnnotations;

namespace H2H.Physiotherapy.Common.Models.BaseModels
{
    public class RolesModel
    {
        public int Id { get; set; }

        public string RoleId { get; set; }

        [Required(ErrorMessage = "Role Name is required")]
        public string Name { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public int UpdatedBy { get; set; }

        public bool IsActive { get; set; }
    }
}
