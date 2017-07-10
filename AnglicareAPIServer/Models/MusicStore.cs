using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnglicareAPIServer.Models
{
    [Table("MusicStore")]
    public class MusicStore
    {
        [Key]
        public int MusicID { get; set; }
        public string MusicName { get; set; }
        public DateTime MusicRelease { get; set; }
        public string MovieName { get; set; }
        
    }
}