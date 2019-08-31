namespace EmailSending.Web.Api.Configuration
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string EmailsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IDatabaseSettings
    {
        string EmailsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}