using System.ComponentModel.DataAnnotations;

namespace Omnibees.BeeBilling.Domain.Entities.Common
{
    public abstract class EntityBase
    {
        [Key]
        [Required]
        public int Id { get; set; }
    }
}
