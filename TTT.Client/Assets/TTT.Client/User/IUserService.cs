namespace TTT.Client.User
{
    public interface IUserService
    {
        string UserName { get; }
        
        void RegisterUser(string userName);
    }
}