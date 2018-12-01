using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Core.Domain
{
    public class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        [Key] public int Id { get; set; }

        [Required] public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}