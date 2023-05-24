using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Spectre.Console;
using System.Collections;
using System.Data;

namespace LibraryManagement
{
    public class Book : IBook
    {
        SqlConnection con = new SqlConnection("Server=IN-4W3K9S3; database=LibraryManagement; User Id=sa; password=Nani falls down@22!@nc");
        public void AddBook()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand($"insert into Books(Title,Author,Quantity) values(@Title,@Author,@Quantity)", con);
            string title = AnsiConsole.Ask<string>("[yellow]Enter Book Title:[/]");
            string aname = AnsiConsole.Ask<string>("[yellow]Enter Author Name:[/]");
            int quantity = AnsiConsole.Ask<int>("[yellow]Enter Quantity of Books: [/]");
            cmd.Parameters.AddWithValue("@Title", title);
            cmd.Parameters.AddWithValue("@Author", aname);
            cmd.Parameters.AddWithValue("Quantity", quantity);
            cmd.ExecuteNonQuery();
            AnsiConsole.MarkupLine("[green]Book Details Added Sucessfully [/]");
            con.Close();
        }
        public void UpdateBook()
        {
            con.Open();
            int id = AnsiConsole.Ask<int>("[yellow]Enter the Book ID which you want to update:[/]");
            SqlCommand count = new SqlCommand($"SELECT COUNT(*) FROM Books where BookID = {id}", con);
            int Count = (int)count.ExecuteScalar();
            if (Count > 0)
            {
                SqlCommand cmd = new SqlCommand($"update Books set Title=@Title,Author=@Author,Quantity=@Quantity where BookID = {id}", con);
                string Title = AnsiConsole.Ask<string>("[yellow]Enter Updated Book Title:[/]");
                string Author = AnsiConsole.Ask<string>("[yellow]Enter Updated Author Name:[/]");
                int Quantity = AnsiConsole.Ask<int>("[yellow]Enter Updated Quantity: [/]");
                cmd.Parameters.AddWithValue("@Title", Title);
                cmd.Parameters.AddWithValue("@Author", Author);
                cmd.Parameters.AddWithValue("@Quantity", Quantity);
                cmd.ExecuteNonQuery();
                AnsiConsole.MarkupLine("[green]Book Updated Sucessfully [/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Enter correct ID[/]");
            }
            con.Close();
        }
        public void DeleteBook()
        {
            con.Open();
            int id = AnsiConsole.Ask<int>("[yellow]Enter the Book id which you want to Delete:[/]");
            SqlCommand count = new SqlCommand($"SELECT COUNT(*) FROM Books where BookID = {id}", con);
            int Count = (int)count.ExecuteScalar();
            if (Count > 0)
            {
                SqlCommand cmd = new SqlCommand($"delete from Books where BookID = {id}", con);
                cmd.ExecuteNonQuery();
                AnsiConsole.MarkupLine("[green]Book Deleted Sucessfully [/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Enter Correct ID[/]");
            }
            con.Close();
        }
        public void ViewBookByAuthorName()
        {
            con.Open();
            string author = AnsiConsole.Ask<string>("[yellow]Enter Author Name of the book which you Want to see: [/]");
            SqlCommand cmd = new SqlCommand($"select * from Books where Author = '{author}'", con);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Console.WriteLine($" {rd[0]} | {rd[1]} | {rd[2]} | {rd[3]} ");
            }
            con.Close();
        }
        public void IssueBook()
        {
            con.Open();
            int id = AnsiConsole.Ask<int>("[yellow]Enter Student ID: [/]");

            string studentQuery = $"select * from Students where StudentID = {id}";
            SqlDataAdapter studentAdp = new SqlDataAdapter(studentQuery, con);
            DataSet studentds = new DataSet();
            studentAdp.Fill(studentds);

            int studentCount = studentds.Tables[0].Rows.Count;
            if (studentCount == 0)
            {
                AnsiConsole.MarkupLine("[red]Student with the given ID does not exist![/]");
                return;
            }

            string issueQuery = $"select * from IssueBook where StudentID = {id}";
            SqlDataAdapter issueAdp = new SqlDataAdapter(issueQuery, con);
            DataSet issueds = new DataSet();
            issueAdp.Fill(issueds);

            int issueRecordCount = issueds.Tables[0].Rows.Count;
            if (issueRecordCount > 0)
            {
                AnsiConsole.MarkupLine("[red]Student has already taken a book! plz return the old one.[/]");
                return;
            }

            int bookId = AnsiConsole.Ask<int>("[yellow]Enter Book ID: [/]");

            string bookQuery = $"select * from Books where BookID = {bookId}";
            SqlDataAdapter bookAdp = new SqlDataAdapter(bookQuery, con);
            DataSet bookds = new DataSet();
            bookAdp.Fill(bookds);

            int bookRecordCount = bookds.Tables[0].Rows.Count;
            if (bookRecordCount == 0)
            {
                AnsiConsole.MarkupLine("[red]Book with the given ID does not exist![/]");
                return;
            }

            int quantity = (int)bookds.Tables[0].Rows[0]["Quantity"];
            if (quantity > 0)
            {
                bookds.Tables[0].Rows[0]["Quantity"] = quantity - 1;
                SqlCommandBuilder builder = new SqlCommandBuilder(bookAdp);
                bookAdp.Update(bookds);

                DateTime dateTime = DateTime.Now;
                var row = issueds.Tables[0].NewRow();
                row["StudentID"] = id;
                row["BookID"] = bookId;
                row["IssuedDate"] = dateTime.Date;
                issueds.Tables[0].Rows.Add(row);

                SqlCommandBuilder builder1 = new SqlCommandBuilder(issueAdp);
                issueAdp.Update(issueds);

                AnsiConsole.MarkupLine("[green]Book issued to the student![/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Book with the given ID is not available![/]");
                return;
            }
            con.Close();
        }
        public void ReturnBook()
        {
            con.Open();
            int id = AnsiConsole.Ask<int>("[yellow]Enter Student ID: [/]");
            string query = $"select * from IssueBook where StudentID = {id}";
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            int recordCount = ds.Tables[0].Rows.Count;
            if (recordCount == 0)
            {
                AnsiConsole.MarkupLine("[red]Student has not taken any book![/]");
                return;
            }

            int bookId = (int)ds.Tables[0].Rows[0]["BookID"];
            ds.Tables[0].Rows[0].Delete();

            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Update(ds);

            AnsiConsole.MarkupLine("[green]Book returned from student[/]");

            string bookQuery = $"select * from Books where BookID = {bookId}";
            SqlDataAdapter bookAdapter = new SqlDataAdapter(bookQuery, con);
            DataSet bookDataSet = new DataSet();
            bookAdapter.Fill(bookDataSet);

            int quantity = (int)bookDataSet.Tables[0].Rows[0]["Quantity"];
            bookDataSet.Tables[0].Rows[0]["Quantity"] = quantity + 1;

            SqlCommandBuilder builder1 = new SqlCommandBuilder(bookAdapter);
            bookAdapter.Update(bookDataSet);
            con.Close();
        }       

    }
}
