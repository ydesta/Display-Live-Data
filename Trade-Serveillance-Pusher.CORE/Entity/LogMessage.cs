using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade_Serveillance_Pusher.CORE.Entity
{
   public class LogMessage
    {       
        public int LogMessageId { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Success { get; set; }
        public int? LineNumber { get; set; }
        public string FileName { get; set; }
    }
}
