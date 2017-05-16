using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL
{

    //https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
    class Program
    {
        static void Main(string[] args)
        {
            FirstExecute();

            //SecondExecute();

            //TransactionExample();

            //IEnumerableIQueryableExample();

            Console.WriteLine("Done");
            Console.ReadKey(true);
        }

        private static void FirstExecute()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var bart = new Student()
                {
                    Name = "Bart",
                    Surname = "Simpson"
                };
                var lisa = new Student()
                {
                    Name = "Lisa",
                    Surname = "Simpson"
                };
                unitOfWork.StudetRepository.InsertStudent(bart);
                unitOfWork.StudetRepository.InsertStudent(lisa);
                unitOfWork.Save();
            }
        }

        private static void SecondExecute()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var milhouse = new Student()
                {
                    Name = "Milhouse",
                    Surname = "Van Houten"
                };

                unitOfWork.StudetRepository.InsertStudent(milhouse);

                var lisa = unitOfWork.StudetRepository.GetStudents().FirstOrDefault(s => s.Name == "Lisa");
                lisa.Surname = "Van Houten";
                unitOfWork.StudetRepository.UpdateStudent(lisa);

                var bart = unitOfWork.StudetRepository.GetStudents().FirstOrDefault(s => s.Name == "Bart");
                unitOfWork.StudetRepository.DeleteStudent(bart.Id);

                var students = unitOfWork.StudetRepository.GetStudents().ToList();
                ConsoleWriteStudents(students);
                unitOfWork.Save();
            }
        }

        private static async void TransactionExample()
        {
            using (var context = new UniversityDbContext())
            {
                using (var transaction = await context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var student = new Student()
                        {
                            Name = "Valera",
                            Surname = "Valera"
                        };
                        context.Students.Add(student);
                        context.SaveChanges();
                        //throw new Exception();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

        private static void IEnumerableIQueryableExample()
        {
            using (var context = new UniversityDbContext())
            {
                IEnumerable<Student> students = context.Students;
                var student = students.Where(s => s.Id > 3).ToList();
            }

            using (var context = new UniversityDbContext())
            {
                IQueryable<Student> students = context.Students;
                var student = students.Where(s => s.Id > 3).ToList();
            }
        }

        private static void ConsoleWriteStudents(List<Student> students)
        {
            foreach (var student in students)
            {
                Console.WriteLine($"{student.Name} {student.Surname}");
            }
        }
    }
}
