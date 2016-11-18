using FluentAssertions;
using System;
using System.Threading.Tasks;
using XCommon.Application.Login;
using XCommon.Application.Login.Entity;
using XCommon.Application.Login.Implementations;
using XCommon.Extensions.Converters;
using Xunit;

namespace XCommon.Test.Application.Login
{
    public class TicketManagerTest
    {
        public string DefaultCulture => "pt-BR";

        [Fact(DisplayName = "Default ticket")]
        public void DefaultTicket()
        {
            ITicketManager ticket = new TicketManagerInMemory(DefaultCulture);

            ticket.User.Should().BeNull();
            ticket.Culture.Should().Be(DefaultCulture);
            ticket.IsAuthenticated.Should().Be(false);
            ticket.UserKey.Should().Be(Guid.Empty);
        }

        [Fact(DisplayName = "SignIn")]
        public async Task SignIn()
        {
            ITicketManager ticket = new TicketManagerInMemory(DefaultCulture);

            await ticket.SignInAsync(new TicketEntity { Culture = "en-CA", Key = "1".ToGuid(), Name = "Marvio André", Status = TicketStatus.Sucess });

            ticket.User.Should().NotBeNull();
            ticket.Culture.Should().Be("en-CA");
            ticket.IsAuthenticated.Should().Be(true);
            ticket.UserKey.Should().Be("1".ToGuid());
        }

        [Fact(DisplayName = "SignIn (Null)")]
        public async Task SignInNull()
        {
            ITicketManager ticket = new TicketManagerInMemory(DefaultCulture);

            await ticket.SignInAsync(null);

            ticket.User.Should().BeNull();
            ticket.Culture.Should().Be(DefaultCulture);
            ticket.IsAuthenticated.Should().Be(false);
            ticket.UserKey.Should().Be(Guid.Empty);
        }

        [Fact(DisplayName = "SignIn (Status Fail)")]
        public async Task SignStatusFail()
        {
            ITicketManager ticket = new TicketManagerInMemory(DefaultCulture);

            await ticket.SignInAsync(new TicketEntity { Culture = "en-CA", Key = "1".ToGuid(), Name = "Marvio André", Status = TicketStatus.Fail });

            ticket.User.Should().BeNull();
            ticket.Culture.Should().Be(DefaultCulture);
            ticket.IsAuthenticated.Should().Be(false);
            ticket.UserKey.Should().Be(Guid.Empty);
        }

        [Fact(DisplayName = "SignIn (Status FailExternal)")]
        public async Task SignStatusFailExternal()
        {
            ITicketManager ticket = new TicketManagerInMemory(DefaultCulture);

            await ticket.SignInAsync(new TicketEntity { Culture = "en-CA", Key = "1".ToGuid(), Name = "Marvio André", Status = TicketStatus.FailExternal });

            ticket.User.Should().BeNull();
            ticket.Culture.Should().Be(DefaultCulture);
            ticket.IsAuthenticated.Should().Be(false);
            ticket.UserKey.Should().Be(Guid.Empty);
        }

        [Fact(DisplayName = "SignOutAsync")]
        public async Task SignOutAsync()
        {
            ITicketManager ticket = new TicketManagerInMemory(DefaultCulture);

            await ticket.SignInAsync(new TicketEntity { Culture = "en-CA", Key = "1".ToGuid(), Name = "Marvio André", Status = TicketStatus.Sucess });

            ticket.User.Should().NotBeNull();
            ticket.Culture.Should().Be("en-CA");
            ticket.IsAuthenticated.Should().Be(true);
            ticket.UserKey.Should().Be("1".ToGuid());

            await ticket.SignOutAsync();

            ticket.User.Should().BeNull();
            ticket.Culture.Should().Be(DefaultCulture);
            ticket.IsAuthenticated.Should().Be(false);
            ticket.UserKey.Should().Be(Guid.Empty);
        }
    }
}
