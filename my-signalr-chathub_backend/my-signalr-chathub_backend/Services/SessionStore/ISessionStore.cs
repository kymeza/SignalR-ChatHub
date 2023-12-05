namespace my_signalr_chathub_backend.Services.SessionStore
{
    public interface ISessionStore
    {
        void Store(string sessionId, string jwtToken, DateTimeOffset? expiration);
        string Retrieve(string sessionId);
        void Remove(string sessionId);
        void Update(string sessionId, string jwtToken, DateTimeOffset? expiration);
    }
}
