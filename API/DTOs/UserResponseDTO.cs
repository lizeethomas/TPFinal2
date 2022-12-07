using TPFinalStrasbourg.Models;

namespace TPFinalStrasbourg.DTOs
{
    public class UserResponseDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserRole { get; set; }
        public bool Status { get; set; }
    }
}
