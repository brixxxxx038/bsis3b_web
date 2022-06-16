using System.Collections.Generic;
namespace bsis3b_web.Models.ViewModels
{
    public class TypeViewModel
    {
        public Type Type { get; set; }


        public IEnumerable<Item> Items { get; set; } 
    }
} 