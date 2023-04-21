using Simulation.Proxies;


var ashaService = await MqttAshaService.CreateAsync();
AshaSimulation ashaSimulation = new AshaSimulation(ashaService);
var asha = ashaSimulation.Start();


Task.Run(() => Thread.Sleep(Timeout.Infinite)).Wait();