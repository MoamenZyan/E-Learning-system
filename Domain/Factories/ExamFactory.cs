using Ganss.Xss;
using Microsoft.Extensions.Primitives;
using E_Learning_Platform_API.Domain.Entities;

namespace E_Learning_Platform_API.Domain.Factories
{
    public class ExamFactory
    {
        static public HtmlSanitizer sanitizer = new HtmlSanitizer();
        public static Exam CreateExam(IFormCollection body, int courseId)
        {
            Exam exam = new Exam()
            {
                Name = sanitizer.Sanitize(body["Name"]!),
                Duration = new TimeSpan(Convert.ToInt32(body["Minutes"]) / 60, 0, 0),
                CourseId = courseId
            };
            return exam;
        }

        public static ExamDto CreateExamDto(Exam exam)
        {
            return new ExamDto(exam);
        }

        public static ExamMinimalDto CreateExamMinimalDto(Exam exam)
        {
            return new ExamMinimalDto(exam);
        }
    }
}
