using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    public interface IStudent
    {
        void AddStudent();
        void UpdateStudent();
        void SearchStudentByID();
        void StudentsHavingBooks();
    }
}
