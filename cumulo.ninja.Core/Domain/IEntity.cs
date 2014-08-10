using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cumulo.ninja.Core.Domain
{
    public interface IEntity<T> : IEntity where T : IEntity<T>
    { }

    public interface IEntity<T, TKey> : IEntity where T : IEntity<T, TKey>
    {
        TKey Id { get; }
    }

    public interface IEntity
    {
        bool IsNew();
    }
}
