// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories;
using DAL.Repositories.Interfaces;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly ApplicationDbContext _context;

        ICustomerRepository _customers;
        IProductRepository _products;
        IOrdersRepository _orders;
        ISiteRepository _sites;
        IJobApplicationRepository _jobApplication;



        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }



        public ICustomerRepository Customers
        {
            get
            {
                if (_customers == null)
                    _customers = new CustomerRepository(_context);

                return _customers;
            }
        }



        public IProductRepository Products
        {
            get
            {
                if (_products == null)
                    _products = new ProductRepository(_context);

                return _products;
            }
        }



        public IOrdersRepository Orders
        {
            get
            {
                if (_orders == null)
                    _orders = new OrdersRepository(_context);

                return _orders;
            }
        }


        public ISiteRepository Sites
        {
            get
            {
                if (_sites == null)
                    _sites = new SiteRepository(_context);

                return _sites;
            }
        }

        public IJobApplicationRepository JobApplication
        {
            get
            {
                if (_jobApplication == null)
                    _jobApplication = new JobApplicationRepository(_context);

                return _jobApplication;
            }
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
