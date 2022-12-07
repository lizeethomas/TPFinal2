namespace BlazorFinalStrasbourg.DTOs
{
    public class AdResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public List<string> Urls { get; set; }
        public List<CommentResponseDTO> Comments { get; set; }
        public AdResponseDTO()
        {
            Comments = new();
        }
    }
}
