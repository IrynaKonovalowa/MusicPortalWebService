using System.ComponentModel.DataAnnotations;

namespace MusicPortalWebAPIClient.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string? Title { get; set; }  
        public string? Singer { get; set; }     
        public int GenreId {  get; set; }
        public int UserId { get; set; }
        virtual public Genre? Genre { get; set; }
        virtual public User? User { get; set; }
    }
}
