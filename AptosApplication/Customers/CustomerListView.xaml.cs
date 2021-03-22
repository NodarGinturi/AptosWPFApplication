using Aptos.Data;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AptosApplication.Customers
{
    /// <summary>
    /// Interaction logic for CustomerListView.xaml
    /// </summary>
    public partial class CustomerListView : UserControl
    {
        public CustomerListView()
        {
            InitializeComponent();
        }

        private static void OnLoadData(object sender, RoutedEventArgs e)
        {
            var a = new CustomerListViewModel();
            a.LoadCustomers();
        }

        private void DataGridTextColumn_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (customerDataGrid.SelectedItem == null) return;
            var selectedCustomer = customerDataGrid.SelectedItem as Customer;
            CustomerListViewModel customerListViewModel = new CustomerListViewModel();
            customerListViewModel.EditCommand.Execute(selectedCustomer);


        }
    }
}
