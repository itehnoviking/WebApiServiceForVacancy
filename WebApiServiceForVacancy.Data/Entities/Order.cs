using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiServiceForVacancy.Data.Entities;

public class Order : BaseEntity
{
    public string Number { get; set; }
    
    public DateTime CreaDateTime { get; set; }

    public uint CustomerId { get; set; }
    public virtual Customer Customer { get; set; }

    public virtual IEnumerable<OrderProduct> OrderProducts { get; set; }
}