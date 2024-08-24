using Sm.Crm.Domain.Common;
using Sm.Crm.Domain.Entities.LST;

namespace Sm.Crm.Domain.Events;

public class TitleCreatedEvent : BaseEvent
{
    public Title Title { get; }

    public TitleCreatedEvent(Title title)
    {
        Title = title;
    }
}