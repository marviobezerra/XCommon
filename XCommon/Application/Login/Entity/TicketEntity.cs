using System;

namespace XCommon.Application.Login.Entity
{
    public class TicketEntity
    {
        public TicketStatus Status { get; set; }

        public Guid Key { get; set; }

        public string Name { get; set; }

        public string Culture { get; set; }
    }
}
