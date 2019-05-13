using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BankAppModel;
using Microsoft.EntityFrameworkCore;

namespace SomeUI
{
    class Program
    {
        static void Main(string[] args)
        {
            int pageNumber = 1;
            int linesPerPage = 20;

            Console.WriteLine("Sök efter namn:");
            var text = Console.ReadLine();

            do
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                var request = new GetCustomerWithNameRequest
                {
                    search = text,
                    limit = linesPerPage,
                    offset = linesPerPage * (pageNumber - 1)
                };
                var response = GetCustomerWithNameHandler(request);
                stopWatch.Stop();

                Console.WriteLine($"Exekveringstid: {stopWatch.ElapsedMilliseconds}ms");

                Console.WriteLine($"Sida:{pageNumber} Totalt antal: {response.Total} Antal Sidor:{response.NumberOfPages}");
                foreach (var customer in response.Customers)
                {
                    //var numberOfAccounts = GetAccounts(customer.Id);
                    Console.WriteLine($"{customer.Id} {customer.Givenname} {customer.Surname} {customer.City} ");
                }

                if (!response.HasMorePages) break;

                Console.WriteLine("Tryck Enter för nästa sida!");
                pageNumber = pageNumber + 1;
                Console.ReadLine();

            } while (true);



        }

        private static int GetAccounts(int id)
        {
            using (var context = new BankAppDataContext())
            {
                //                return context.Customers.Single(c => c.CustomerId == id).Dispositions.Count();
                return context.Dispositions.Where(d => d.CustomerId == id).Count();

            }
        }

        //public static IList<Customer> GetAllCustomers()
        //{
        //    using (var context = new BankAppDataContext())
        //    {
        //        var query = context.Customers;

        //        return query.ToList();
        //    }
        //}

        //public static IList<Customer> GetCustomerWithName(string search, int limit = 25, int offset = 0)
        //{
        //    using (var context = new BankAppDataContext())
        //    {
        //        var query = context.Customers.Where(c => c.Givenname.StartsWith(search));

        //        return query.Skip(offset).Take(limit).ToList();
        //    }
        //}

        //public static (int Total, IList<Customer> Customers) GetCustomerWithName(string search, int limit = 25, int offset = 0)
        //{
        //    using (var context = new BankAppDataContext())
        //    {
        //        var query = context.Customers.Where(c => c.Givenname.StartsWith(search));

        //        return (Total: query.Count(), Customers: query.AsNoTracking().Skip(offset).Take(limit).ToList());
        //    }
        //}

        public class GetCustomerWithNameRequest
        {
            public string search;
            public int limit = 25;
            public int offset = 0;
        }

        public class GetCustomerWithNameResponse
        {

            public class CustomerListViewModel
            {
                public int Id;
                public string Givenname;
                public string Surname;
                public string City;
                public int NumberOfAccounts;
            }

            public int Total;
            public int NumberOfPages;
            public bool HasMorePages;
            public IList<CustomerListViewModel> Customers;
        }

        public static GetCustomerWithNameResponse GetCustomerWithNameHandler(GetCustomerWithNameRequest request)
        {
            var response = new GetCustomerWithNameResponse();

            using (var context = new BankAppDataContext())
            {
                var query = context.Customers.Where(c => c.Givenname.StartsWith(request.search));

                response.Total = query.Count();
                response.NumberOfPages = response.Total / request.limit + 1;
                response.HasMorePages = request.offset + request.limit < response.Total;
                response.Customers = query
                    .AsNoTracking()
                    .Skip(request.offset)
                    .Take(request.limit)
                    .Select(c => new GetCustomerWithNameResponse.CustomerListViewModel
                    {
                        Id = c.CustomerId,
                        Givenname = c.Givenname,
                        Surname = c.Surname,
                        City = c.City
                    })
                    .ToList();
            }

            return response;
        }



        // Projection
    }
}
