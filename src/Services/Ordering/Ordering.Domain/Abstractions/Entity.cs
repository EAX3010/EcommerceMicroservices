namespace Ordering.Domain.Abstractions
{
    public abstract class Entity<T> : IEntity<T>
    {
        public T Id { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = "Seeder";
        public DateTime? LastModified { get; set; } = DateTime.UtcNow;
        public string LastModifiedBy { get; set; } = "Seeder";
    }
}