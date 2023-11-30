namespace TTT.Client.User
{
    public class UserService : IUserService
    {
        public string UserName { get; private set; }

        public void RegisterUser(string userName)
        {
            UserName = userName;
        }
    }
}