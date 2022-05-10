using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillbox_7
{
    
    public record struct Worker
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Growth { get; set; }
        public string DateOfBirth { get; set; }
        public string NameOfBirth { get; set; }     
        #region Конструктор
        public Worker(int ID, DateTime Date, string Name, int Age, int Growth, string DateOfBirth, string NameOfBirth)
        {
            this.ID = ID;
            this.Date = Date;
            this.Name = Name;
            this.Age = Age;
            this.Growth = Growth;
            this.DateOfBirth=DateOfBirth;
            this.NameOfBirth = NameOfBirth;
        }
        #endregion
    }
}
