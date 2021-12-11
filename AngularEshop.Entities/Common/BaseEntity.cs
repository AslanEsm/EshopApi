using System;

namespace AngularEshop.Entities.Common
{
    public interface IEntity
    {

    }

    public abstract class BaseEntity<TKey> : IEntity
    {
        public TKey Id { get; set; }


        public DateTime CreateDate { get; set; }

        public DateTime LastUpdateDate { get; set; }

        public BaseEntity()
        {
            CreateDate = DateTime.Now;
            LastUpdateDate = DateTime.Now;
        }
    }

    public abstract class BaseEntity : BaseEntity<int>
    {

    }
}
