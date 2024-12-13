using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.Student
{
    public class EntStudent
    {
        public int IdStudent { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public char Genero { get; set; }
        public DateTime Date { get; set; }
    }
}
