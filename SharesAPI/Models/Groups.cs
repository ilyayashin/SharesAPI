using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SharesAPI.Models
{
    public class Groups
    {
        [Key]
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public ICollection<Shares> Shares { get; set; }
        public Groups()
        {
            Shares = new List<Shares>();
        }
    }
}
