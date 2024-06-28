namespace H2H.Physiotherapy.Services.Request
{
    public class RequestContext : IRequestContext
    {
        public RequestContext()
        {

        }

        public string UserId { get; set; }
        //public string UserName { get; set; }
        public string UserRole { get; set; }

        public string UserName { get; set; }
    }
}
