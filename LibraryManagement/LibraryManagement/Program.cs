using System.Data.SqlClient;
using Spectre.Console;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AnsiConsole.Write(new FigletText("Library Management System App").Centered().Color(Color.Blue));
            LoginUser login = new LoginUser();
            Student student = new Student();
            Book book = new Book();
            bool IsLogin = login.UserLogin();
            while (IsLogin)
            {
                var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("[green]Select your choice :[/]")
                    .AddChoices(new[] {
                        "Add Student",
                        "Update Student By ID",
                        "Delete Student By ID",
                        "Search Student By ID",
                        "Students list who have books",
                        "Add Book",
                        "Update Book By ID",
                        "Delete Book By ID",
                        "Search Books By Author",
                        "Issue Book",
                        "Return Book"
                   }));
                switch (choice)
                {
                    case "Add Student":
                        {
                            student.AddStudent();
                            break;
                        }
                    case "Update Student By ID":
                        {
                            student.UpdateStudent();
                            break;
                        }
                    case "Delete Student By ID":
                        {
                            student.DeleteStudent();
                            break;
                        }
                    case "Search Student By ID":
                        {
                            student.SearchStudentByID();
                            break;
                        }
                    case "Students list who have books":
                        {
                            student.StudentsHavingBooks();
                            break;
                        }
                    case "Add Book":
                        {
                            book.AddBook();
                            break;
                        }
                    case "Update Book By ID":
                        {
                            book.UpdateBook();
                            break;
                        }
                    case "Delete Book By ID":
                        {
                            book.DeleteBook();
                            break;
                        }
                    case "Search Books By Author":
                        {
                            book.ViewBookByAuthorName();
                            break;
                        }
                    case "Issue Book":
                        {
                            book.IssueBook();
                            break;
                        }
                    case "Return Book":
                        {
                            book.ReturnBook();
                            break;
                        }
                }
            }
        }
    }
}