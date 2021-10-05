using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datebook
{
    class Program
    {
        static void Main(string[] args)
        {
            Datebook datebook = new Datebook();
            datebook.Events.Add(new Event(new DateTime(1982,09,28,12,45,0), new TimeSpan(1,30,0), "Тестовое событие", "Санкт-Петербург, 9-я Красноармейская"));


            datebook.Events[0].WriteFullEvent();
            
        }
    }
}
