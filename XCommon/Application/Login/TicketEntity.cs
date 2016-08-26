using System;

namespace XCommon.Application.Login
{
    public class TicketEntity
    {
        public TicketStatus Status { get; set; }

        public Guid Key { get; set; }

        public string Name { get; set; }

        public string Culture { get; set; }
    }
}
