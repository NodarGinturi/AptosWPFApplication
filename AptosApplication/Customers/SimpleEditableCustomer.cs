using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AptosApplication.Customers
{
    public class SimpleEditableCustomer : ValidatableBindableBase
    {
        private Guid _id;
        private string _firstName;
        private string _lastName;
        private int _birthDateDay;
        private int _birthDateYear;
        private int _birthDateMonth;
        private string _comment;
        private string _gender;
        private string _birthDate;
        private ObservableCollection<ImageSource> _images;

        public SimpleEditableCustomer()
        {
            _images = new ObservableCollection<ImageSource>();
        }

        public Guid Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
        [Required]
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }
        [Required]
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }
        [Required]
        public int BirthDateDay
        {
            get { return _birthDateDay; }
            set { SetProperty(ref _birthDateDay, value); }
        }
        [Required]
        public int BirthDateMonth
        {
            get { return _birthDateMonth; }
            set { SetProperty(ref _birthDateMonth, value); }
        }
        [Required]
        public int BirthDateYear
        {
            get { return _birthDateYear; }
            set { SetProperty(ref _birthDateYear, value); }
        }
        [Required]
        public string Comment
        {
            get { return _comment; }
            set { SetProperty(ref _comment, value); }
        }
        [Required]
        public string Gender
        {
            get { return _gender; }
            set { SetProperty(ref _gender, value); }
        }

        public string BirthDate
        {
            get { return _birthDate; }
            set { SetProperty(ref _birthDate, value); }
        }

        public ObservableCollection<ImageSource> Images
        {
            get { return _images; }
            set { SetProperty(ref _images, value); }
        }
    }
}
