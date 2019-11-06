using System;
using System.Collections.Generic;
using SQLite;
using VechcileTracking.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VechcileTracking.Views
{
    public partial class PendingAmount : ContentPage
    {
        SQLiteAsyncConnection connection;
        Command CallCommand;
        public PendingAmount()
        {
            InitializeComponent();

            connection = DependencyService.Get<ISQLiteDb>().GetConnection();

            GetData();

           CallCommand = new Command<PendingList>((p) =>
           {
               PhoneDialer.Open(p.Customer.PhoneNo);
           });
        }

        private async void GetData()
        {            
            var paymentList = await connection.Table<PaymentInfo>().ToListAsync();

            var pendingList = new List<PendingList>();
            var totalPendingAmount = 0;
            foreach(var payment in paymentList)
            {
                var customer = await connection.Table<Customer>().Where(c => c.Id == payment.CustomerId).FirstOrDefaultAsync();
                var amount = payment.TotalAmount - payment.Settled;
                totalPendingAmount += amount;
                var pending = new PendingList
                {
                    Customer = customer,
                    Amount = amount,
                    Command = CallCommand
                };
                pendingList.Add(pending);
            }

            TotalPendingAmount.Text = "Total Pending Amount Rs."+totalPendingAmount;
            PendingListView.ItemsSource = pendingList;
        }

        class PendingList
        {
            public Customer Customer { get; set; }

            public int Amount { get; set; }

            public Command Command { get; set; }
        }
    }
}
