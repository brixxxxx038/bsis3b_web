using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace bsis3b_web.Models
{
    public class Type
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Type")]
        
        public string Name { get; set; }

        public Item Item{ get; set; }

        public int ItemId { get; set; }
    }
}