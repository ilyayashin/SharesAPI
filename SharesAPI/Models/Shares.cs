using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SharesAPI.Models
{
    public class Shares
    {
        [Key]
        public int ShareId { get; set; }
        public string ShareName { get; set; }

        public int GroupsGroupId { get; set; }
        //[ForeignKey("GroupsInfoKey")]
        public Groups Groups { get; set; }
    }
}
