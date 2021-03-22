using Aptos.Data;
using AptosApplication.Customers;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptosApplication
{
    public class MainWindowViewModel : BindableBase
    {
        private CustomerListViewModel _customerListViewModel = new CustomerListViewModel();
        private CustomerDetailViewModel _addEditViewModel = new CustomerDetailViewModel();
        private string folderPath = Path.GetDirectoryName(Directory.GetCurrentDirectory());
        public MainWindowViewModel()
        {
            NavCommand = new RelayCommand<string>(OnNav);
            _customerListViewModel.AddCustomerRequested += NavToAddCustomer;
            _customerListViewModel.EditCustomerRequested += NavToEditCustomer;
            _addEditViewModel.SaveCustomerDelegate += AddCustomer;
            _addEditViewModel.CancelDelegate += Cancel;
        }
        private BindableBase _CurrentViewModel;

        public BindableBase CurrentViewModel 
        {
            get { return _CurrentViewModel; } 
            set { SetProperty(ref _CurrentViewModel, value); }
        }

        public RelayCommand<string> NavCommand { get; private set; }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "addCustomer":
                    CurrentViewModel = _addEditViewModel;
                    break;
                case "editCustomer":
                    CurrentViewModel = _addEditViewModel;
                    break;
                case "customers":
                    CurrentViewModel = _customerListViewModel;
                    break;
            }
        }

        private void NavToAddCustomer(Customer customer)
        {
            _addEditViewModel.EditMode = false;
            _addEditViewModel.SetCustomer(customer);
            CurrentViewModel = _addEditViewModel;
        }

        private void NavToEditCustomer(Customer customer)
        {
            _addEditViewModel.EditMode = true;
            _addEditViewModel.SetCustomer(customer);
            CurrentViewModel = _addEditViewModel;
        }

        private void AddCustomer()
        {
            _customerListViewModel.LoadCustomers();
            CurrentViewModel = _customerListViewModel;
        }

        private void Cancel()
        {
            CurrentViewModel = _customerListViewModel;
        }

    }
}
