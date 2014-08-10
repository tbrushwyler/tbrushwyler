using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace cumulo.ninja.Core.Domain
{
    [Serializable]
    [DataContract]
    abstract public class EntityBase<T> : EntityBase<T, long>, IEntity<T> where T : IEntity<T>, IEntity<T, long>
    {

    }

    [Serializable]
    [DataContract]
    abstract public class EntityBase<T, TKey> : IEntity<T, TKey> where T : IEntity<T, TKey>
    {
        [DataMember]
        public virtual TKey Id { get; protected set; }

        public override bool Equals(object obj)
        {
            var other = obj as EntityBase<T, TKey>;
            if (other == null) return false;
            if (IsNew() && other.IsNew())
                return ReferenceEquals(this, other);
            return Id.Equals(other.Id);
        }

        private int? _oldHashCode;
        public override int GetHashCode()
        {
            if (_oldHashCode.HasValue)
                return _oldHashCode.Value;
            if (!IsNew()) return Id.GetHashCode();
            _oldHashCode = base.GetHashCode();
            return _oldHashCode.Value;
        }

        public static bool operator ==(EntityBase<T, TKey> lhs, EntityBase<T, TKey> rhs)
        {
            return Equals(lhs, rhs);
        }

        public static bool operator !=(EntityBase<T, TKey> lhs, EntityBase<T, TKey> rhs)
        {
            return !Equals(lhs, rhs);
        }

        public virtual bool IsNew()
        {
            return Id.Equals(default(TKey));
        }
    }

    [Serializable]
    [DataContract]
    public class StringEntityBase<T> : IEntity<T> where T : IEntity<T>
    {
        [DataMember]
        public virtual String Id { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as StringEntityBase<T>;
            if (other == null) return false;
            if (IsNew() && other.IsNew())
                return ReferenceEquals(this, other);
            return Id.Equals(other.Id);
        }

        private int? _oldHashCode;
        public override int GetHashCode()
        {
            if (_oldHashCode.HasValue)
                return _oldHashCode.Value;
            if (IsNew())
            {
                _oldHashCode = base.GetHashCode();
                return _oldHashCode.Value;
            }
            return Id.GetHashCode();
        }

        public static bool operator ==(StringEntityBase<T> lhs, StringEntityBase<T> rhs)
        {
            return Equals(lhs, rhs);
        }

        public static bool operator !=(StringEntityBase<T> lhs, StringEntityBase<T> rhs)
        {
            return !Equals(lhs, rhs);
        }

        public virtual bool IsNew()
        {
            return String.IsNullOrEmpty(Id);
        }
    }
}
