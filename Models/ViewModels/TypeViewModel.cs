using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace bsis3b_web.Models.ViewModels
{
    public class TypeViewModel
    {
        public Type Type { get; set; }


        public IEnumerable<Item> Items { get; set; } 


        public IEnumerable<SelectListItem> selectListItem(IEnumerable<Item> Items)
        {
            List<SelectListItem> ItemList = new List<SelectListItem>();
            SelectListItem sli = new SelectListItem()
            {
                Text = "Select Items",
                Value = "0"
            };
            ItemList.Add(sli);
            foreach(Item item in Items)
            {
            sli = new SelectListItem()
                {
                    Text =item.Name,
                    Value =item.Id.ToString()
                };
                ItemList.Add(sli);
            }
            return ItemList;
        }
    }
} 