using Ganss.Xss;
using E_Learning_Platform_API.Domain.Entities;

namespace E_Learning_Platform_API.Domain.Factories
{
    public class CertificationFactory
    {
        static HtmlSanitizer sanitizer = new HtmlSanitizer();
        public static Certification CreateCertification(int studentId, int courseId)
        {
            Certification certification = new Certification()
            {
                StudentId = studentId,
                CourseId = courseId,
                IssueDate = DateTime.Now,
            };
            return certification;
        }

        public static CertificationMinimalDto CreateCertificationMinimalDto(Certification certification)
        {
            return new CertificationMinimalDto(certification);
        }
    }
}
