using System;

namespace H2H.Physiotherapy.Common.Models.BaseModels
{
    public class CountryModel
    {
        public int Id { get; set; }

        public string CountryId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public long PhoneCode { get; set; }

        public bool IsActive { get; set; }
    }
}