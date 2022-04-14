using System;
using Framework.Core;

namespace Framework.Snowflake
{
    public class SnowflakeIdGenerator : IIdGenerator
    {
        private static IdGen.IdGenerator _idGen = new IdGen.IdGenerator(0);

        public long Create()
        {
            return _idGen.CreateId();
        }
    }
}
