using Aptos.Data;
using AptosApplication.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AptosApplication.Customers
{
    public class CustomerListViewModel : BindableBase
    {
        private ICustomerRepository _repository = new CustomersRepository();
        private ObservableCollection<Customer> _customers;


        public CustomerListViewModel()
        {
            LoadCustomers();
            EditCommand = new RelayCommand<Customer>(OnEdit);
            AddCustomerCommand = new RelayCommand<Customer>(OnCreate);
        }
        public ObservableCollection<Customer> Customers { get { return _customers; } set { SetProperty(ref _customers, value); } }

        public RelayCommand<Customer> EditCommand { get; private set; }
        public RelayCommand<Customer> AddCustomerCommand { get; private set; }


        public event Action<Customer> AddCustomerRequested = delegate { };
        public event Action<Customer> EditCustomerRequested = delegate { };

        public async void LoadCustomers()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;

            Customers = new ObservableCollection<Customer>(await _repository.GetCustomersAsync());

        }

        private void OnEdit(Customer customer)
        {
            EditCustomerRequested(customer);
        }

        private void OnCreate(Customer customer)
        {
            AddCustomerRequested(new Customer { Id = Guid.NewGuid() });
        }
    }
}
