
namespace NewsletterProvider.Functions
{
    using Data.Contexts;
    using Data.Entities;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.Functions.Worker;
    using Microsoft.Azure.Functions.Worker.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    namespace NewsletterProvider.Functions
    {
        public class GetAllSubscribers
        {
            private readonly ILogger<GetAllSubscribers> _logger;
            private readonly DataContext _context;

            public GetAllSubscribers(ILogger<GetAllSubscribers> logger, DataContext context)
            {
                _logger = logger;
                _context = context;
            }

            [Function("GetAllSubscribers")]
            public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req, FunctionContext context)
            {
                try
                {
                    var subscribers = await _context.Subscribers.ToListAsync();
                    return new OkObjectResult(subscribers);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while retrieving subscribers.");
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                }
            }
        }
    }

}
