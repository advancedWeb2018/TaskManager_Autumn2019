using MakeIt.DAL.Common;
using System;

namespace MakeIt.DAL.EF
{
    public class ExceptionDetail : AuditableEntity<int>
    {
        public string ExceptionMessage { get; set; }    // сообщение об исключении
        public string ControllerName { get; set; }  // контроллер, где возникло исключение
        public string ActionName { get; set; }  // действие, где возникло исключение
        public string StackTrace { get; set; }  // стек исключения
        public DateTime Date { get; set; }  // дата и время исключения
    }
}
