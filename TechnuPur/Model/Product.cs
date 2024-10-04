using System.ComponentModel.DataAnnotations;

namespace TechnuPur.Model
{
    public class Product
    {
        [Key]
        public Guid ID { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
