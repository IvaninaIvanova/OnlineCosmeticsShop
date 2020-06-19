using System;
using System.Collections.Generic;
using System.Text;

namespace OC.Model.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
