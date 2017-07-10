using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AnglicareAPIServer.Models
{
    [Table("TokensManager")]
    public class TokensManager
    {
        [Key]
        public int TokenID { get; set; }
        public string TokenKey { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CompanyID { get; set; }
    }
}