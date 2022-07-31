namespace Paybills.API.DTOs
{
    public class LoginResultDto
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }
    }
}