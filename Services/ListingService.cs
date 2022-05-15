using Microsoft.EntityFrameworkCore;
using wapp_workshop.Models;

namespace wapp_workshop.Services;

public class ListingService : IListingService
{
    private readonly AIRBNB2022Context _context;
    private readonly DbSet<Listing> _repo;

    public ListingService(AIRBNB2022Context context)
    {
        _context = context;
        _repo = _context.Set<Listing>();
    }

    public Task<List<Listing>> GetAll()
    {
        return _repo.ToListAsync();
    }
    public Task<Listing> Get(int id)
    {
        throw new NotImplementedException();
    }
    public Task<Listing> Update(int id, Listing listing)
    {
        throw new NotImplementedException();
    }
    public Task<Listing> Create(Listing listing)
    {
       throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
       throw new NotImplementedException();
    }
    public Task<bool> Exists(int id)
    {
        throw new NotImplementedException();
    }
}