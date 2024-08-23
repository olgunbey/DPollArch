using DPoll.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPoll.Domain.Entities
{
    public class User:BaseEntity
    {
        public string Username { get; set; }
        public string Email{ get; set; }
        public string ClerkId{ get; set; }
        public bool IsActive{ get; set; }

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
    }
}
