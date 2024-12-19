using static MansardRenting.Common.EntityValidationConstants.Category;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MansardRenting.Data.Models
{
    public class Category
    {
        public Category()
        {
            Houses = new HashSet<House>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public ICollection<House> Houses { get; set; }
    }
}
