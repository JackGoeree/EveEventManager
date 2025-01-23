using System.ComponentModel.DataAnnotations;

namespace EveFrontend.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(20, ErrorMessage = "Name cannot exceed 20 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(100, ErrorMessage = "Description cannot exceed 100 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(20, ErrorMessage = "Location cannot exceed 20 characters.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Max Attendees is required.")]
        [Range(1, 1000, ErrorMessage = "Max Attendees must be between 1 and 1000.")]
        public int MaxAttendees { get; set; }

        public List<string> Attendees { get; set; }
    }
}

