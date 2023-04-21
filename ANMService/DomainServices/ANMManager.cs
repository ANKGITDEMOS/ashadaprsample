
using AshaAppCommon.Models;

namespace ANMService.DomainServices
{
    public class ANMManager
    {
        private Random _rnd;

        public ANMManager() {
            _rnd = new Random();
        }

        public VisitSchedule GetNextSchedule(CitizenServiceRequest serviceRequest)
        {
            VisitSchedule schedule = new VisitSchedule(serviceRequest.Citizenid,serviceRequest.AshaWorkerId, GenerateRandomCharacters(10));
            schedule.Address = "2432, abc street" + GenerateRandomCharacters(20);
            schedule.StartDate = DateTime.Now.AddDays(10);
            schedule.EndDate = DateTime.Now.AddDays(100);
            return schedule;
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

    }
}
