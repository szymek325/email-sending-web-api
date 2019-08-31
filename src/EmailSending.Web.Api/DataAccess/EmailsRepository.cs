using System.Collections.Generic;
using System.Threading.Tasks;
using EmailSending.Web.Api.Configuration;
using EmailSending.Web.Api.DataAccess.Entities;
using MongoDB.Driver;

namespace EmailSending.Web.Api.DataAccess
{
    public interface IEmailsRepository
    {
        Task Create(Email email);
        Task<IList<Email>> GetAll();
        Task<Email> Get(string id);
        Task Update(string id, Email rentalIn);
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

        public async Task<IList<Email>> GetAll()
        {
            var result = await _rentals.FindAsync(rental => true);
            return await result.ToListAsync();
        }

        public async Task<Email> Get(string id)
        {
            var result = await _rentals.FindAsync(rental => rental.Id == id);
            return await result.SingleOrDefaultAsync();
        }

        public async Task Update(string id, Email rentalIn)
        {
            await _rentals.ReplaceOneAsync(rental => rental.Id == id, rentalIn);
        }
    }
}