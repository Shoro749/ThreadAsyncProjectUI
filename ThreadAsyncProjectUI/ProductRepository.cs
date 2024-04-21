using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ThreadAsyncProjectUI.Models;

namespace ThreadAsyncProjectUI
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product item)
        {
            _context.Product.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IQueryable<Product>> GetAllAsync(CancellationToken token)
        {
            return (await _context.Product.ToListAsync(token)).AsQueryable();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            Product? product = await _context.Product.FindAsync(id) ??
                throw new NullReferenceException();

            return product;
        }
    }
}
