namespace WebAppDemoStudent.Models
{
    public class EmailConfiguration
    {
        public string From { get; set; }
        public string SntpServer { get; set; }
        public int Port { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
