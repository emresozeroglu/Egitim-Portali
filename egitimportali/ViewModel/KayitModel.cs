using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace egitimportali.ViewModel
{
    public class KayitModel
    {
        public string kayitId { get; set; }
        public string kayitKursId { get; set; }
        public string kayitOgrId { get; set; }
        public OgrenciModel OgrBilgi { get; set; }
        public KurslarModel KurslarBilgi { get; set; }
    }
}