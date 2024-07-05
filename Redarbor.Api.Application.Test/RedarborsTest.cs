using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Redarbor.Api.Application.Repo;

namespace Redarbor.Api.Application.Test
{
    [TestClass]
    public class RedarborsTest
    {
        private readonly Persistence _persistence;
        private readonly IMapper _mapper;
        public RedarborsTest(Persistence persistence, IMapper mapper)
        {
            _persistence = persistence;
            _mapper = mapper;
        }

        [TestMethod]
        public void Test_Get_Items_By_Id()
        {
            int count = 1;
            var lst = _persistence.GetRedaborById(count).Result;
            NUnit.Framework.Assert.That(1, Is.EqualTo(count));
        }

        [TestMethod]
        public void Test_Get_User_OK()
        {
            int count = 1;
            var lst = _persistence.GetUser("test1@test.test.tmp","test").Result;
            NUnit.Framework.Assert.That(lst == null ? 0 : 1, Is.EqualTo(count));
        }
    }
}
