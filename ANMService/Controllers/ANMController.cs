using ANMService.DomainServices;

using AshaAppCommon.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ANMService.Controllers
{
    [ApiController]
    [Route("")]
    public class ANMController : ControllerBase
    {

        private ANMManager _manager;
        private readonly ILogger<ANMController> _logger;


        public ANMController(
            ILogger<ANMController> logger,
            ANMManager anmmanager, DaprClient daprClient)
        {
            _logger = logger;
            _manager = anmmanager;
        }



        //[Topic("pubsub", "citizenservicerequests", "deadletters", false)]
        //[Route("generateschedule")]
        //[HttpPost()]
        //public async Task<ActionResult> GenerateSchedule(CitizenServiceRequest citizenservicerequest, [FromServices] DaprClient daprClient)
        //{
        //    try
        //    {
        //        if (citizenservicerequest != null)
        //        {
        //            _logger.LogInformation("Service request is not null" + citizenservicerequest.AshaWorkerId);
        //        }
        //        else
        //        {
        //            _logger.LogInformation("Service request is null");
        //        }

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error occurred while processing EXIT");
        //        return StatusCode(500);
        //    }
        //}



        [Topic("pubsub", "citizenservicerequests", "deadletters", false)]
        [Route("generateschedule")]
        [HttpPost()]
        public async Task<ActionResult> GenerateSchedule(CitizenServiceRequest serviceRequest, [FromServices] DaprClient daprClient)
        {
            try
            {
                if (serviceRequest != null)
                {
                    _logger.LogInformation("Service request is not null");
                }
                else
                {
                    _logger.LogInformation("Service request is null");
                    return Ok();
                }


                _logger.LogInformation("CitizenRequest Received " + serviceRequest.Citizenid);
                VisitSchedule visitSchedule;
                if (serviceRequest != null)
                {
                    _logger.LogInformation("Getting schedule");
                    visitSchedule = _manager.GetNextSchedule(serviceRequest);
                    if (visitSchedule != null)
                    {
                        _logger.LogInformation("Publishing schedule");
                        await daprClient.PublishEventAsync("pubsub", "visitschedules", visitSchedule);
                        _logger.LogInformation("Published schedule");

                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing EXIT");
                return StatusCode(500);
            }
        }


        [Topic("pubsub", "deadletters")]
        [Route("deadletters")]
        [HttpPost()]
        public ActionResult HandleDeadLetter(object message)
        {
            _logger.LogError("The service was not able to handle a citizenrequest message.");

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
