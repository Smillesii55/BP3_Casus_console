using BP3_Casus_console.Users.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BP3_Casus_console.Events;
using BP3_Casus_console.Users;

namespace BP3_Casus_console.Events.Service
{
    public class EventService
    {
        EventDataAccesLayer eventDataAccesLayer = EventDataAccesLayer.Instance;

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

        public Event CreateEvent(string name, double expPerParticipant)
        {
            Event newEvent = new Event(name, expPerParticipant);
            eventDataAccesLayer.InsertEvent(newEvent);
            return newEvent;
        }
        public void UpdateEvent(Event updatedEvent)
        {
            eventDataAccesLayer.UpdateEvent(updatedEvent);
        }
        public void DeleteEvent(Event eventToDelete)
        {
            eventDataAccesLayer.DeleteEvent(eventToDelete);
        }

        public Event GetEventById(int eventId)
        {
            Event? eventToGet = eventDataAccesLayer.GetEventById(eventId);
            if (eventToGet != null)
            {
                return eventToGet;
            }
            else
            {
                return null;
            }
        }
        public Event GetEventByName(string eventName)
        {
            Event? eventToGet = eventDataAccesLayer.GetEventByName(eventName);
            if (eventToGet != null)
            {
                return eventToGet;
            }
            else
            {
                return null;
            }
        } 
        
        public List<Event> GetEventsByTag(string tag)
        {
            List<Event>? events = eventDataAccesLayer.GetEventsByTag(tag);
            if (events != null)
            {
                return events;
            }
            else
            {
                return new List<Event>();
            }
        }
        public List<Event> GetEvents()
        {
            List<Event>? events = eventDataAccesLayer.GetEvents();
            if (events != null)
            {
                return events;
            }
            else
            {
                return new List<Event>();
            }
        }

        public void AddTagToEvent(Event @event, string tag)
        {
            @event.Tags.Add(tag);
            UpdateEvent(@event);
        }
        public void RemoveTagFromEvent(Event @event, string tag)
        {
            @event.Tags.Remove(tag);
            UpdateEvent(@event);
        }
    }
}
