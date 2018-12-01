using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Core.Domain
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public Category Category { get; set; }

        public int CategoryId { get; set; }
        
        public int Stock { get; set; }

        //SQL Lite does not have bit or boolean datatypes  https://www.sqlite.org/datatype3.html
        //Boolean values are stored as integers 0 (false) and 1 (true)
        public bool? InStock { get; set; }
    }
}