using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace MyTimerTrigger;

public class Function1
{
    private readonly ILogger _logger;

    public Function1(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<Function1>();
    }

    [Function("Function1")]
    public async Task RunAsync([TimerTrigger("*/5 * * * * *")] TimerInfo myTimer)
    {
       _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            
            var httpClient=new HttpClient();
            var response = await httpClient.GetAsync("https://timertriggerdemo20260204074544.azurewebsites.net/api/Function2?code=LgQ6TxwbAmoxRNWp13Po5fv7JLaugk8LLzMyESczndn7AzFuWP1UZQ==");
            var message=await response.Content.ReadAsStringAsync();
            _logger.LogInformation($"This is response from HttpTrigger function {message}");
            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }
    }
}