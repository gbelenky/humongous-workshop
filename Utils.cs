using System.Collections.Generic;

namespace Humongous.Health
{
    public class Utils
    {
            public static int Count(IEnumerable<HealthCheck> ratings)
        {
            int c = 0;
            using (var e = ratings.GetEnumerator())
            {
                while (e.MoveNext())
                    c++;
            }
            return c;
        }
    }
}