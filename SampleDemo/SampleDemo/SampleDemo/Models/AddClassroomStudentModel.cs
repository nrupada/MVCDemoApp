using System.ComponentModel.DataAnnotations;

namespace SampleDemo.Models
{
    public class AddClassroomStudentModel
    {
        public int ClassroomId { get; set; }

        public string ClassRoomName { get; set; }

        [Required]
        [Display(Name = "Student Name")]
        public string StudentName { get; set; }
    }
}