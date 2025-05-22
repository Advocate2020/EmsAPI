namespace WowBL.Logic.FirebaseNS
{
    public class UserCustomClaims
    {
        public UserCustomClaims(int userId, string role)
        {
            UserId = userId;

            // Get and store the role names.
            Role = role;
        }

        /// <summary>
        ///     The roles that the user has.
        /// </summary>
        public string Role { get; }

        public int UserId { get; }
    }
}