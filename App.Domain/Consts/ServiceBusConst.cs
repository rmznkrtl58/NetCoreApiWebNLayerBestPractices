using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Consts
{
    public class ServiceBusConst
    {
        //Exchange bir mesaj geldiği zaman o exchangin göndereceği kuyruğun ismi olacak.
        public const string ProductAddedEventQueueName = "clean.app.productadded.event.queue";
    }
}
