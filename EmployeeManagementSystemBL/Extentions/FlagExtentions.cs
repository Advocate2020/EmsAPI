namespace EmployeeManagementSystemBL.Extentions
{
    public static class FlagExtentions
    {
        public static async Task FailIfTrueAsync(this Task<bool> task, string message)
        {
            if (await task)
            {
                throw new UserClientError(message);
            }
        }

        public static async Task FailIfFalseAsync(this Task<bool> task, string message)
        {
            if (!(await task))
            {
                throw new UserClientError(message);
            }
        }

        public static async Task<T> FailIfNullAsync<T>(this Task<T> task, string message)
        {
            return (await task) ?? throw new UserClientError(message);
        }
    }
}