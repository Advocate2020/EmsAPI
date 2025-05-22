using EmployeeManagementSystemBL.DTO_s.Post;
using EmployeeManagementSystemBL.Extentions;
using EmployeeManagementSystemBL.Interfaces;
using EmployeeManagementSystemDB.Databases;
using EmployeeManagementSystemDB.Databases.BaseData;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WowBL.Logic.FirebaseNS;
using WowBL.Logic.ProfileNS.Interfaces;

namespace EmployeeManagementSystemBL.Logic.ProfileNS
{
    public class UserBL(IDbContextFactory<EmpDatabaseContext> ContextFactory, IUserQueries Queries, IFirebaseHandler FirebaseHandler) : BusinessLayer<EmpDatabaseContext>(ContextFactory), IUserBL
    {
        public async Task AddProfile(AddEmployeeForm form, string firebaseId, HttpRequest request, string userEmail)
        {
            int userId = 0;

            await ExecuteWithTransaction(async (tContext) =>
                {
                    await FlagUserAlreadyExists(firebaseId);

                    // Add a new user
                    var user = form.Map(firebaseId, userEmail);
                    tContext.Add(user);

                    await tContext.SaveChangesAsync();

                    userId = user.Id;
                });

            await FirebaseAddInitialClaimsAsync(request, userId); // Store the user role in their firebase token claims. This should be done after the transaction is committed to ensure that the user was successfully created.
        }

        /// <summary>
        ///     Add the following claims to the user's JWT Token:
        ///     * UserId -> Used to identify the user in our database.
        ///     * WOW role -> All new users are granted this role after creating their profile.
        /// </summary>
        private async Task FirebaseAddInitialClaimsAsync(HttpRequest request, int userId)
        {
            var customClaims = new UserCustomClaims(userId, RoleData.USER.Name);
            await FirebaseHandler.SetUserClaims(request, customClaims);
        }

        private async Task FlagUserAlreadyExists(string firebaseId)
        {
            await Queries
                .GetUserByFirebaseId(firebaseId)
                .AnyAsync()
                .FailIfTrueAsync("Profile already exists.");
        }

        //public async Task UpdateProfile(UpdateProfileForm profile, int userId)
        //{
        //    await ExecuteWithTransaction(async (tContext) =>
        //        {
        //            var queries = new ProfileQueries() { Context = tContext };

        //            var user = await queries
        //            .GetUserById(userId)
        //            .Include(u => u.Person!.Identification)
        //            .FirstOrDefaultAsync()
        //            .FailIfNullAsync(ErrorStrings.NotFound("User"));

        //            await FlagPhoneNumberAlreadyExistsDuringEdit(profile, user!.Id);

        //            profile.Map(user);

        //            if (profile.IdentificationFile != null) // The user uploaded a new identification file.
        //            {
        //                await UploadIdentification(profile.IdentificationFile, tContext, user);
        //            }

        //            await tContext.SaveChangesAsync();
        //        });
        //}
    }
}