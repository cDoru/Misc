using NUnit.Framework;
using RestWrap.Interfaces;

namespace RestWrap.Test
{
    public class RequestTests
    {
        [Test]
        public void RequestIsIRequest()
        {
            var sut = new Request();
            Assert.IsInstanceOf<IRequest>(sut);
        }



    }
}
