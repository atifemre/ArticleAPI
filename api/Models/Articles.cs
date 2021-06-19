using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Articles
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ArticleContent { get; set; }
        public short StarCount { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime UpdateDate { get; set; }

        public List<Reviews> Reviews { get; set; }
    }
}
