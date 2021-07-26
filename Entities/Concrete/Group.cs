using Core.Entities;

namespace Entities.Concrete
{
    public class Group : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}
