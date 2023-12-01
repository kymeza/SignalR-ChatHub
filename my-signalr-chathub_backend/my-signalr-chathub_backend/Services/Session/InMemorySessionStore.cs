using System.Collections.Concurrent;

namespace my_signalr_chathub_backend.Services.Session
{
    public class InMemorySessionStore : ISessionStore
    {
        private readonly ConcurrentDictionary<string, (string, DateTimeOffset?)> _store = new();

        public void Store(string sessionId, string jwtToken, DateTimeOffset? expiration)
        {
            _store[sessionId] = (jwtToken, expiration);
        }

        public string Retrieve(string sessionId)
        {
            if (_store.TryGetValue(sessionId, out var sessionInfo))
            {
                // Check if the token is expired.
                if (sessionInfo.Item2 <= DateTimeOffset.UtcNow)
                {
                    Remove(sessionId);
                    return null;
                }
                return sessionInfo.Item1;
            }
            return null;
        }

        public void Remove(string sessionId)
        {
            _store.TryRemove(sessionId, out var _);
        }

        public void Update(string sessionId, string jwtToken, DateTimeOffset? expiration)
        {
            _store[sessionId] = (jwtToken, expiration);
        }
    }

}
