using EmployeeManagementSystemAPI.Util.Swagger;

namespace BookXChangeApi.Util.Swagger
{
    public class EMSTags : ISwaggerTags
    {
        public const string Auth = "01.Auth";
        public const string Employee = "02.Book";
        public const string Dev = "03.Dev";

        /// <summary>
        /// All endpoints need to be listed under <see cref="TagNames"/>, in order for them to be sorted alphabetically.
        /// </summary>
        public List<string> TagNames => new()
        {
            Auth,
            Employee,
        };

        string ISwaggerTags.DeveloperTag => Dev;
    }
}