using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using NewBookingofmeetingrooms;

namespace NewBookingofmeetingrooms.ControllersApi
{
    public class ServicesController : ApiController
    {
        private BookingOfMeetingRoomsDBEntities db = new BookingOfMeetingRoomsDBEntities();

        // GET: api/Services
        public IQueryable<Services> GetServices()
        {
            return db.Services;
        }

        // GET: api/Services/5
        public IHttpActionResult GetServices(int id)
        {
            var services = db.Services.Where(p=>p.IdUser==id);
            if (services == null)
            {
                return NotFound();
            }


            return Ok(services);
        }

        // PUT: api/Services/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutServices(int id, Services services)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != services.Id)
            {
                return BadRequest();
            }

            db.Entry(services).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Services
        [ResponseType(typeof(Services))]
        public IHttpActionResult PostServices(Services services)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Services.Add(services);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = services.Id }, services);
        }

        // DELETE: api/Services/5
        [ResponseType(typeof(Services))]
        public IHttpActionResult DeleteServices(int id)
        {
            Services services = db.Services.Find(id);
            if (services == null)
            {
                return NotFound();
            }

            db.Services.Remove(services);
            db.SaveChanges();

            return Ok(services);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ServicesExists(int id)
        {
            return db.Services.Count(e => e.Id == id) > 0;
        }
    }
}