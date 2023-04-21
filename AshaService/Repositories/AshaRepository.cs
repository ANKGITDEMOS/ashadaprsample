using AshaAppCommon.Models;
using AshaService.Models;

namespace AshaService.Repositories
{
    public class AshaRepository : IAshaRepository
    {
        private const string DAPR_SCHEDULESTORE_NAME = "statestore";

        //private const string DAPR_ASHAWORKER_STORE_NAME = "ashaworkerstore";

        //private const string DAPR_CITIZENREQUEST_STORE_NAME = "citizenrequeststore";


        private readonly DaprClient _daprClient;

        public AshaRepository(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }


        public async Task<VisitSchedule?> GetVisitScheduleAsync(string visitId)
        {
            //Fetch from DB - may put cacheaside pattern if required.

            var stateEntry = await _daprClient.GetStateEntryAsync<VisitSchedule>(
            DAPR_SCHEDULESTORE_NAME, visitId);
            return stateEntry.Value;
        }

        public async Task SaveVisitScheduleAsync(VisitSchedule visitSchedule)
        {
            //Save it in DB as well now only cache.

            await _daprClient.SaveStateAsync<VisitSchedule>(
            DAPR_SCHEDULESTORE_NAME, visitSchedule.Id, visitSchedule);
        }


        public async Task<CitizenServiceRequest?> GetCitizeServiceRequestAsync(string requestid)
        {
            //Fetch from DB - may put cacheaside pattern if required.

            var stateEntry = await _daprClient.GetStateEntryAsync<CitizenServiceRequest>(
            DAPR_SCHEDULESTORE_NAME, requestid);
            return stateEntry.Value;
        }

        public async Task SaveCitizeServiceRequestAsync(CitizenServiceRequest servicerequest)
        {
            //Save it in DB as well now only cache.

            await _daprClient.SaveStateAsync<CitizenServiceRequest>(
            DAPR_SCHEDULESTORE_NAME, servicerequest.ServiceId, servicerequest);
        }




        public void  RegisterCitizenRequest()
        {

        }


    }
}
