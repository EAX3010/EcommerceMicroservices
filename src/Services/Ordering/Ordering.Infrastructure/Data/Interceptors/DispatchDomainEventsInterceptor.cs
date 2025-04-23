#region

using MediatR;
using Ordering.Domain.Interfaces;

#endregion

namespace Ordering.Infrastructure.Data.Interceptors
{
    public sealed class DispatchDomainEventsInterceptor : SaveChangesInterceptor
    {
        private readonly IMediator _mediator;

        public DispatchDomainEventsInterceptor(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            DispatchDomainEvents(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            await DispatchDomainEventsAsync(eventData.Context, cancellationToken);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void DispatchDomainEvents(DbContext? context)
        {
            if (context == null)
            {
                return;
            }

            List<IAggregate> aggregates = context.ChangeTracker
                .Entries<IAggregate>()
                .Where(e => e.Entity.DomainEvents.Count > 0)
                .Select(e => e.Entity)
                .ToList();

            if (aggregates.Count == 0)
            {
                return;
            }

            foreach (IAggregate? aggregate in aggregates)
            {
                List<IDomainEvent> events = aggregate.DomainEvents.ToList();
                _ = aggregate.ClearDomainEvents();

                foreach (IDomainEvent? domainEvent in events)
                {
                    _mediator.Publish(domainEvent).GetAwaiter().GetResult();
                }
            }
        }

        private async Task DispatchDomainEventsAsync(
            DbContext? context,
            CancellationToken cancellationToken)
        {
            if (context == null)
            {
                return;
            }

            List<IAggregate> aggregates = context.ChangeTracker
                .Entries<IAggregate>()
                .Where(e => e.Entity.DomainEvents.Count > 0)
                .Select(e => e.Entity)
                .ToList();

            if (aggregates.Count == 0)
            {
                return;
            }

            foreach (IAggregate? aggregate in aggregates)
            {
                List<IDomainEvent> events = aggregate.DomainEvents.ToList();
                _ = aggregate.ClearDomainEvents();

                foreach (IDomainEvent? domainEvent in events)
                {
                    await _mediator.Publish(domainEvent, cancellationToken)
                        .ConfigureAwait(false);
                }
            }
        }
    }
}