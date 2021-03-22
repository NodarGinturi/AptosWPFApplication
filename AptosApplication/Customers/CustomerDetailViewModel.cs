using Aptos.Data;
using AptosApplication.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AptosApplication.Customers
{
    public class CustomerDetailViewModel : BindableBase
    {

        public CustomerDetailViewModel()
        {
            SaveCustomer = new RelayCommand<Customer>(SaveCustomerInformation);
            Cancel = new RelayCommand<Customer>(CancelWindow);
        }

        private ICustomerRepository _repository = new CustomersRepository();
        private bool _EditMode;

        public bool EditMode
        {
            get { return _EditMode; }
            set { SetProperty(ref _EditMode, value); }
        }

        private Customer _editingCustomer = null;

        public event Action CancelDelegate = delegate { };
        public event Action SaveCustomerDelegate = delegate { };

        private SimpleEditableCustomer _customer;

        public SimpleEditableCustomer ACustomer
        {
            get { return _customer; }
            set { SetProperty(ref _customer, value); }
        }

        public void SetCustomer(Customer customer)
        {
            _editingCustomer = customer;
            ACustomer = new SimpleEditableCustomer();
            CopyCustomer(customer, ACustomer);

            var imagesFolderPath = AppDomain.CurrentDomain.BaseDirectory + @"Images\" + customer.Id;
            if (Directory.Exists(imagesFolderPath))
            {
                var imagePaths = Directory.GetFiles(imagesFolderPath, "*")
                                     .Select(Path.GetFullPath);
                ACustomer.Images.Clear();
                foreach (var path in imagePaths)
                {
                    ACustomer.Images.Add(new BitmapImage(new Uri(path)));
                }
            }
        }

        public RelayCommand<Customer> SaveCustomer { get; set; }
        public RelayCommand<Customer> Cancel { get;  set; }

        private void CopyCustomer(Customer source, SimpleEditableCustomer target)
        {
            target.Id = source.Id;
            if (EditMode)
            {
                target.FirstName = source.FirstName;
                target.LastName = source.LastName;
                target.Gender = source.Gender;
                target.BirthDateDay = source.BirthDateDay;
                target.BirthDateMonth = source.BirthDateMonth;
                target.BirthDateYear = source.BirthDateYear;
                target.BirthDate = source.BirthDate;
                target.Comment = source.Comment;
            }
        }

        private void SaveCustomerInformation(Customer customer)
        {
            Customer my = new Customer()
            {
                BirthDate = new DateTime(ACustomer.BirthDateYear, ACustomer.BirthDateMonth, ACustomer.BirthDateDay).ToShortDateString(),
                FirstName = ACustomer.FirstName,
                LastName = ACustomer.LastName,
                Gender = ACustomer.Gender,
                Id = ACustomer.Id,
                BirthDateDay = ACustomer.BirthDateDay,
                BirthDateMonth = ACustomer.BirthDateMonth,
                BirthDateYear = ACustomer.BirthDateYear,
                Comment = ACustomer.Comment
            };

            if (_EditMode)
            {
                _repository.UpdateCustomerAsync(my);
            }
            else
            {
                my.Id = Guid.NewGuid();
                _repository.AddCustomerAsync(my);

            }

            foreach (var image in ACustomer.Images)
            {
                var filePath = ((BitmapImage)image).UriSource.AbsolutePath;
                var filename = Path.GetFileName(filePath);
                var destFolderPath = AppDomain.CurrentDomain.BaseDirectory + @"Images\" + my.Id;
                if (!Directory.Exists(destFolderPath))
                {
                    Directory.CreateDirectory(destFolderPath);
                }
                var destFilePath = Path.Combine(destFolderPath, filename);
                if (!File.Exists(destFilePath))
                {
                    File.Copy(filePath, destFilePath);
                }
            }
            
            SaveCustomerDelegate();
        }

        private void CancelWindow(Customer customer)
        {
            CancelDelegate();
        }
    }
}
