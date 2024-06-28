using System.ComponentModel.DataAnnotations;

namespace H2H.Physiotherapy.Common.Utilities
{
    public class GrantTypeAttribute : ValidationAttribute
    {
        public string ExpectedGrantType { get; set; }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            if (ExpectedGrantType != null)
            {
                return value.ToString() == ExpectedGrantType;
            }
            return true;
        }


    }
}
