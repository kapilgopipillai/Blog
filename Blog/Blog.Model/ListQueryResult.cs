using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Model
{
    public class ListQueryResult<TEntity>
    {
        public ListQueryResult(IEnumerable<TEntity> payload, int totalCount)
        {
            Payload = payload;
            TotalCount = totalCount;
        }

        public readonly IEnumerable<TEntity> Payload;

        public readonly int TotalCount;
    }
}
