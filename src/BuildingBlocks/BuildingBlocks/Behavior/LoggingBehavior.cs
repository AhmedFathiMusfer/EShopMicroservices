using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behavior
{
    public class LoggingBehavior<TRquets, TResponse>(ILogger<LoggingBehavior<TRquets, TResponse>> logger) : IPipelineBehavior<TRquets, TResponse>
    where TRquets : notnull, IRequest<TResponse>
    where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRquets request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            logger.LogInformation("[START] handel request={Request} -Response={Response} -RequestData ={RequestData}", typeof(TRquets).Name, typeof(TResponse).Name
            , request);

            var timer = new Stopwatch();
            timer.Start();
            var response = await next();
            timer.Stop();

            var timeTaken = timer.Elapsed;
            if (timeTaken.Seconds > 3)
            {
                logger.LogWarning("[PERFORMANCE] the request {Request} took {timeTaken}", typeof(TRquets).Name, timeTaken.Seconds
           );
            }

            logger.LogInformation("[end] handel request={Request} wtih Response={Response}", typeof(TRquets).Name, typeof(TResponse).Name
                        );

            return response;



        }
    }
}