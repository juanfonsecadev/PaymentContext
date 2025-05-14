using PaymentContext.Domain.ValueObjetcs;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Queries;
using PaymentContext.Domain.Enums;

namespace PaymentContext.Tests.Queries
{
    [TestClass]
    public class StudentQueriesTests
    {
        private IList<Student> _students;

        public StudentQueriesTests()
        {
            _students = new List<Student>();

            for (var i = 0; i <= 10; i++)
            {
                _students.Add(new Student(
                    new Name("Aluno", i.ToString()),
                    new Document("1111111111" + i, EDocumentType.CPF),
                    new Email($"{i}@email.com"))
                );
            }
        }

        [TestMethod]
        public void ShouldReturnNullWhenDocumentDoesNotExist()
        {
            var expression = StudentQueries.GetStudentInfo("12345678909");
            var student = _students.AsQueryable().Where(expression).FirstOrDefault();

            Assert.IsNull(student);
        }

        [TestMethod]
        public void ShouldReturnStudentWhenDocumentExists()
        {
            var existingDocument = "11111111110"; // corresponde ao aluno com i == 0
            var expression = StudentQueries.GetStudentInfo(existingDocument);
            var student = _students.AsQueryable().Where(expression).FirstOrDefault();

            Assert.IsNotNull(student);
            Assert.AreEqual(existingDocument, student.Document.Number);
        }
    }
}
