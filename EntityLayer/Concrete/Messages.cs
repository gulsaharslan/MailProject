using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Messages
    {
        [Key]
        public int MessageId { get; set; }

        public int ReceiverUserId { get; set; }
        public AppUser ReceiverUser { get; set; }
        public int SenderUserId { get; set; }
        public AppUser SenderUser { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
        public bool IsDeleted {  get; set; }
        public bool IsDraft { get; set; }
        public bool IsFavorite { get; set; }


    }
}
