#region

using Ordering.Domain.Interfaces;

#endregion

namespace Ordering.Infrastructure.Data.Interceptors
{
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        private static readonly string SystemUser = "System";

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken).ConfigureAwait(false);
        }

        private static void UpdateEntities(DbContext? context)
        {
            if (context == null) return;

            var timestamp = DateTime.UtcNow;
            var entries = context.ChangeTracker.Entries<IEntity>();

            foreach (var entry in entries)
                switch (entry.State)
                {
                    case EntityState.Added:
                        SetCreatedProperties(entry.Entity, timestamp);
                        SetModifiedProperties(entry.Entity, timestamp);
                        break;

                    case EntityState.Modified:
                        SetModifiedProperties(entry.Entity, timestamp);
                        break;
                }
        }

        private static void SetCreatedProperties(IEntity entity, DateTime timestamp)
        {
            entity.CreatedAt = timestamp;
            entity.CreatedBy = SystemUser;
        }

        private static void SetModifiedProperties(IEntity entity, DateTime timestamp)
        {
            entity.LastModified = timestamp;
            entity.LastModifiedBy = SystemUser;
        }
    }
}