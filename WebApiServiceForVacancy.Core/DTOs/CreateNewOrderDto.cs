namespace WebApiServiceForVacancy.Core.DTOs;

public class CreateNewOrderDto
{
    public string Number { get; set; }
    public DateTime CreateDateTime { get; set; }
    public uint CustomerId { get; set; }

    public IEnumerable<uint> ProductIds { get; set; }
}