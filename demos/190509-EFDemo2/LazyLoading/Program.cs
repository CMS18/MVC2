using System;
using System.Diagnostics;
using System.Linq;
using BankAppModel;
using Microsoft.EntityFrameworkCore;

namespace LazyLoading
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new BankAppDataContext())
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                //var kunder = context.Customers.Take(1000).ToList();
                var kunder = context.Customers
                    .Include(c => c.Dispositions)
                    .ThenInclude(d => d.Account)
                    .Take(1000).ToList();

                foreach (var kund in kunder)
                {
                    Console.WriteLine($"{kund.CustomerId} {kund.Givenname} {kund.Surname}");

                    foreach (var disposition in kund.Dispositions)
                    {
                        Console.WriteLine($"{disposition.AccountId} {disposition.Account.Created} {disposition.Account.Frequency}");
                    }
                }

                stopWatch.Stop();
                Console.WriteLine($"Tid: {stopWatch.Elapsed}ms");
            }
        }
    }
}
