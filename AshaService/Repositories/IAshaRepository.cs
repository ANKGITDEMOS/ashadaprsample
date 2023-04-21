using AshaAppCommon.Models;
using AshaService.Models;

namespace AshaService.Repositories
{
    public interface IAshaRepository
    {
        Task SaveVisitScheduleAsync(VisitSchedule visitSchedule);
        Task<VisitSchedule?> GetVisitScheduleAsync(string visitId);


        Task SaveCitizeServiceRequestAsync(CitizenServiceRequest serviceRequest);
        Task<CitizenServiceRequest?> GetCitizeServiceRequestAsync(string serviceid);
    }
}
