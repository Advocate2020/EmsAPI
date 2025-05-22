namespace EmployeeManagementSystemBL.Extentions
{
    public class UserClientError : Exception
    {
        public UserClientError(string message) : base(message)
        { }
    }
}