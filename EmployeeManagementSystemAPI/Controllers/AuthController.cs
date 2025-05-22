using BookXChangeApi.Util.Swagger;
using BookXChangeApi.Util.Swagger.SwaggerResponseAttributes;
using EmployeeManagementSystemAPI.Controllers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WowBL.Logic.ProfileNS.Interfaces;

namespace EmployeeManagementSystemAPI.Controllers
{
    public class AuthController(IUserBL ProfileBL, IFirebaseHandler FirebaseHandler) : EMSBaseController
    {
        [HttpPost]
        [Authorize] // No role required.
        [SwaggerOperation(
            Summary = "Add Profile",
            Description = "A user adds their profile.",
            Tags = [EMSTags.Auth])]
        [SuccessResponse("Profile added.")]
        public IActionResult Index()
        {
            return View();
        }
    }
}