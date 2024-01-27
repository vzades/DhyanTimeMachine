using DhyanTimeMachine.Data.Entity;

namespace DhyanTimeMachine.Service
{
    public interface IQuotesService
    {
        Task CreateAsync(Quotes quote);

        Task<List<Quotes>> GetAsync(DateTime dateTime);

        Task<List<Quotes>> GetAsync();
    }
}