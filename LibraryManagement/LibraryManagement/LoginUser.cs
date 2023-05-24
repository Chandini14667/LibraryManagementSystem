using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Spectre.Console;

namespace LibraryManagement
{
    public class LoginUser : ILoginUser
    {
        SqlConnection con = new SqlConnection("Server=IN-4W3K9S3; database=LibraryManagement; User Id=sa; password=Nani falls down@22!@nc");
        public bool UserLogin()
        {
            AnsiConsole.MarkupLine("[green]Please Login to see the Services:[/]");
            Console.WriteLine();
            string username = AnsiConsole.Ask<string>("[yellow]Enter User Name: [/]");
            string password = AnsiConsole.Ask<string>("[yellow]Enter Password: [/]");
            con.Open();
            SqlCommand cmd = new SqlCommand("select count(*) from LoginUser where UserName = @username and Password = @password", con);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            int count = (int)cmd.ExecuteScalar();
            if (count > 0)
            {
                AnsiConsole.MarkupLine("[green]Logged in successfully [/]");
                return true;
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Invalid username or password [/]");
                return false;
            }
            con.Close();
        }

        bool ILoginUser.LoginUser()
        {
            throw new NotImplementedException();
        }
    }
}
