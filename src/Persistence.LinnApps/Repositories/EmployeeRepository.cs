﻿namespace Linn.Production.Persistence.LinnApps.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Production.Domain.LinnApps.ViewModels;

    public class EmployeeRepository : IRepository<Employee, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public EmployeeRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public Employee FindById(int key)
        {
            return this.serviceDbContext.Employees.Where(e => e.Id == key).ToList().FirstOrDefault();
        }

        public IQueryable<Employee> FindAll()
        {
            return this.serviceDbContext.Employees.Where(e => e.DateInvalid == null).OrderBy(e => e.FullName);
        }

        public void Add(Employee entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Employee entity)
        {
            throw new NotImplementedException();
        }

        public Employee FindBy(Expression<Func<Employee, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Employee> FilterBy(Expression<Func<Employee, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}