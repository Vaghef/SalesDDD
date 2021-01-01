using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }        
        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }
        public DateTime LastModified { get; set; }
        public DateTime AddedDate { get; set; }
        public string SystemUserId { get; set; }
        public string Title { get; set; }
    }
    public class Unit : BaseEntity { }
    public class Brand : BaseEntity { }
}
