using System.ComponentModel.DataAnnotations;

namespace MotorPlantDatabaseImplement.Models
{
    public class StoreComponent
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int ComponentId { get; set; }
        [Required]
        public int Count { get; set; }
        public virtual Component Component { get; set; }
        public virtual Store Store { get; set; }
    }
}
