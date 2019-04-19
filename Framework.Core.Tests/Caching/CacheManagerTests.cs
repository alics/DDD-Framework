using Framework.Core.Caching;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Tests.Caching
{
    [TestClass]
    public class CacheManagerTests
    {
        [TestMethod]
        public void Set_HelloString_ReturnHelloInGet()
        {
            //arrange
            var cacheManager = new MemoryCacheManager();

            //act
            cacheManager.Set("key1", "Hello", 10000);
            var result = cacheManager.Get<string>("key1");
            //assert
            Assert.AreEqual("Hello", result);
        }
    }
}
