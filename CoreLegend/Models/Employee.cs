using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLegend.Models
{
    public class Employee
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public long ContactNo { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public long Postcode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }    

    public class DTO
    {
        public List<Employee> EmpList { get; set; }
    }

    public interface IProcessEmployee
    {
        int AddEmployee(Employee employee);
        bool UpdateEmployee(Employee employee);
        bool DeleteEmployee(Employee employee);
    }
    public class EmployeeRecord : IProcessEmployee
    {
        public int AddEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public bool UpdateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
