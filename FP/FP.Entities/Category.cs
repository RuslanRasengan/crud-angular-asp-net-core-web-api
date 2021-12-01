using FP.Entities.Base;

namespace FP.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public bool IsIncome { get; set; }
        public AppUser AppUser { get; set; }
    }
}
