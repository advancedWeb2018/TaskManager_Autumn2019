using System;

namespace MakeIt.DAL.EF
{
    public class Visitor
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Ip { get; set; }
        public string Url { get; set; }
        public DateTime Date { get; set; }
    }
}
