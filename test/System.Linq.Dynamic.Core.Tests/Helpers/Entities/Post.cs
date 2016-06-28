﻿
namespace System.Linq.Dynamic.Core.Tests.Helpers.Entities
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }

        public int NumberOfReads { get; set; }

#if NET4 || NET452
        public DateTime PostDate { get; set; }
#else
        public DateTimeOffset PostDate { get; set; }
#endif
    }
}