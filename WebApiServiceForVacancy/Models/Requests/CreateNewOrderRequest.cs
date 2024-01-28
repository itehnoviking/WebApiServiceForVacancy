namespace WebApiServiceForVacancy.Models.Requests;

public class CreateNewOrderRequest
{
    public string Number { get; set; }
    public uint CustomerId { get; set; }

    public IEnumerable<uint> ProductIds { get; set; }
}