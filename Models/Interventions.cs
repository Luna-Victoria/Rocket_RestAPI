using System;
using System.Collections.Generic;

namespace Rocket.Models
{
    public partial class Interventions
    {
        public long Id { get; set; }
        public string Author { get; set; }
        public long? CustomerId { get; set; }
        public long? BuildingId { get; set; }
        public long? BatteryId { get; set; }
        public long? ColumnId { get; set; }
        public long? ElevatorId { get; set; }
        public string Report { get; set; }
        public long? EmployeeId { get; set; }
        public string Result { get; set; }
        public string Status { get; set; }
        public string StartingDate { get; set; }
        public string EndingDate { get; set; }

        public Batteries Battery { get; set; }
        public Buildings Building { get; set; }
        public Columns Column { get; set; }
        public Customers Customer { get; set; }
        public Elevators Elevator { get; set; }
        public Employees Employee { get; set; }
    }
}
