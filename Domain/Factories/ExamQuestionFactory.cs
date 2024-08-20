using E_Learning_Platform_API.Domain.Entities;

namespace E_Learning_Platform_API.Domain.Factories
{
    public class ExamQuestionFactory
    {
        public static ExamQuestion CreateExamQuestion(int examId, int questionId)
        {
            ExamQuestion question = new ExamQuestion()
            {
                ExamId = examId,
                QuestionId = questionId
            };
            return question;
        }
    }
}
