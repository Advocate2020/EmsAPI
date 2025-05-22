using BookXChangeApi.Util.Swagger.SwaggerResponseAttributes;
using EmployeeManagementSystemDB.Databases;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystemAPI.Controllers.Interfaces
{
    [SuccessResponse]
    [BadRequestResponse]
    [ForbiddenResponse]
    [UnauthorizedResponse]
    [UnhandledExceptionResponse]
    public abstract class EMSBaseController : MainController<EmpDatabaseContext>
    {
        protected EMSBaseController(EmpDatabaseContext dbContext, IDbContextFactory<EmpDatabaseContext> contextFactory) : base(dbContext, contextFactory)
        {
        }
    }
}