using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    public interface IBook
    {
        void AddBook();
        void UpdateBook();
        void DeleteBook();
        void ViewBookByAuthorName();
        void IssueBook();
        void ReturnBook();
    }
}
