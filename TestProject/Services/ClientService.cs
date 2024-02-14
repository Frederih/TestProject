using TestProject.Persistence;

namespace TestProject.Services
{
    public class ClientService
    {
        public Client GenerateNewExperiment(string deviceToken)
        {
            var random = new Random();
            var experimentKey = random.Next(1, 3) == 1 ? "button_color" : "price";

            if (experimentKey == "button_color")
            {
                var colors = new[] { "#FF0000", "#00FF00", "#0000FF" };
                return new Client { DeviceToken = deviceToken, ExperimentKey = experimentKey, ExperimentValue = colors[random.Next(colors.Length)] };
            }
            else
            {
                var prices = new[] { "10", "20", "50", "5" };
                return new Client { DeviceToken = deviceToken, ExperimentKey = experimentKey, ExperimentValue = prices[random.Next(prices.Length)] };
            }
        }
    }
}
