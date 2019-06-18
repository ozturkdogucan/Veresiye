using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veresiye.Model
{
    public class BaseEntity
    { 
        // projenin ortak alanlarını BaseEntity'e koyuyoruz.
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } // ne zaman oluşturuldu?
        public string CreatedBy { get; set; } // kim tarafından oluşturuldu?
        public DateTime UpdatedAt { get; set; } // ne zaman güncellendi?
        public string UpdatedBy { get; set; } // kim tarafından güncellendi?
    }
}
