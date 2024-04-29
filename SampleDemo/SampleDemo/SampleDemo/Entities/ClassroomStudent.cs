using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleDemo.Entities
{
    public class ClassroomStudent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClassroomStudentId { get; set; }

        [ForeignKey("Classroom")]
        public int? ClassroomId { get; set; }
        public virtual Classroom Classroom { get; set; }

        [ForeignKey("Student")]
        public int? StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}