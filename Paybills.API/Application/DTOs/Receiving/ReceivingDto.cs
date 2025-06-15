namespace Paybills.API.Application.DTOs.Receiving
{
    public class ReceivingDto
    {
        public int Id { get; set; }
        public float Value { get; set; }
        
        public string[] UserName { get; set; }
    }
}