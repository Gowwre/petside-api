namespace PetHealthCare.Model.DTO.Response;

public class ResultResponse<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }
    public string Messages { get; set; }
    public int code { get; set; }
}
