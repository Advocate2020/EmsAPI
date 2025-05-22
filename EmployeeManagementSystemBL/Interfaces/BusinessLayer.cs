using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace EmployeeManagementSystemBL.Interfaces
{
    public abstract class BusinessLayer<T> where T : DbContext
    {
        protected IDbContextFactory<T> ContextFactory { get; }

        public bool SetDatabaseCommandTimeout { get; set; } = true;

        public BusinessLayer(IDbContextFactory<T> contextFactory)
        {
            ContextFactory = contextFactory;
        }

        public async Task<T> CreateDbContext(int commandTimeout)
        {
            T val = await ContextFactory.CreateDbContextAsync();
            if (SetDatabaseCommandTimeout)
            {
                val.Database.SetCommandTimeout(commandTimeout);
            }

            return val;
        }

        public async Task ExecuteWithTransaction(Func<T, Task> action, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, int commandTimeout = 60)
        {
            using T _context = await CreateDbContext(commandTimeout);
            using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync(isolationLevel);
            try
            {
                await action(_context);
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}