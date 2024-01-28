using WebApiServiceForVacancy.Data.Entities;

namespace WebApiServiceForVacancy.Core.DTOs;

public class OrderDto
{
    public uint Id { get; set; }
    public string Number { get; set; }

    public DateTime CreateDateTime { get; set; }

    public uint CustomerId { get; set; }
    public virtual Customer Customer { get; set; }

    public virtual IEnumerable<OrderProduct> OrderProducts { get; set; }
}