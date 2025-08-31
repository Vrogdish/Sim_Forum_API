namespace Sim_Forum.DTOs.Errors
{
    public class ErrorResponseDto
    {
        public int statusCode { get; set; }
        public string message { get; set; } = string.Empty;
    }
}
