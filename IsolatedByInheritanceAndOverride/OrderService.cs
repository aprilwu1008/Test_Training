using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IsolatedByInheritanceAndOverride
{
    public interface IBookDao
    {
        void Insert(Order order);
    }

    /// <summary>
    /// not complete yet
    /// </summary>
    public class BookDao : IBookDao
    {
        public void Insert(Order order)
        {
            throw new NotImplementedException();
        }
    }

    public class Order
    {
        public string CustomerName { get; set; }
        public int Price { get; set; }
        public string ProductName { get; set; }
        public string Type { get; set; }
    }

    public class OrderService
    {
        private IBookDao _bookDao;

        /// <summary>
        /// The file path
        /// </summary>
        private string _filePath = @"C:\temp\testOrders.csv";

        public OrderService()
        {
            _bookDao = new BookDao();
        }

        public OrderService(IBookDao bookDao)
        {
            _bookDao = bookDao;
        }

        public void SyncBookOrders()
        {
            var orders = this.GetOrders();

            // only get orders of book
            var ordersOfBook = orders.Where(x => x.Type == "Book");

            var bookDao = _bookDao;
            foreach (var order in ordersOfBook)
            {
                bookDao.Insert(order);
            }
        }

        internal virtual List<Order> GetOrders()
        {
            // parse csv file to get orders
            var result = new List<Order>();

            // directly depend on File I/O
            using (StreamReader sr = new StreamReader(this._filePath, Encoding.UTF8))
            {
                int rowCount = 0;

                while (sr.Peek() > -1)
                {
                    rowCount++;

                    var content = sr.ReadLine();

                    // Skip CSV header line
                    if (rowCount > 1)
                    {
                        string[] line = content.Trim().Split(',');

                        result.Add(this.Mapping(line));
                    }
                }
            }

            return result;
        }

        private Order Mapping(string[] line)
        {
            var result = new Order
            {
                ProductName = line[0],
                Type = line[1],
                Price = Convert.ToInt32(line[2]),
                CustomerName = line[3]
            };

            return result;
        }
    }
}