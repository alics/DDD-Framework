using Xunit;

namespace Framework.Snowflake.Tests
{
    public class SnowflakeIdGeneratorTests
    {
        [Fact]
        public void generate_id()
        {
            var idGen = new Framework.Snowflake.SnowflakeIdGenerator();
            var id = idGen.Create();

            Assert.NotEqual(default, id);
        }
    }
}
