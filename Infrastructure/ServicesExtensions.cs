using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Core;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;

namespace Infrastructure
{
    public static class ServicesExtensions
    {
        public static void ConfigureApiReader(this IServiceCollection services)
        {
            services
                .AddHttpClient<ISourceReader, FundaApiReader>()
                .AddPolicyHandler(RetryOn429TooManyRequests);

            services.AddHostedService<SourceReaderService>();
        }

        private static AsyncRetryPolicy<HttpResponseMessage> RetryOn429TooManyRequests
            => HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(response => response.StatusCode == (HttpStatusCode)429)
                .OrResult(response => response.StatusCode == (HttpStatusCode)401)
                .WaitAndRetryForeverAsync(
                    sleepDurationProvider: (i, response, context) =>
                    {
                        var retryHeader = response.Result?.Headers.RetryAfter;
                        if (retryHeader == null)
                            return TimeSpan.FromSeconds(5 * i);

                        var delay = retryHeader.Delta;
                        return delay ?? TimeSpan.FromSeconds(5 * i);

                    },
                    onRetryAsync: (_, __, ___) => Task.CompletedTask);
    }
}