using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Group : EntityBase, IEntity
    {
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}
