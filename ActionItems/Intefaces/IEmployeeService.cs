using ActionItems.Models;

namespace ActionItems.Intefaces
{
    public interface IEmployeeService
    {
        public List<Employee> GetEmployees();
        public Employee AddEmployee(Employee employee);
    }
}
