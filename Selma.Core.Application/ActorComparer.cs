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
            ICollection<IActor> common = new List<IActor>(x.Intersect(y));

            /// If the <see cref="ICollection.Count"/> of <see cref="common"/> is 0, then they are disjoint.
            if (common.Count == 0)
            {
                return 0;
            }

            /// If the first <see cref="common"/> is closest to <see cref="this"/> then 1; otherwise, -1.
            IActor closests = common.First();
            return Distance(closests, x).CompareTo(Distance(closests, y));
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
