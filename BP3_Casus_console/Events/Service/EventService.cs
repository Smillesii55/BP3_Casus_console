using BP3_Casus_console.Users.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BP3_Casus_console.Events;

namespace BP3_Casus_console.Events.Service
{
    public class EventService
    {
        private EventService()
        {
        }
        private static EventService? instance = null;
        public static EventService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EventService();
                }
                return instance;
            }
        }

        public void CreateEvent(Event newEvent)
        {
            EventDataAccesLayer.Instance.InsertEvent(newEvent);
        }
    }
}
