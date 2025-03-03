namespace Server.DTOs

{
    public class UserRegistrationDTO : CommonDTO
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}