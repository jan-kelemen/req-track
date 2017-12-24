using ReqTrack.Domain.Core.Entities;
using Xunit;

namespace Domain.Core.Test.Unit.Entities
{

    public class IdentityUnitTests
    {
        [Fact]
        public void BlankIdentitiesDontMatch()
        {
            var id1 = Identity.BlankIdentity;
            var id2 = Identity.BlankIdentity;

            Assert.NotEqual(id1, id2);
            Assert.False(id1 == id2);
            Assert.True(id1 != id2);
        }

        [Fact]
        public void ConversionsFromStringToIdentityWork()
        {
            const string str = "identity string";

            Identity factoryMethodCreatedIdentity = Identity.FromString(str);
            Identity implicitConversionCreatedIdentity = str;

            Assert.Equal("identity string", factoryMethodCreatedIdentity.ToString());
            Assert.Equal("identity string", implicitConversionCreatedIdentity);
        }

        [Theory]
        [InlineData("id1", "id1", true)]
        [InlineData("id1", "id2", false)]
        public void IdentityEqualityTest(string first, string second, bool expected)
        {
            var id1 = Identity.FromString(first);
            var id2 = Identity.FromString(second);

            Assert.Equal(expected, id1 == id2);
        }
    }
}
