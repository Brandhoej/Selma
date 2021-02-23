using Selma.Core.Application.Abstractions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Selma.Core.Application
{
    internal class ActorComparer
        : IComparer
        , IComparer<IActor>
    {
        public int Compare(object x, object y)
            => Compare(x as IActor, y as IActor);

        public int Compare(IActor x, IActor y)
        {
            if (x.Equals(y))
            {
                return 0;
            }

            ICollection<IActor> common = new List<IActor>(x.Intersect(y));

            if (x.Contains(y))
            {
                common.Add(y);
            }
            else if(y.Contains(x))
            {
                common.Add(x);
            }

            /// If the <see cref="ICollection.Count"/> of <see cref="common"/> is 0, then they are disjoint.
            if (common.Count == 0)
            {
                return 0;
            }

            /// If the first <see cref="common"/> is closest to <see cref="this"/> then 1; otherwise, -1.
            IActor closests = common.First();
            int distX = Distance(closests, x);
            int distY = Distance(closests, y);
            return distX.CompareTo(distY);
        }

        private int Distance(IActor to, IActor from)
        {
            int distance = 1;
            foreach (IActor successor in from)
            {
                if (successor.Equals(to))
                {
                    return distance;
                }
            }
            return -1;
        }
    }
}
