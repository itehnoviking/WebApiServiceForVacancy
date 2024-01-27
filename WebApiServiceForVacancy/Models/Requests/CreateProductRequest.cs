namespace WebApiServiceForVacancy.Models.Requests;

public class CreateNewProductRequest
{
    public string Name { get; set; }
    public bool IsAvailable { get; set; }
}