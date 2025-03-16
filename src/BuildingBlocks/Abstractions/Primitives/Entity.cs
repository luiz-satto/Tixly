namespace BuildingBlocks.Abstractions.Primitives
{
    public abstract class Entity
    {
        protected Entity(Guid id) => Id = id;
        protected Entity()
        {

        }

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
