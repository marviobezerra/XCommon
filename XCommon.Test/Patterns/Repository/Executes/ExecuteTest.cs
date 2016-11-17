using FluentAssertions;
using System;
using XCommon.Extensions.Converters;
using XCommon.Patterns.Repository.Executes;
using XCommon.Test.Patterns.Specification.Validation.Sample;
using Xunit;

namespace XCommon.Test.Patterns.Repository.Executes
{
    public class ExecuteTest
    {
        public ExecuteTest()
        {
            SampleUser = new ExecuteUser
            {
                Key = "1".ToGuid(),
                Login = "marvio.bezerra",
                Name = "Márvio André Bezerra Silverio"
            };
        }

        private ExecuteUser SampleUser { get; set; }

        [Fact(DisplayName = "Constructor (Null parameters)")]
        public void ConstructorNullParameters()
        {
            Execute execute = new Execute(null, null);

            execute.HasErro.Should().Be(false, "There isn't messages to have error");
            execute.HasWarning.Should().Be(false, "There isn't messages to have warning");
            execute.HasException.Should().Be(false, "There isn't messages to have exception");

            execute.User.Should().Be(null, "Just assing a user when it is infomed");

            execute.Messages.Should().NotBeNull("Messages never can be null");
            execute.Messages.Count.Should().Be(0, "There isn't messages");
        }

        [Fact(DisplayName = "Constructor (Empty)")]
        public void Constructor()
        {
            Execute execute = new Execute();

            execute.HasErro.Should().Be(false, "There isn't messages to have error");
            execute.HasWarning.Should().Be(false, "There isn't messages to have warning");
            execute.HasException.Should().Be(false, "There isn't messages to have exception");

            execute.User.Should().Be(null, "Just assing a user when it is infomed");

            execute.Messages.Should().NotBeNull("Messages never can be null");
            execute.Messages.Count.Should().Be(0, "There isn't messages");
        }

        [Fact(DisplayName = "Constructor (User)")]
        public void ConstructorUser()
        {
            Execute execute = new Execute(SampleUser);

            execute.HasErro.Should().Be(false, "There isn't messages to have error");
            execute.HasWarning.Should().Be(false, "There isn't messages to have warning");
            execute.HasException.Should().Be(false, "There isn't messages to have exception");

            execute.User.Should().NotBeNull("The user was infomed");

            execute.Messages.Should().NotBeNull("Messages never can be null");
            execute.Messages.Count.Should().Be(0, "There isn't messages");
        }

        [Fact(DisplayName = "Constructor (Execute)")]
        public void ConstructorExecute()
        {
            Execute prioExecute = new Execute();

            Execute execute = new Execute(prioExecute);

            execute.HasErro.Should().Be(false, "There isn't messages to have error");
            execute.HasWarning.Should().Be(false, "There isn't messages to have warning");
            execute.HasException.Should().Be(false, "There isn't messages to have exception");

            execute.User.Should().Be(null, "Just assing a user when it is infomed");

            execute.Messages.Should().NotBeNull("Messages never can be null");
            execute.Messages.Count.Should().Be(0, "There isn't messages");
        }

        [Fact(DisplayName = "Constructor (Execute & User)")]
        public void ConstructorExecuteThatWasUser()
        {
            Execute prioExecute = new Execute(SampleUser);

            Execute execute = new Execute(prioExecute);

            execute.HasErro.Should().Be(false, "There isn't messages to have error");
            execute.HasWarning.Should().Be(false, "There isn't messages to have warning");
            execute.HasException.Should().Be(false, "There isn't messages to have exception");

            execute.User.Should().NotBe(null, "The user was infomed");

            execute.Messages.Should().NotBeNull("Messages never can be null");
            execute.Messages.Count.Should().Be(0, "There isn't messages");
        }

        [Fact(DisplayName = "Constructor (Execute, User & Messages)")]
        public void ConstructorExecuteUserMessages()
        {
            Execute prioExecute = new Execute(SampleUser);
            prioExecute.AddMessage(ExecuteMessageType.Error, "Error message");
            prioExecute.AddMessage(ExecuteMessageType.Warning, "Warning message");
            prioExecute.AddMessage(ExecuteMessageType.Exception, "Exception message");

            Execute execute = new Execute(prioExecute);

            execute.HasErro.Should().Be(true, "There is error message");
            execute.HasWarning.Should().Be(true, "There is warning message");
            execute.HasException.Should().Be(true, "There is exception message");

            execute.User.Should().NotBe(null, "The user was infomed");

            execute.Messages.Should().NotBeNull("Messages never can be null");
            execute.Messages.Count.Should().Be(3, "There are 3 messages");
        }

        [Fact(DisplayName = "Constructor (Execute + User, Messages & User)")]
        public void ConstructorExecuteUserMessagesPlusUser()
        {
            Execute prioExecute = new Execute(SampleUser);

            prioExecute.AddMessage(ExecuteMessageType.Error, "Error message");
            prioExecute.AddMessage(ExecuteMessageType.Warning, "Warning message");
            prioExecute.AddMessage(ExecuteMessageType.Exception, "Exception message");

            ExecuteUser user = new ExecuteUser
            {
                Key = "2".ToGuid(),
                Login = "jonhy",
                Name = "Jonhy"
            };

            Execute execute = new Execute(prioExecute, user);

            execute.HasErro.Should().Be(true, "There is error message");
            execute.HasWarning.Should().Be(true, "There is warning message");
            execute.HasException.Should().Be(true, "There is exception message");

            execute.User.Should().NotBe(null, "The user was infomed");
            execute.User.Should().Be(user, "The expected user is Jonhy");

            execute.Messages.Should().NotBeNull("Messages never can be null");
            execute.Messages.Count.Should().Be(3, "There isn't messages");
        }

        [Fact(DisplayName = "Constructor (Execute + User (null), Messages & User)")]
        public void ConstructorExecuteUserNullMessagesPlusUser()
        {
            Execute prioExecute = new Execute(SampleUser);

            prioExecute.AddMessage(ExecuteMessageType.Error, "Error message");
            prioExecute.AddMessage(ExecuteMessageType.Warning, "Warning message");
            prioExecute.AddMessage(ExecuteMessageType.Exception, "Exception message");
            
            Execute execute = new Execute(prioExecute);

            execute.HasErro.Should().Be(true, "There is error message");
            execute.HasWarning.Should().Be(true, "There is warning message");
            execute.HasException.Should().Be(true, "There is exception message");

            execute.User.Should().NotBe(null, "The user was infomed");
            execute.User.Should().Be(SampleUser, "The expected user is Jonhy");

            execute.Messages.Should().NotBeNull("Messages never can be null");
            execute.Messages.Count.Should().Be(3, "There isn't messages");
        }

        [Fact(DisplayName = "AddMessage (Message null)")]
        public void AddMessageMessageNull()
        {
            Execute execute = new Execute();

            execute.AddMessage(ExecuteMessageType.Error, null);

            execute.HasErro.Should().Be(true, "There is error message");
            execute.HasWarning.Should().Be(false, "There isn't warning message");
            execute.HasException.Should().Be(false, "There isn't exception message");

            execute.Messages.Count.Should().Be(1, "There is 1 message");
        }

        [Fact(DisplayName = "AddMessage (Message null & Args Null)")]
        public void AddMessageMessageNullArgsNull()
        {
            Execute execute = new Execute();

            execute.AddMessage(ExecuteMessageType.Error, null, null);

            execute.HasErro.Should().Be(true, "There is error message");
            execute.HasWarning.Should().Be(false, "There isn't warning message");
            execute.HasException.Should().Be(false, "There isn't exception message");

            execute.Messages.Count.Should().Be(1, "There isn 1 message");
        }

        [Fact(DisplayName = "AddMessage (Type, Message & Args)")]
        public void AddMessageTypeMessageArgs()
        {
            Execute execute = new Execute();

            execute.AddMessage(ExecuteMessageType.Error, "Errors: {0} - {1}", "One", "Two");

            execute.HasErro.Should().Be(true, "There is error message");
            execute.HasWarning.Should().Be(false, "There isn't warning message");
            execute.HasException.Should().Be(false, "There isn't exception message");

            execute.Messages.Count.Should().Be(1, "There is 1 message");
            execute.Messages[0].Message.Should().Be("Errors: One - Two", "Needs to format the message");
        }

        [Fact(DisplayName = "AddMessage (Type & Message)")]
        public void AddMessageTypeMessage()
        {
            Execute execute = new Execute();

            execute.AddMessage(ExecuteMessageType.Warning, "Errors: {0} - {1}");

            execute.HasErro.Should().Be(false, "There isn't error message");
            execute.HasWarning.Should().Be(true, "There is warning message");
            execute.HasException.Should().Be(false, "There isn't exception message");

            execute.Messages.Count.Should().Be(1, "There isn 1 message");
            execute.Messages[0].Message.Should().Be("Errors: {0} - {1}", "Can't format the message");
        }

        [Fact(DisplayName = "AddMessage (Exception & Message)")]
        public void AddMessageExceptionMessage()
        {
            Execute execute = new Execute();

            execute.AddMessage(new Exception("Check this message"), "Errors: {0} - {1}");

            execute.HasErro.Should().Be(true, "There is error message");
            execute.HasWarning.Should().Be(false, "There is warning message");
            execute.HasException.Should().Be(true, "There is exception message");

            execute.Messages.Count.Should().Be(1, "There isn 1 message");

            var message = execute.Messages[0];

            message.Message.Should().Be("Errors: {0} - {1}", "Can't format the message");
            message.MessageInternal.MessageException.Count.Should().Be(1, "Had been added one exception");
            message.MessageInternal.MessageException[0].Should().Be("Check this message", "That is the exception message");
        }

        [Fact(DisplayName = "AddMessage (Exception, Message & Args)")]
        public void AddMessageExceptionMessageArgs()
        {
            Execute execute = new Execute();

            execute.AddMessage(new Exception("Check this message"), "Errors: {0} - {1}", "Three", "Four");

            execute.HasErro.Should().Be(true, "There is error message");
            execute.HasWarning.Should().Be(false, "There is warning message");
            execute.HasException.Should().Be(true, "There is exception message");

            execute.Messages.Count.Should().Be(1, "There isn 1 message");

            var message = execute.Messages[0];

            message.Message.Should().Be("Errors: Three - Four", "Needs to format the message");
            message.MessageInternal.MessageException.Count.Should().Be(1, "Had been added one exception");
            message.MessageInternal.MessageException[0].Should().Be("Check this message", "That is the exception message");
        }

        [Fact(DisplayName = "AddMessage (Execute array)")]
        public void AddMessageExecuteArray()
        {
            Execute execute01 = new Execute();
            execute01.AddMessage(ExecuteMessageType.Error, "Execute 01 error message");
            execute01.AddMessage(ExecuteMessageType.Warning, "Execute 01 warning message");

            Execute execute02 = new Execute();
            execute02.AddMessage(ExecuteMessageType.Error, "Execute 02 error message");

            Execute execute = new Execute();

            execute.AddMessage(execute01, execute02);

            execute.HasErro.Should().Be(true, "There is error message");
            execute.HasWarning.Should().Be(true, "There is warning message");
            execute.HasException.Should().Be(false, "There isn't exception message");

            execute.Messages.Count.Should().Be(3, "There are 3 messages");
        }

        [Fact(DisplayName = "AddMessage (Execute)")]
        public void AddMessageExecute()
        {
            Execute execute01 = new Execute();
            execute01.AddMessage(ExecuteMessageType.Error, "Execute 01 error message");
            execute01.AddMessage(ExecuteMessageType.Warning, "Execute 01 warning message");

            Execute execute = new Execute();

            execute.AddMessage(execute01);

            execute.HasErro.Should().Be(true, "There is error message");
            execute.HasWarning.Should().Be(true, "There is warning message");
            execute.HasException.Should().Be(false, "There isn't exception message");

            execute.Messages.Count.Should().Be(2, "There are 3 messages");
        }
        
        [Fact(DisplayName = "GetProperty (Simple)")]
        public void GetPropertySimple()
        {
            string propertyName = "IgnorePermissions";
            Execute execute = new Execute();

            execute.SetProperty(propertyName, "Yes");
            string result = execute.GetProperty<string>(propertyName);
            result.Should().Be("Yes", "It is a valid property");
        }

        [Fact(DisplayName = "GetProperty (Complex)")]
        public void GetProperty()
        {
            string propertyName = "AdminUser";
            Execute execute = new Execute();

            execute.SetProperty(propertyName, new PersonEntity { Name = "Marvio" });
            PersonEntity result = execute.GetProperty<PersonEntity>(propertyName);
            result.Name.Should().Be("Marvio", "The person name is Marvio");
        }

        [Fact(DisplayName = "SetProperty (string)")]
        public void SetProperty()
        {
            string propertyName = "IgnorePermissions";
            Execute execute = new Execute();

            var result = execute.SetProperty(propertyName, "Yes");
            result.Should().Be(true, "It is a valid property");
        }
    }
}
