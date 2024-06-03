using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Repository.Entity
{
    [Table("ConversationMessage")]
    public class ConversationMessage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long ConversationId { get; set; }

        public long SenderId { get; set; }

        public DateTime CreateTime { get; set; }

        public string Content { get; set; }

        public bool IsDelete { get; set; }

        public DateTime DeleteAt { get; set; }

        public bool IsSeen { get; set; }

        [ForeignKey("ConversationId")]
        public virtual Conversation Conversation { get; set; }

        [ForeignKey("SenderId")]
        public virtual User User { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<MessageReaction> MessageReactions { get; set; }
    }
}
