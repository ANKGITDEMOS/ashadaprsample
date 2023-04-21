using AshaAppCommon.Models;

namespace AshaService.DomainServices
{
    public interface IAshaManagement
    {
        //int RegisterCitizen(string ashaworkerid, string citizenid);
       
        //int RegisterVisit(string ashaworkerid, string citizenid, string visitid);

        //IEnumerable<VisitSchedule> GetVisitSchedules(int AshaWorkerId, int CitizenID);

    }

    public interface IStateAshaManagement : IAshaManagement
    {
        //State Specific Overrides


    
    }


}
