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



        public void AddEventOfType(Coach coach, DateTime Date, int MaxParticipants, EventType eventType)
        {
            Event @event = new Event(coach, Date, MaxParticipants);
            @event.EventType = eventType;
            eventDataAccesLayer.InsertEvent(@event);
        }
        public void AddParticipantToEvent(Event eventToAddParticipant, List<Participant> participants)
        {
            eventToAddParticipant.Participants.AddRange(participants);
            eventDataAccesLayer.UpdateEvent(eventToAddParticipant);
        }
        public void RemoveParticipantFromEvent(Event eventToRemoveParticipant, Participant participant)
        {
            eventToRemoveParticipant.Participants.Remove(participant);
            eventDataAccesLayer.UpdateEvent(eventToRemoveParticipant);
        }
        public void UpdateEvent(Event eventToUpdate)
        {
            eventDataAccesLayer.UpdateEvent(eventToUpdate);
        }
        public void RemoveEvent(Event eventToRemove)
        {
            eventDataAccesLayer.DeleteEvent(eventToRemove);
        }
        public Event? GetEventByName(string name)
        {
            return eventDataAccesLayer.GetEventByName(name);
        }



        public void AddEventType(string name, string description, double expPerParticipant)
        {
            EventType eventType = new EventType(name, description, expPerParticipant);
            eventDataAccesLayer.InsertEventType(eventType);
        }
        public void UpdateEventType(EventType eventType)
        {
            eventDataAccesLayer.UpdateEventType(eventType);
        }
        public void RemoveEventType(EventType eventType) 
        {
            eventDataAccesLayer.DeleteEventType(eventType);
        }
        public EventType? GetEventTypeByName(string name)
        {
            return eventDataAccesLayer.GetEventTypeByName(name);
        }



        public List<Event> GetAllEvents()
        {
            return eventDataAccesLayer.GetAllEvents();
        }
        public List<EventType> GetAllEventTypes() 
        {
            return eventDataAccesLayer.GetAllEventTypes();
        }

        public EventType GetEventTypeByID(int id)
        {
            return eventDataAccesLayer.GetEventTypeById(id);
        }

    }
}
