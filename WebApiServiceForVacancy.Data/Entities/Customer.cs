using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiServiceForVacancy.Data.Entities;

public class Customer : BaseEntity
{
    public string Name { get; set; }
    public virtual IEnumerable<Order>? Orders { get; set; }
}