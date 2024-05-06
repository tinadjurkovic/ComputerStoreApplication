using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerStoreApplication.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        [NotMapped]
        public List<string> CategoryList { get; set; }

        public string CategoryString
        {
            get => string.Join(",", CategoryList);
            set => CategoryList = value?.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public virtual ICollection<Category> Categories { get; set; }
    }
}
