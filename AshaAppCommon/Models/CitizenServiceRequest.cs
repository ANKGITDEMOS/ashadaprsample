
namespace AshaAppCommon.Models;

//public record struct CitizenServiceRequest(string AshaWorkerId, string Citizenid, string ServiceCode, string ServiceId);

public record struct CitizenServiceRequest
{
    public CitizenServiceRequest() { }

    public string AshaWorkerId { get; set; }

    public string Citizenid { get; set; }

    public string ServiceCode { get; set; }

    public string ServiceId { get; set; } 

}