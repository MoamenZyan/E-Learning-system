namespace E_Learning_Platform_API.Domain.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public required string Fname { get; set; }
        public required string Lname { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Phone { get; set; }

        public IEnumerable<StudentCourse> EnrolledCourses { get; set; } = new List<StudentCourse>();
        public IEnumerable<Certification> OwnedCertifications { get; set; } = new List<Certification>();
    }


    public class StudentDto
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }


        public IEnumerable<StudentCourse> EnrolledCourses { get; set; } = new List<StudentCourse>();
        public IEnumerable<Certification> OwnedCertifications { get; set; } = new List<Certification>();
        public StudentDto(Student student)
        {
            Id = student.Id;
            Fname = student.Fname;
            Lname = student.Lname;
            Email = student.Email;
            Phone = student.Phone;

            EnrolledCourses = student.EnrolledCourses;
            OwnedCertifications = student.OwnedCertifications;
        }
    }

    public class StudentMinimalDto
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public StudentMinimalDto(Student student)
        {
            Id = student.Id;
            Fname = student.Fname;
            Lname = student.Lname;
            Email = student.Email;
            Phone = student.Phone;
        }
    }
}