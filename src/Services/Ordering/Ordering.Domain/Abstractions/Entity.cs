using Ordering.Domain.Interfaces;

namespace Ordering.Domain.Abstractions
{
    public abstract class Entity<T> : IEntity<T> where T : notnull
    {
        public required T Id { get; set; }
        public DateTime? CreatedAt { get; set; } = default;
#pragma warning disable CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
        public string? CreatedBy { get; set; } = default;
#pragma warning restore CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
        public DateTime? LastModified { get; set; } = default;
#pragma warning disable CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
        public string? LastModifiedBy { get; set; } = default;
#pragma warning restore CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
    }
}