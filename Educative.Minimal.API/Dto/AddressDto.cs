namespace Educative.Minimal.API.Dto;

public class AddressDto
{
    public string AddressID { get; set; } = string.Empty;
    public string Addr1 { get; set; } = string.Empty;
    public string Add2 { get; set; } = string.Empty;
    public string? City { get; set; }
    public string County { get; set; } = string.Empty!;
    public string Postcode { get; set; } = string.Empty!;
    public string? StudentAddressID { get; set; }
    public StudentDto? StudentDto { get; set; }
}