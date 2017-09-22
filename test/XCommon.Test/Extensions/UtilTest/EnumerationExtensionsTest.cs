using FluentAssertions;
using XCommon.Extensions.Enum;
using XCommon.Test.Extensions.UtilTest.Sample;
using Xunit;

namespace XCommon.Test.Extensions.UtilTest
{
	public class EnumerationExtensionsTest
    {
        [Fact(DisplayName = "Has (False)")]
		[Trait("Extensions", "Util")]
		public void HasFalse()
        {
            var value = Profile.Manager;

            var result = value.Has(Profile.CEO);

            result.Should().Be(false, "The profile is a manager, not a CEO");
        }

        [Fact(DisplayName = "Has (True)")]
		[Trait("Extensions", "Util")]
		public void HasTrue()
        {
            var value = Profile.Manager;

            var result = value.Has(Profile.Manager);

            result.Should().Be(true, "The profile is a manager");
        }

        [Fact(DisplayName = "Has Multiple")]
		[Trait("Extensions", "Util")]
		public void HasMultiple()
        {
            var value = Profile.User | Profile.Director | Profile.Manager;

            value.Has(Profile.User).Should().Be(true, "The profile is a user too");
            value.Has(Profile.Director).Should().Be(true, "The profile is a director too");
            value.Has(Profile.Manager).Should().Be(true, "The profile is a manager too");
            value.Has(Profile.SuperUser).Should().Be(false, "The profile isn't a SuperUser");
            value.Has(Profile.Supervisor).Should().Be(false, "The profile isn't a Supervisor");
            value.Has(Profile.CEO).Should().Be(false, "The profile isn't a CEO");
        }

        [Fact(DisplayName = "Add (Cast int)")]
		[Trait("Extensions", "Util")]
		public void AddCast()
        {
            var value = Profile.User;

            value = value
                .Add(Profile.Director)
                .Add(Profile.Manager);

            var intValue = (int)value;

            intValue.Should().Be(6);
        }

        [Fact(DisplayName = "Add")]
		[Trait("Extensions", "Util")]
		public void Add()
        {
            var value = Profile.User;

            value = value
                .Add(Profile.Director)
                .Add(Profile.Manager);

            value.Has(Profile.User).Should().Be(true, "The profile is a user too");
            value.Has(Profile.Director).Should().Be(true, "The profile is a director too");
            value.Has(Profile.Manager).Should().Be(true, "The profile is a manager too");
            value.Has(Profile.SuperUser).Should().Be(false, "The profile isn't a SuperUser");
            value.Has(Profile.Supervisor).Should().Be(false, "The profile isn't a Supervisor");
            value.Has(Profile.CEO).Should().Be(false, "The profile isn't a CEO");
        }

        [Fact(DisplayName = "Add (Twice)")]
		[Trait("Extensions", "Util")]
		public void AddTwice()
        {
            var value = Profile.User;

            value = value
                .Add(Profile.Director)
                .Add(Profile.Manager);

            value = value
                .Add(Profile.Director);

            value.Has(Profile.User).Should().Be(true, "The profile is a user too");
            value.Has(Profile.Director).Should().Be(true, "The profile is a director too");
            value.Has(Profile.Manager).Should().Be(true, "The profile is a manager too");

            value.Has(Profile.SuperUser).Should().Be(false, "The profile isn't a SuperUser");
            value.Has(Profile.Supervisor).Should().Be(false, "The profile isn't a Supervisor");
            value.Has(Profile.CEO).Should().Be(false, "The profile isn't a CEO");
        }

        [Fact(DisplayName = "Remove")]
		[Trait("Extensions", "Util")]
		public void Remove()
        {
            var value = Profile.User | Profile.Director | Profile.CEO | Profile.Manager;

            value = value
                .Remove(Profile.Director);

            value.Has(Profile.User).Should().Be(true, "The profile is a user too");
            value.Has(Profile.CEO).Should().Be(true, "The profile is a CEO");
            value.Has(Profile.Manager).Should().Be(true, "The profile is a manager");

            value.Has(Profile.Director).Should().Be(false, "The profile isn't a director too");
            value.Has(Profile.SuperUser).Should().Be(false, "The profile isn't a SuperUser");
            value.Has(Profile.Supervisor).Should().Be(false, "The profile isn't a Supervisor");
        }

        [Fact(DisplayName = "Remove (Twice)")]
		[Trait("Extensions", "Util")]
		public void RemoveTwice()
        {
            var value = Profile.User | Profile.Director | Profile.CEO | Profile.Manager;

            value = value
                .Remove(Profile.Director)
                .Remove(Profile.Manager);

            value = value
                .Remove(Profile.Director);

            value.Has(Profile.User).Should().Be(true, "The profile is a user too");
            value.Has(Profile.CEO).Should().Be(true, "The profile is a CEO");

            value.Has(Profile.Director).Should().Be(false, "The profile isn't a director");
            value.Has(Profile.Manager).Should().Be(false, "The profile isn't a manager");
            value.Has(Profile.SuperUser).Should().Be(false, "The profile isn't a SuperUser");
            value.Has(Profile.Supervisor).Should().Be(false, "The profile isn't a Supervisor");
        }

        [Fact(DisplayName = "Remove (All)")]
		[Trait("Extensions", "Util")]
		public void RemoveAll()
        {
            var value = Profile.User | Profile.Director | Profile.CEO | Profile.Manager;

            value = value
                .Remove(Profile.User)
                .Remove(Profile.Director)
                .Remove(Profile.CEO)
                .Remove(Profile.Manager);

            value.Has(Profile.User).Should().Be(true, "The profile always is a user");
            value.Has(Profile.CEO).Should().Be(false, "The profile isn't a CEO");
            value.Has(Profile.Director).Should().Be(false, "The profile isn't a director");
            value.Has(Profile.Manager).Should().Be(false, "The profile isn't a manager");
            value.Has(Profile.SuperUser).Should().Be(false, "The profile isn't a SuperUser");
            value.Has(Profile.Supervisor).Should().Be(false, "The profile isn't a Supervisor");
        }
    }
}
