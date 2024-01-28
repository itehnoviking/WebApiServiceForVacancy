namespace WebApiServiceForVacancy.Models.Responses;

public class GetOrderByCustomerIdResponce
{
    public uint Id { get; set; }
    public string Number { get; set; }
    public DateTime CreateDateTime { get; set; }
    public uint CustomerId { get; set; }
}