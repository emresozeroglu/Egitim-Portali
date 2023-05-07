using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using egitimportali.Models;
using egitimportali.ViewModel;

namespace egitimportali.Controllers
{
    public class ServisController : ApiController
    {
        DBEntities db = new DBEntities();
        SonucModel sonuc = new SonucModel();

        #region Kurslar

        [HttpGet]
        [Route("api/kurslarliste")]

        public List<KurslarModel> KurslarListe()
        {
            List<KurslarModel> liste = db.Kurslar.Select(x => new KurslarModel()
            {
                kursId = x.kursId,
                kursKodu = x.kursKodu,
                kursAdi = x.kursAdi,
                kursKredi = x.kursKredi

            }).ToList();
            return liste;
        }


        [HttpGet]
        [Route("api/kurslarbyid/{dersId}")]

        public KurslarModel kurslarbyid(string kursId)
        {
            KurslarModel kayit = db.Kurslar.Where(s => s.kursId == kursId).Select(x => new KurslarModel()
            {
                kursId = x.kursId,
                kursKodu = x.kursKodu,
                kursAdi = x.kursAdi,
                kursKredi = x.kursKredi

            }).SingleOrDefault();
            return kayit;

        }

        [HttpPost]
        [Route("api/kurslarekle")]

        public SonucModel KurslarEkle(KurslarModel model)
        {
            if (db.Kurslar.Count(s => s.kursKodu == model.kursKodu) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = " Girilen Kurs Kodu Kayıtlıdır!";
                return sonuc;
            }
            Kurslar yeni = new Kurslar();
            yeni.kursId = Guid.NewGuid().ToString();
            yeni.kursKodu = model.kursKodu;
            yeni.kursAdi = model.kursAdi;
            yeni.kursKredi = model.kursKredi;
            db.Kurslar.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kurs Eklendi";
            return sonuc;
        }
        [HttpPut]
        [Route("api/kurslarduzenle")]

        public SonucModel kurslarduzenle(KurslarModel model)
        {
            Kurslar kayit = db.Kurslar.Where(s => s.kursId == model.kursId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kurs Bulunamadı";
                return sonuc;
            }

            kayit.kursKodu = model.kursKodu;
            kayit.kursAdi = model.kursAdi;
            kayit.kursKredi = model.kursKredi;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kurs Düzenlendi";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/kurslarsil/{kursId}")]
        public SonucModel KurslarSil(string kursId)
        {
            Kurslar kayit = db.Kurslar.Where(s => s.kursId == kursId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kurs Bulunamadı";
                return sonuc;
            }

            if (db.Kayit.Count(s => s.kayitKursId == kursId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kursa Kayıtlı Öğrenci Olduğu İçin Silinemez";
                return sonuc;
            }

            db.Kurslar.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kurs Silindi";
            return sonuc;
        }
        #endregion

        #region Ogrenci

        [HttpGet]
        [Route("api/ogrenciliste")]

        public List<OgrenciModel> OgrenciListe()
        {
            List<OgrenciModel> liste = db.Ogrenci.Select(x => new OgrenciModel()
            {
                ogrId = x.ogrId,
                ogrNo = x.ogrNo,
                ogrAdsoyad = x.ogrAdsoyad,
                ogrDogTarih = x.ogrDogTarih,
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/ogrencibyid/{ogrId}")]

        public OgrenciModel OgrenciById(string ogrId)
        {
            OgrenciModel kayit = db.Ogrenci.Where(s => s.ogrId == ogrId).Select(x => new OgrenciModel()
            {
                ogrId = x.ogrId,
                ogrNo = x.ogrNo,
                ogrAdsoyad = x.ogrAdsoyad,
                ogrDogTarih = x.ogrDogTarih,
            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/ogrenciekle")]
        public SonucModel OgrenciEkle(OgrenciModel model)
        {
            if (db.Ogrenci.Count(s => s.ogrNo == model.ogrNo) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Öğrenci Numarası Kayıtlıdır";
            }
            Ogrenci yeni = new Ogrenci();
            yeni.ogrId = Guid.NewGuid().ToString();
            yeni.ogrNo = model.ogrNo;
            yeni.ogrAdsoyad = model.ogrAdsoyad;
            yeni.ogrDogTarih = model.ogrDogTarih;
            db.Ogrenci.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Öğrenci Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/ogrenciduzenle")]

        public SonucModel OgrenciDuzenle(OgrenciModel model)
        {
            Ogrenci kayit = db.Ogrenci.Where(s => s.ogrId == model.ogrId).SingleOrDefault();
            if (kayit ==null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Öğrenci Bulunamadı";
                return sonuc;
            }
            kayit.ogrNo = model.ogrNo;
            kayit.ogrAdsoyad = model.ogrAdsoyad;
            kayit.ogrDogTarih = model.ogrDogTarih;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Öğrenci Bilgileri Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/ogrencisil/{ogrId}")]
        public SonucModel OgrenciSil(string ogrId)
        {
            Ogrenci kayit = db.Ogrenci.Where(s => s.ogrId == ogrId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Öğrenci Bulunamadı";
                return sonuc;
            }
            if (db.Kayit.Count(s => s.kayitOgrId == ogrId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Öğrenci Kursa Kayıtlı";
                return sonuc;
            }

            db.Ogrenci.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Öğrenci Bilgileri Silindi";
            return sonuc;
        }

        #endregion

        #region Kayıt
        [HttpGet]
        [Route("api/ogrecikurslarliste/{ogrId}")]

        public List<KayitModel> OgrenciKurslarListe(string ogrId)
        {
            List<KayitModel> liste = db.Kayit.Where(s => s.kayitOgrId == ogrId).Select(x => new KayitModel()
            {
                kayitId = x.kayitId,
                kayitKursId = x.kayitKursId,
                kayitOgrId = x.kayitOgrId,
            }).ToList();
            foreach (var kayit in liste)
            {
                kayit.OgrBilgi = OgrenciById(kayit.kayitOgrId);
                kayit.KurslarBilgi = kurslarbyid(kayit.kayitKursId);
            }
            return liste;
        }

        [HttpGet]
        [Route("api/kurslarogrenciliste/{kursId}")]

        public List<KayitModel> kurslarogrenciliste(string ogrId)
        {
            List<KayitModel> liste = db.Kayit.Where(s => s.kayitKursId == ogrId).Select(x => new KayitModel()
            {
                kayitId = x.kayitId,
                kayitKursId = x.kayitKursId,
                kayitOgrId = x.kayitOgrId,
            }).ToList();
            foreach (var kayit in liste)
            {
                kayit.OgrBilgi = OgrenciById(kayit.kayitOgrId);
                kayit.KurslarBilgi = kurslarbyid(kayit.kayitKursId);
            }
            return liste;
        }

        [HttpPost]
        [Route("api/kayitekle")]
        public SonucModel KayitEkle(KayitModel model)
        {   
            if(db.Kayit.Count(s => s.kayitKursId == model.kayitKursId && s.kayitOgrId == model.kayitOgrId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Bu Öğrenci Bu Kursa Kayıtlıdır";
                return sonuc;

            }

            Kayit yeni = new Kayit();
            yeni.kayitId = Guid.NewGuid().ToString();
            yeni.kayitOgrId = model.kayitOgrId;
            yeni.kayitKursId = model.kayitKursId;
            db.Kayit.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Öğrenci Kursa Kayıt Edildi";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/kayitsil/{kayitId}")]
        public SonucModel KayitSil(string kayitId)
        {
            Kayit kayit = db.Kayit.Where(s => s.kayitId == kayitId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı";
                return sonuc;
            }
            db.Kayit.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kursa Kydı Silindi";

            return sonuc;
        }

        #endregion

    }
}
