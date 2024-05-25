using System.ComponentModel.DataAnnotations;

namespace MusicPortalWebAPIClient.Models
{
    public class SongViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }  
        public string? Singer { get; set; }
        public string? Genre { get; set; }
        public string? User { get; set; }
    }
}
