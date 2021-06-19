using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Reviews
    {  
        public long Id { get; set; }
        public long ArticlesId { get; set; }
     //   public Articles Articles { get; set; }

        public string Reviewer { get; set; }
        public string ReviewerContent { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime UpdateDate { get; set; }

    }
}
