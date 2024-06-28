using System;

namespace H2H.Physiotherapy.Common.Models.BaseModels
{
    public class UserBaseData
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public string EmailId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int RoleId { get; set; }
        //public string Role { get; set; }
        public string Name { get; set; }
        public string EmployeId { get; set; }
        public string Code { get; set; }
        public int? SupervisorId { get; set; }
        public string SupervisorName { get; set; }
        public string EmiratesId { get; set; }
        public int Emirates { get; set; }
        public string PassPortNumber { get; set; }
        public DateTime? PassPortExpiry { get; set; }
        public string VisaNumber { get; set; }
        public DateTime? VisaExpiry { get; set; }
        public string Qualification { get; set; }
        public string Bio { get; set; }
        public DateTime? LastVisited { get; set; }
        public string ImagePath { get; set; }
        public int? WorkingHours { get; set; }
        public string Address { get; set; }
        public long? ContactNo { get; set; }
        public int? Country { get; set; }
        public string CountryName { get; set; }
        public int? CreatedBy { get; set; }
        public string IdentityColor { get; set; }
        public bool? ChangePasswordOnNextLogin { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? IsActive { get; set; }
      
    }

}
