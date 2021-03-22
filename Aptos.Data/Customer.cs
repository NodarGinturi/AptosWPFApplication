using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aptos.Data
{
    public class Customer : INotifyPropertyChanged
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int BirthDateYear { get; set; }
        public int BirthDateMonth { get; set; }
        public int BirthDateDay { get; set; }
        public string BirthDate { get; set; }
        public string Comment { get; set; }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
