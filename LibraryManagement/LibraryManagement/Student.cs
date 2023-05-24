using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Spectre.Console;

namespace LibraryManagement
{
    public class Student : IStudent 
    {
        SqlConnection con = new SqlConnection("Server=IN-4W3K9S3; database=LibraryManagement; User Id=sa; password=Nani falls down@22!@nc");
        public void AddStudent()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand($"insert into Students values(@StudentName,@Department)", con);
            string name = AnsiConsole.Ask<string>("[yellow]Enter Student Name:[/]");
            string dept = AnsiConsole.Ask<string>("[yellow]Enter Student Department:[/]");
            cmd.Parameters.AddWithValue("@StudentName", name);
            cmd.Parameters.AddWithValue("@Department", dept);
            cmd.ExecuteNonQuery();
            AnsiConsole.MarkupLine("[green]Student Added Successfully!![/]");
            con.Close();
        }
        public void UpdateStudent()
        {
            con.Open();
            int id = AnsiConsole.Ask<int>("[yellow]Enter the Student ID which you want to update:[/]");
            SqlCommand count = new SqlCommand($"SELECT COUNT(*) FROM Students where StudentID = {id}", con);
            int Count = (int)count.ExecuteScalar();
            if (Count > 0)
            {
                SqlCommand cmd = new SqlCommand($"update Students set StudentName=@StudentName, Department=@Department where StudentID = {id}", con);
                string StudentName = AnsiConsole.Ask<string>("[yellow]Enter Updated Student Name:[/]");
                string Department = AnsiConsole.Ask<string>("[yellow]Enter Updated Student Department:[/]");
                cmd.Parameters.AddWithValue("@StudentName", StudentName);
                cmd.Parameters.AddWithValue("@Department", Department);
                cmd.ExecuteNonQuery();
                AnsiConsole.MarkupLine("[green]Student Details Updated Sucessfully [/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Enter correct ID[/]");
            }
            con.Close();
        }
        public void DeleteStudent()
        {
            con.Open();
            int id = AnsiConsole.Ask<int>("[yellow]Enter the Student id which you want to Delete:[/]");
            SqlCommand count = new SqlCommand($"SELECT COUNT(*) FROM Students where StudentID = {id}", con);
            int Count = (int)count.ExecuteScalar();
            if (Count > 0)
            {
                SqlCommand cmd = new SqlCommand($"delete from Students where StudentID = {id}", con);
                cmd.ExecuteNonQuery();
                AnsiConsole.MarkupLine("[green]Student Deleted Sucessfully [/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Enter Correct ID[/]");
            }
            con.Close();
        }
        public void SearchStudentByID()
        {
            con.Open();
            int id = AnsiConsole.Ask<int>("[yellow]Enter Student ID you want to view: [/]");
            SqlCommand cmd = new SqlCommand($"select * from  Students where StudentID = {id}", con);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Console.WriteLine($" {rd[0]} | {rd[1]} | {rd[2]} ");
            }
            con.Close();
        }
        public void StudentsHavingBooks()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand($"select Students.StudentID,Students.StudentName,Books.Title,Books.Author from IssueBook join Students on IssueBook.StudentID = Students.StudentID join Books on IssueBook.BookID = Books.BookID", con);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Console.WriteLine($" {rd[0]} | {rd[1]} | {rd[2]} | {rd[3]} ");
            }
            con.Close();
        }
    }
}
