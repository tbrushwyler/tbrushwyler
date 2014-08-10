using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Engine;
using NHibernate.Id;

namespace cumulo.ninja.Infrastructure.NHibernate
{
    internal class StringIdGenerator : IIdentifierGenerator
    {
        /// <summary>
        /// Generate a new <see cref="T:System.Guid"/> using the comb algorithm and converts it to a string.
        /// </summary>
        /// <param name="session">The <see cref="T:NHibernate.Engine.ISessionImplementor"/> this id is being generated in.</param><param name="obj">The entity for which the id is being generated.</param>
        /// <returns>
        /// The new identifier as a <see cref="T:System.String"/>.
        /// </returns>
        public object Generate(ISessionImplementor session, object obj)
        {
            return (object)this.GenerateComb();
        }

        /// <summary>
        /// Generate a new <see cref="T:System.String"/> using the comb algorithm.
        /// </summary>
        private string GenerateComb()
        {
            byte[] b = Guid.NewGuid().ToByteArray();
            DateTime dateTime = new DateTime(1900, 1, 1);
            DateTime now = DateTime.Now;
            TimeSpan timeSpan = new TimeSpan(now.Ticks - dateTime.Ticks);
            TimeSpan timeOfDay = now.TimeOfDay;
            byte[] bytes1 = BitConverter.GetBytes(timeSpan.Days);
            byte[] bytes2 = BitConverter.GetBytes((long)(timeOfDay.TotalMilliseconds / 3.333333));
            Array.Reverse((Array)bytes1);
            Array.Reverse((Array)bytes2);
            Array.Copy((Array)bytes1, bytes1.Length - 2, (Array)b, b.Length - 6, 2);
            Array.Copy((Array)bytes2, bytes2.Length - 4, (Array)b, b.Length - 4, 4);
            return new Guid(b).ToString("D");
        }
    }
}
