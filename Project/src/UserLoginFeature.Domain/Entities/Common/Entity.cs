namespace UserLoginFeature.Domain.Entities.Common
{
    public class Entity
    {
        public virtual Guid Id { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime? UpdatedDate { get; set; }
        public virtual bool IsActive { get; set; } = true;
    }
}
