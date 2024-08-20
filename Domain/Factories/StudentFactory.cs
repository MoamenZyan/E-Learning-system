using Microsoft.Extensions.Primitives;
using Ganss.Xss;
using BCrypt.Net;
using E_Learning_Platform_API.Domain.Entities;
namespace E_Learning_Platform_API.Domain.Factories
{
    public class StudentFactory
    {
        static HtmlSanitizer sanitizer = new HtmlSanitizer();
        public static Student CreateStudent(Dictionary<string, StringValues> body)
        {
            Student student = new Student
            {
                Fname = sanitizer.Sanitize(body["Fname"]!),
                Lname = sanitizer.Sanitize(body["Lname"]!),
                Password = BCrypt.Net.BCrypt.HashPassword(body["Password"]!),
                Email = sanitizer.Sanitize(body["Email"]!),
                Phone = sanitizer.Sanitize(body["Phone"]!)
            };
            return student;
        }

        public static StudentDto CreateStudentDto(Student student)
        {
            return new StudentDto(student);
        }

        public static StudentMinimalDto CreateStudentMinimalDto(Student student)
        {
            return new StudentMinimalDto(student);
        }
    }
}
