namespace WebApiServiceForVacancy.Data.Entities;

public class OrderProduct : BaseEntity
{
    public uint OrderId { get; set; }
    public virtual Order Order { get; set; }

    public uint ProductId { get; set; }
    public virtual Product Product { get; set; }
}