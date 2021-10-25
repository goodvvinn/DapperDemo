using System.ComponentModel.DataAnnotations;

namespace DapperDemo.Models
{
    public class Company
    {
        public int CompanyId { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }
    }
}
