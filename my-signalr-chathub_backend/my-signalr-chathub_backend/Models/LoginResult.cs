namespace my_signalr_chathub_backend.Models
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public UserInfo UserInfo { get; set; } // Populate only the non-sensitive data.
    }

    public class UserInfo
    {
        // Include properties relevant to the user info you want to return
        public string Name { get; set; }
        public string[] Roles { get; set; }
        // Other properties as needed
    }
}
