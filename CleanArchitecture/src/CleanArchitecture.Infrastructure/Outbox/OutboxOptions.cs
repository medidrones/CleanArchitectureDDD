namespace CleanArchitecture.Infrastructure.Outbox;

public class OutboxOptions
{
    public int IntervallSeconds { get; init; }
    public int BatchSize { get; init; }
}
