using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Options
{
    public class ServiceBusOption
    {   //Rabbit Mq Urli tip güvenliğinden dolayı böyle bir class oluşturdum.
        public string Url { get; set; } = default!;
    }
}
