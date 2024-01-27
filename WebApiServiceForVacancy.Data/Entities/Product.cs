using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiServiceForVacancy.Data.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public bool IsAvailable { get; set; }

    public virtual IEnumerable<OrderProduct>? OrderProducts { get; set; }
}