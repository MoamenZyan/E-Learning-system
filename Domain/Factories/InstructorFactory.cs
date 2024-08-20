using Ganss.Xss;
using Microsoft.Extensions.Primitives;
using E_Learning_Platform_API.Domain.Entities;
namespace E_Learning_Platform_API.Domain.Factories
{
    public class InstructorFactory
    {
        static HtmlSanitizer sanitizer = new HtmlSanitizer();
        public static Instructor CreateInstructor(Dictionary<string, StringValues> body)
        {
            Instructor instructor = new Instructor()
            {
                Fname = sanitizer.Sanitize(body["Fname"]!),
                Lname = sanitizer.Sanitize(body["Lname"]!),
                Password = BCrypt.Net.BCrypt.HashPassword(body["Password"]!),
                Email = sanitizer.Sanitize(body["Email"]!),
                Phone = sanitizer.Sanitize(body["Phone"]!)
            };
            return instructor;
        }

        public static InstructorDto CreateInstructorDto(Instructor instructor)
        {
            return new InstructorDto(instructor);
        }

        public static InstructorMinimalDto CreateInstructorMinimalDto(Instructor instructor)
        {
            return new InstructorMinimalDto(instructor);
        }

    }
}
