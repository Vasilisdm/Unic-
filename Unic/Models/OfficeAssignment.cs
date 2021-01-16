using System.ComponentModel.DataAnnotations;

namespace Unic.Models
{
    public class OfficeAssignment
    {
        public int ID { get; set; }

        [StringLength(50)]
        [Display(Name = "Office Location")]
        public string Location { get; set; }

        public Instructor Instructor { get; set; }
    }
}
