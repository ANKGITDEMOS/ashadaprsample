using AshaAppCommon.Models;
using AshaService.DomainServices;
using AshaService.Events;
using AshaService.Models;
using AshaService.Repositories;
using Dapr;
using System.Text.Json;

namespace AshaService.Controllers
{
    

    [ApiController]
    [Route("")]
    public class AshaController : ControllerBase
    {
        private Random _rnd = new Random();
        private readonly ILogger<AshaController> _logger;
        private readonly IAshaRepository _asharepository;
        private readonly IAshaManagement _ashamgmt;

        public AshaController(
             ILogger<AshaController> logger,
             IAshaRepository ashaRepository,
             IAshaManagement ashamgmt)
        {
            _logger = logger;
            _asharepository = ashaRepository;
            _ashamgmt = ashamgmt;
        }



        [HttpPost("registercitizen")]
        public async Task<ActionResult> RegisterCitizenAsync(CitizenRequest msg,[FromServices] DaprClient daprClient)
        {
            try
            {
                _logger.LogInformation("AshaService: Registration of Citizenrequest started");
                var citizenservicerequest = new CitizenServiceRequest();
                citizenservicerequest.AshaWorkerId = msg.ashaworkerid;
                citizenservicerequest.Citizenid = msg.citizenid;
                citizenservicerequest.ServiceCode = msg.servicecode;
                citizenservicerequest.ServiceId = GenerateRandomCharacters(15);
                _logger.LogInformation("AshaService: Saving Citizenrequest ");
                await _asharepository.SaveCitizeServiceRequestAsync(citizenservicerequest);
                _logger.LogInformation("AshaService: Citizenrequest Saved");

                //var state = await _asharepository.GetCitizeServiceRequestAsync(servicerequest.serviceid);
                //if(state == null) {
                //    await _asharepository.SaveCitizeServiceRequestAsync(servicerequest);
                //}
                _logger.LogInformation("AshaService: publishing citizen request for visit schedule" 
                    + citizenservicerequest.ServiceId + "||" + citizenservicerequest.Citizenid + "||" + citizenservicerequest.AshaWorkerId + "||"
                    + citizenservicerequest.ServiceCode);

                var messageJson = JsonSerializer.Serialize<object>(citizenservicerequest);
                _logger.LogInformation("AshaService: message json = "+ messageJson);
                await daprClient.PublishEventAsync("pubsub", "citizenservicerequests", citizenservicerequest);
                _logger.LogInformation("AshaService: published citizen request for visit schedule");
                
                return Ok();
            }
            catch (Exception ex) {

                _logger.LogError(ex, "Error occurred while processing registeration of citizen");
                return StatusCode(500);
            }            
        }

        [Topic("pubsub", "visitschedules", "deadletters", false)]
        [Route("savevisitschedule")]
        [HttpPost()]
        public async Task<ActionResult> SaveVisitSchedule(VisitSchedule visitSchedule, [FromServices] DaprClient daprClient)
        {
            _logger.LogInformation("Visit schedule received");
            if (visitSchedule != null)
            {
                _logger.LogInformation("Saving Visit schedule");
                //await _asharepository.SaveVisitScheduleAsync(visitSchedule);
                _logger.LogInformation("Saved Visit schedule");

                //var state = await _asharepository.GetVisitScheduleAsync(visitSchedule.Id);
                //if(state == null)
                //{
                //    await _asharepository.SaveVisitScheduleAsync(visitSchedule);
                //}
            }
            else
            {
                _logger.LogInformation("Visit schedule is null");
            }
            return Ok();
        }


        private string _validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ 1234567890";
        private string GenerateRandomCharacters(int aantal)
        {
            char[] chars = new char[aantal];
            for (int i = 0; i < aantal; i++)
            {
                chars[i] = _validChars[_rnd.Next(_validChars.Length - 1)];
            }
            return new string(chars);
        }


        [Topic("pubsub", "deadletters")]
        [Route("deadletters")]
        [HttpPost()]
        public ActionResult HandleDeadLetter(object message)
        {
            _logger.LogError("The service was not able to handle a visitor message.");

            try
            {
                //var messageJson = JsonSerializer.Serialize<object>(message);
                //_logger.LogInformation($"Unhandled message content: {messageJson}");
            }
            catch
            {
                _logger.LogError("Unhandled message's payload could not be deserialized.");
            }

            return Ok();
        }

    }
}
