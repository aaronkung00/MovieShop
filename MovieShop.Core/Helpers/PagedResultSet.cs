using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Core.Helpers
{
    public class PagedResultSet<TEntity>
    {

        public PagedResultSet(IEnumerable<TEntity> data, int pageIndex, int pageSize, int count)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public int PageIndex { get; }
        public int PageSize { get; }
        public int TotalPages { get; }
        public long Count { get; }

        public IEnumerable<TEntity> Data { get; }
    }
}
