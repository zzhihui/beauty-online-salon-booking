using Core.Common.Infrastructure.EntityFramework;

namespace App.Data.Entities
{
    public abstract class Entity<T> : IEntity<T>
    {
    }

    public abstract class Entity : Entity<int>
    {
    }
}