using System;

namespace razorweb.Helpers
{
    public class PagingModel 
    {
        public int CurrentPage { get; set; }
        public int CountPages { get; set; }

        public required Func<int?, string> GenerateUrl { get; set; }

    }
}