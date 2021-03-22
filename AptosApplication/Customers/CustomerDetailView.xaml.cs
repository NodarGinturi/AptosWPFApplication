using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace AptosApplication.Customers
{
    public partial class CustomerDetailView : UserControl
    {
        public CustomerDetailView()
        {
            InitializeComponent();
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;
            int currentDay = DateTime.Now.Day;
            string[] genderArray = { "Male", "Female" };
            for (int i = 1950; i <= currentYear; i++)
            {
                ComboBoxItem itemYear = new ComboBoxItem();
                itemYear.Content = i;
                birthDateYearTextBox.Items.Add(itemYear);
            }

            for(int i = 1; i < 12; i++)
            {
                ComboBoxItem itemMonth = new ComboBoxItem();
                itemMonth.Content = i;
                birthDateMonthTextBox.Items.Add(itemMonth);
            }

            for(int i = 1; i <= 31; i++)
            {
                ComboBoxItem itemDay = new ComboBoxItem();
                itemDay.Content = i;
                birthDateDayTextBox.Items.Add(itemDay);
            }

            for(int i = 0; i < genderArray.Length; i++)
            {
                ComboBoxItem gender = new ComboBoxItem();
                gender.Content = genderArray[i];
                genderTextBox.Items.Add(gender);
            }

        }

        private void Thumbnails_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                List<string> imageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };
                foreach (var file in files)
                {
                    if (!imageExtensions.Contains(Path.GetExtension(file).ToUpper()))
                    {
                        continue;
                    }
                    var customerObj = (sender as ListBox).DataContext as SimpleEditableCustomer;

                    customerObj.Images.Add(new BitmapImage(new Uri(file)));
                }
            }
        }
    }
}
