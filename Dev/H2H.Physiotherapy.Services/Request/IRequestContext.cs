namespace H2H.Physiotherapy.Services.Request
{
    public interface IRequestContext
    {
        public string UserId { get; set; }
        //public string UserName { get; set; }
        public string UserRole { get; set; }
        public string UserName { get; set; } 
    }
}
