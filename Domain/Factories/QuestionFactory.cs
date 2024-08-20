using Ganss.Xss;
using Microsoft.Extensions.Primitives;
using E_Learning_Platform_API.Domain.Entities;

namespace E_Learning_Platform_API.Domain.Factories
{
    public class QuestionFactory
    {
        static HtmlSanitizer sanitizer = new HtmlSanitizer();

        public static Question CreateQuestion(IFormCollection body, int courseId)
        {
            Question question = new Question()
            {
                Description = sanitizer.Sanitize(body["Description"]!),
                Answer = sanitizer.Sanitize(body["Answer"]!),
                CourseId = Convert.ToInt16(courseId)
            };
            return question;
        }

        public static QuestionMinimalDto CreateQuestionMinimalDto(Question question)
        {
            return new QuestionMinimalDto(question);
        }
    }
}
