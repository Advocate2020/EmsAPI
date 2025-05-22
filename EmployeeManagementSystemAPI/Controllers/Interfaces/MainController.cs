using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystemAPI.Controllers.Interfaces
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json", new string[] { })]
    public abstract class MainController<T> : ControllerBase where T : DbContext
    {
        protected MainController(T dbContext, IDbContextFactory<T> contextFactory)
        {
            dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
    }
}