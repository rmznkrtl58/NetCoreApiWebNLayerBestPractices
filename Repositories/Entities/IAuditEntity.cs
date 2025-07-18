using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Entities
{
    public interface IAuditEntity
    {
        public DateTime CreatedDate{ get; set; }
        public DateTime? UpdatedDate{ get; set; }
    }
}
