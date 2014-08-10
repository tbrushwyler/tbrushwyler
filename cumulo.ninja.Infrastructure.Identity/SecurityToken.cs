using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cumulo.ninja.Infrastructure.Identity
{
    internal sealed class SecurityToken
    {
        private readonly byte[] _data;

        public SecurityToken(byte[] data)
        {
            this._data = (byte[])data.Clone();
        }

        internal byte[] GetDataNoClone()
        {
            return this._data;
        }
    }
}
