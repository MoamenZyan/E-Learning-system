namespace E_Learning_Platform_API.Domain.Entities
{
    public class Instructor
    {
        public int Id { get; set; }
        public required string Fname { get; set; }
        public required string Lname { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Phone { get; set; }


        public IEnumerable<Course> OwnedCourses { get; set; } = new List<Course>();
    }

    public class InstructorDto
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public IEnumerable<Course> OwnedCourses { get; set; } = new List<Course>();
        public InstructorDto(Instructor instructor)
        {
            Id = instructor.Id;
            Fname = instructor.Fname;
            Lname = instructor.Lname;
            Email = instructor.Email;
            Phone = instructor.Phone;

            OwnedCourses = instructor.OwnedCourses;
        }
    }


    public class InstructorMinimalDto
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public InstructorMinimalDto(Instructor instructor)
        {
            Id = instructor.Id;
            Fname = instructor.Fname;
            Lname = instructor.Lname;
            Email = instructor.Email;
            Phone = instructor.Phone;
        }
    }

}
