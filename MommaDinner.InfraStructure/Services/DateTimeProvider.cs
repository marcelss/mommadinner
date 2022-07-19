using MommaDinner.Application.Common.Interfaces.Services;

namespace MommaDinner.InfraStructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}