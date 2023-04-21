namespace Simulation;

public class AshaSimulation
{
    private readonly IAshaService _ashaService;
    private Random _rnd;
    private int _minEntryDelayInMS = 50;
    private int _maxEntryDelayInMS = 5000;
    private int _minExitDelayInS = 4;
    private int _maxExitDelayInS = 10;

    public AshaSimulation(IAshaService ashaService)
    {
        _rnd = new Random();
        _ashaService = ashaService;
    }

    public Task Start()
    {
        Console.WriteLine($"Start Asha for Punjab NIC simulation.");

        while (true)
        {
            try
            {
                // simulate entry
                TimeSpan entryDelay = TimeSpan.FromMilliseconds(_rnd.Next(_minEntryDelayInMS, _maxEntryDelayInMS) + _rnd.NextDouble());
                Task.Delay(entryDelay).Wait();

                Task.Run(async () =>
                {
                    // simulate entry
                    DateTime entryTimestamp = DateTime.Now;
                    var citizenrequest = new CitizenRequest
                    {
                        ashaworkerid = "32342",
                        citizenid = GenerateRandomCitizenIDNumber(),
                        servicecode = "PNJB8976"
                    };
                    await _ashaService.RegisterCitizenAsync(citizenrequest);
                    Console.WriteLine($"Simulated citizenrequest with ashaworkerid {citizenrequest.ashaworkerid} for citizen {citizenrequest.citizenid}");

                }).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error: {ex.Message}");
            }
        }
    }

    #region Private helper methods

    private string _validCitizenIDNumberChars = "DFGHJKLNPRSTXYZ";

    private string GenerateRandomCitizenIDNumber()
    {
        int type = _rnd.Next(1, 9);
        string kenteken = string.Empty;
        switch (type)
        {
            case 1: // 99-AA-99
                kenteken = string.Format("{0:00}-{1}-{2:00}", _rnd.Next(1, 99), GenerateRandomCharacters(2), _rnd.Next(1, 99));
                break;
            case 2: // AA-99-AA
                kenteken = string.Format("{0}-{1:00}-{2}", GenerateRandomCharacters(2), _rnd.Next(1, 99), GenerateRandomCharacters(2));
                break;
            case 3: // AA-AA-99
                kenteken = string.Format("{0}-{1}-{2:00}", GenerateRandomCharacters(2), GenerateRandomCharacters(2), _rnd.Next(1, 99));
                break;
            case 4: // 99-AA-AA
                kenteken = string.Format("{0:00}-{1}-{2}", _rnd.Next(1, 99), GenerateRandomCharacters(2), GenerateRandomCharacters(2));
                break;
            case 5: // 99-AAA-9
                kenteken = string.Format("{0:00}-{1}-{2}", _rnd.Next(1, 99), GenerateRandomCharacters(3), _rnd.Next(1, 10));
                break;
            case 6: // 9-AAA-99
                kenteken = string.Format("{0}-{1}-{2:00}", _rnd.Next(1, 9), GenerateRandomCharacters(3), _rnd.Next(1, 10));
                break;
            case 7: // AA-999-A
                kenteken = string.Format("{0}-{1:000}-{2}", GenerateRandomCharacters(2), _rnd.Next(1, 999), GenerateRandomCharacters(1));
                break;
            case 8: // A-999-AA
                kenteken = string.Format("{0}-{1:000}-{2}", GenerateRandomCharacters(1), _rnd.Next(1, 999), GenerateRandomCharacters(2));
                break;
        }

        return kenteken;
    }

    private string GenerateRandomCharacters(int aantal)
    {
        char[] chars = new char[aantal];
        for (int i = 0; i < aantal; i++)
        {
            chars[i] = _validCitizenIDNumberChars[_rnd.Next(_validCitizenIDNumberChars.Length - 1)];
        }
        return new string(chars);
    }

    #endregion
}
