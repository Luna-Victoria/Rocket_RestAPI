using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Rocket.Models;
using System;


namespace Rocket.Controllers {
    [Route ("api/interventions")]
    [ApiController]


    public class InterventionsController : ControllerBase {
        private readonly LunaContext _context;
        public InterventionsController (LunaContext context) {
            _context = context;
        }

        // DateTime saveNow = DateTime.Now;

        // GET api/interventions/5
        [HttpGet ("{id}", Name = "GetInterventions")]

        public ActionResult GetById (string Status, long id) {
            var item = _context.Interventions.Find (id);
            if (item == null) {
                return NotFound ("Not Found");
            }
            var json = new JObject ();
            json["status"] = item.Status;
            return Content (json.ToString (), "application/json");
        }

        // Get api/interventions/list
        [HttpGet]
        public ActionResult<List<Interventions>> GetAll () {
            var list = _context.Interventions.ToList ();
            if (list == null) {
                return NotFound ("Not Found");
            }
            List<Interventions> list_pending = new List<Interventions> ();

            foreach (var e in list) {
                
                if (e.Status == "Pending" || e.StartingDate == null) {
                    list_pending.Add (e);
                }
            }
            return list_pending;
        }

        // PUT api/interventions/5
        [HttpPut ("inprogress/{id}")]
        public string UpdateInProgress (long id) {
            var inters = _context.Interventions.Find(id);
            if (inters == null) {
                return "Please enter an existing intervention id" ;
            }
            if (inters.Status != "Pending") {
                return "Please choose a Pending intervention";
            }
            else {
                inters.Status = "In Progress";

                string StartingDate = DateTime.Now.ToString("yyyy/MM/dd H:mm:ss");
                inters.StartingDate = StartingDate;

                _context.Interventions.Update (inters);
                _context.SaveChanges ();
                return "The intervention #" + inters.Id + " status has been successufully changed to In Progress at " + inters.StartingDate;
            }   
        }

        
        // PUT api/interventions/5
        [HttpPut ("completed/{id}")]
        public string UpdateCompleted (long id) {
            var interc = _context.Interventions.Find(id);
            if (interc == null) {
                return "Please enter an existing intervention id" ;
            }
            if (interc.Status != "In Progress") {
                return "Please choose a In Progress intervention";
            }
            else {
                interc.Status = "Completed";

                string EndingDate = DateTime.Now.ToString("yyyy/MM/dd H:mm:ss");
                interc.EndingDate = EndingDate;

                _context.Interventions.Update (interc);
                _context.SaveChanges ();
                return "The intervention #" + interc.Id + " status has been successufully changed to Completed at " + interc.EndingDate;
            }   
        }
    }
}