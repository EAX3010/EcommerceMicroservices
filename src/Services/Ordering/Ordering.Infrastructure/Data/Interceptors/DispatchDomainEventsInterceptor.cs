
using MediatR;
namespace Ordering.Infrastructure.Data.Interceptors
{
    public class DispatchDomainEventsInterceptor(IMediator Mediator) : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
            return base.SavingChanges(eventData, result);
        }
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default(CancellationToken))
        {
            await DispatchDomainEvents(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private async Task DispatchDomainEvents(DbContext? context)
        {
            var aggregates =
                context?.ChangeTracker.Entries<Domain.Abstractions.IAggregate>()
                .Where(a => a.Entity.DomainEvents.Any()).Select(a => a.Entity).ToList();

            if (aggregates == null || aggregates.Count == 0) return;
            foreach (var item in aggregates)
            {
                foreach (var domainEvent in item.DomainEvents)
                {
                    await Mediator.Publish(domainEvent);
                }
                item.ClearDomainEvents();
            }
        }
    }
}
