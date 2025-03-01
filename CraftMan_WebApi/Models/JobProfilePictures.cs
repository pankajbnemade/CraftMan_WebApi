using System.ComponentModel.DataAnnotations;

namespace CraftMan_WebApi.Models
{
    public class JobProfilePictures
    {
        [Key]
        public int JobPictureId { get; set; }
        public string TicketId { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
    }
}
