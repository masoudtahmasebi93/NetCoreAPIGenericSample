using System;
using System.Collections.Generic;

namespace NetCoreAPIGenericSample.Models
{
    public partial class Books
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
