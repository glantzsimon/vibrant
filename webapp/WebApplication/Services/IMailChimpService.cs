namespace K9.WebApplication.Services
{
    public interface IMailChimpService
    {
        void AddClient(string firstName, string lastName, string emailAddress);
        void AddClient(string name, string emailAddress);
        void AddAllClients();
    }
}