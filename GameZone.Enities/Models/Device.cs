using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameZone.Enities.Models
{
    public class Device : BaseEntity
    {
        [MaxLength(500)]
        public string Icon {  get; set; } = string.Empty;

        public ICollection<GameDevice> Games { get; set; } = new List<GameDevice>();
    }
}
