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

        public EventType? CreateEventType(string name, string description)
        {
            // Create a new event type
            // eventDataAccesLayer.InsertEventType(newEventType);
            // return newEventType;
            return null;
        }
        public Event? CreateEvent(string name, double expPerParticipant)
        {
            // Create a new event
            // eventDataAccesLayer.InsertEvent(newEvent);
            // return newEvent;
            return null;
        }
        
        public void UpdateEventType(EventType updatedEventType)
        {
            eventDataAccesLayer.UpdateEventType(updatedEventType);
        }
        public void UpdateEvent(Event updatedEvent)
        {
            eventDataAccesLayer.UpdateEvent(updatedEvent);
        }


        public void DeleteEventType(EventType eventTypeToDelete)
        {
            eventDataAccesLayer.DeleteEventType(eventTypeToDelete);
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
        public EventType GetEventTypeByName(string eventTypeName)
        {
            EventType? eventTypeToGet = eventDataAccesLayer.GetEventTypeByName(eventTypeName);
            if (eventTypeToGet != null)
            {
                return eventTypeToGet;
            }
            else
            {
                return null;
            }
        } 
        

        public List<EventType> GetEventTypesByTag(string tag)
        {
            List<EventType>? eventtypes = eventDataAccesLayer.GetEventTypesByTag(tag);
            if (eventtypes != null)
            {
                return eventtypes;
            }
            else
            {
                return new List<EventType>();
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
        public List<EventType> GetEventTypes()
        {
            List<EventType>? eventtypes = eventDataAccesLayer.GetEventTypes();
            if (eventtypes != null)
            {
                return eventtypes;
            }
            else
            {
                return new List<EventType>();
            }
        }


        public void AddTagToEventType(EventType eventType, string tag)
        {

        }
        public void RemoveTagFromEvent(EventType eventType, string tag)
        {

        }

        public void AddParticipantToEvent(Event @event, Participant participant)
        {
            if (@event.Participants.Count < @event.MaxParticipants)
            {
                @event.Participants.Add(participant);
            }
        }
        public void RemoveParticipantFromEvent(Event @event, Participant participant)
        {
            @event.Participants.Remove(participant);
        }
    }
}
