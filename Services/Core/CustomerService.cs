using System.Linq;
using PrimeCapitalBank.Models;

namespace PrimeCapitalBank.Services;

public class CustomerService
{
    private readonly List<Customer> _customers;
    public CustomerService(List<Customer> customers)
    {
        _customers = customers;
    }
    public Customer? FindCustomerById(string idNumber)
    {
        return _customers.FirstOrDefault(c => c.IdNumber == idNumber);
    }
}