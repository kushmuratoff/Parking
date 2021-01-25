//brr
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication_Parkovka.Models
{
    public class baza
    {
        public int? name { get; set; }
        public int? kuni { get; set; }
        public DateTime? kel { get; set; }
        public DateTime? ket { get; set; }
        public string FISH { get; set; }
        public string nomer { get; set; }
        public int? joyi { get; set; }
        public int? narx { get; set; }
       
        public int? summa { get; set; }


    }
    public class Rollar
    {
        public int Id { get; set; }
        public string Nomi { get; set; }
        public ICollection<Userlar> Userlarlar { get; set; }
    }
    public class Userlar
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Parol { get; set; }
        public int? RollarId { get; set; }
        public Rollar Rollar { get; set; }
        public ICollection<Xodim> Xodimlar { get; set; }

    }
    public class Xodim
    {
        public int Id { get; set; }
        public string FISH { get; set; }
        public string MoshinaR { get; set; }
        public int? UserlarId { get; set; }
        public Userlar Userlar { get; set; }
        public int? JoylarId { get; set; }
        public Joylar Joylar { get; set; }
        public int? Holat { get; set; }
        public ICollection<JadvalXodim> JadvalXodimlar { get; set; }
    }
    public class Narx
    {
        public int Id { get; set; }
        public int Summa { get; set; }
        public ICollection<Joylar> Joylarlar { get; set; }
    }
    public class Joylar
    {
        public int Id { get; set; }
        public int Nomeri { get; set; }
        public int? NarxId { get; set; }
        public Narx Narx { get; set; }
        public int Holati { get; set; }
        public ICollection<JadvalFoy> JadvalFoylar { get; set; }
        public ICollection<Xodim> Xodimlar { get; set; }
    }
    public class JadvalFoy
    {
        public int Id { get; set; }
        public string FoyFISH { get; set; }
        public string MoshinaN { get; set; }
        public DateTime? KelishV { get; set; }
        public DateTime? KetishV { get; set; }
        public int? JoylarId { get; set; }
        public Joylar Joylar { get; set; }

    }
    public class JadvalXodim
    {
        public int Id { get; set; }
        public int? XodimId { get; set; }
        public Xodim Xodim { get; set; }
        public DateTime? KelishV { get; set; }
        public DateTime? KetishV { get; set; }      

    }
}