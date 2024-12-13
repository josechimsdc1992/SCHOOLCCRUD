using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Student
    {
        public int IdStudent { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public char Genero { get; set; }
        public DateTime Date {get;set;}

        public ICollection<EntGrade> Grades { get; set; }
    }
}
