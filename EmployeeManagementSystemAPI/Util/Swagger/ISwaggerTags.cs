namespace EmployeeManagementSystemAPI.Util.Swagger
{
    public interface ISwaggerTags
    {
        List<string> TagNames { get; }

        string DeveloperTag { get; }
    }
}