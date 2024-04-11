﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP3_Casus_console.Events
{
    public class EventPlanner
    {
        public int EventID { get; set; }
        public DateTime Date { get; set; }
        public decimal Time { get; set; }
        public string NameCoach { get; set; }

        public EventPlanner(int eventID, DateTime date, decimal time, string nameCoach)
        {
            EventID = eventID;
            Date = date;
            Time = time;
            NameCoach = nameCoach;
        }
    }
}
