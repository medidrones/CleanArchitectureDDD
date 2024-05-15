using Microsoft.Extensions.Options;
using Quartz;

namespace CleanArchitecture.Infrastructure.Outbox;

public class ProcessOutboxMessagesSetup : IConfigureOptions<QuartzOptions>
{
    private readonly OutboxOptions _outboxOptions;

    public ProcessOutboxMessagesSetup(IOptions<OutboxOptions> outboxOptions)
    {
        _outboxOptions = outboxOptions.Value;
    }

    public void Configure(QuartzOptions options)
    {
        const string jobName = nameof(InvokeOutboxMessagesJob);
        options
            .AddJob<InvokeOutboxMessagesJob>(configure => configure.WithIdentity(jobName))
            .AddTrigger(configure => configure
                .ForJob(jobName)
                .WithSimpleSchedule(schedule => schedule
                    .WithIntervalInSeconds(_outboxOptions.IntervallSeconds)
                    .RepeatForever()));
    }
}
