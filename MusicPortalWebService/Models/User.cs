using System.ComponentModel.DataAnnotations;

namespace MusicPortalWebAPIClient.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Login { get; set; }
        public string? Email { get; set; }        
        virtual public ICollection<Song>? Songs { get; set; }
    }
}
