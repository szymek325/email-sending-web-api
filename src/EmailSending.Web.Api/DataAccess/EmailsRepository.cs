using System.Threading.Tasks;
using EmailSending.Web.Api.Configuration;
using EmailSending.Web.Api.DataAccess.Entities;
using MongoDB.Driver;

namespace EmailSending.Web.Api.DataAccess
{
    public interface IEmailsRepository
    {
        Task Create(Email email);
    }

    public class EmailsRepository : IEmailsRepository
    {
        private readonly IMongoCollection<Email> _rentals;

        public EmailsRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _rentals = database.GetCollection<Email>(settings.EmailsCollectionName);
        }

        public async Task Create(Email email)
        {
            await _rentals.InsertOneAsync(email);
        }
    }
}