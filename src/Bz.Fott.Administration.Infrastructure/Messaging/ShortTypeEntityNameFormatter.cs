using MassTransit;

namespace Bz.Fott.Administration.Infrastructure.Messaging;

internal class ShortTypeEntityNameFormatter : IEntityNameFormatter
{
    public string FormatEntityName<T>()
    {
        return typeof(T).Name.ToString();
    }
}
