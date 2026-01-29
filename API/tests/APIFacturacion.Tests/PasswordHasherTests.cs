using Xunit;
using APIFacturacion.Encryption;

namespace APIFacturacion.Tests
{
    public class PasswordHasherTests
    {
        [Fact]
        public void Hash_ShouldReturnEncodedString()
        {
            var hasher = new PasswordHasher();
            string password = "testpassword";
            string hash = hasher.Hash(password);

            Assert.NotEqual(password, hash);
            Assert.NotEmpty(hash);
        }

        [Fact]
        public void Verify_ShouldReturnTrue_ForCorrectPassword()
        {
            var hasher = new PasswordHasher();
            string password = "testpassword";
            string hash = hasher.Hash(password);

            bool result = hasher.Verify(password, hash);

            Assert.True(result);
        }

        [Fact]
        public void Verify_ShouldReturnFalse_ForIncorrectPassword()
        {
            var hasher = new PasswordHasher();
            string password = "testpassword";
            string hash = hasher.Hash(password);

            bool result = hasher.Verify("wrongpassword", hash);

            Assert.False(result);
        }
    }
}

