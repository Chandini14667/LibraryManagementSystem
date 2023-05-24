using FluentAssertions;
using Moq;

namespace LibraryManagement.Tests
{
    public class Tests
    {
        [Test]
        public void LoginUser_WhenCalled_ByCheckingUserPasswordReturnsTrue()
        {
            var _loginmock = new Mock<ILoginUser>();
            _loginmock.Setup(a => a.LoginUser()).Returns(true);
            var result = _loginmock.Object.LoginUser();
            result.Should().Be(true);
        }

        [Test]
        public void LoginUser_WhenCalled_ByCheckingUserPasswordReturnsFalse()
        {
            var _loginmock = new Mock<ILoginUser>();
            _loginmock.Setup(a => a.LoginUser()).Returns(false);
            var result = _loginmock.Object.LoginUser();
            result.Should().Be(false);
        }
    }
}