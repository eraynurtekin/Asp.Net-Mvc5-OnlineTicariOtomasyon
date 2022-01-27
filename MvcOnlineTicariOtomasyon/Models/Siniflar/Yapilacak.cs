using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Yapilacak
    {
        [Key]
        public int YapilacakId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string Baslik { get; set; }
       
        [Column(TypeName = "bit")]
        public bool Durum { get; set; }
        
    }
}