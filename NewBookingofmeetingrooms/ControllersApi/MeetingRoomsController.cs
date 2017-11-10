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
    public class MeetingRoomsController : ApiController
    {
        private BookingOfMeetingRoomsDBEntities db = new BookingOfMeetingRoomsDBEntities();

        // GET: api/MeetingRooms
        public IQueryable<MeetingRooms> GetMeetingRooms()
        {
            return db.MeetingRooms;
        }

        // GET: api/MeetingRooms/5
        [ResponseType(typeof(MeetingRooms))]
        public IHttpActionResult GetMeetingRooms(int id)
        {
            MeetingRooms meetingRooms = db.MeetingRooms.Find(id);
            if (meetingRooms == null)
            {
                return NotFound();
            }

            return Ok(meetingRooms);
        }

        // PUT: api/MeetingRooms/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMeetingRooms(int id, MeetingRooms meetingRooms)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != meetingRooms.Id)
            {
                return BadRequest();
            }

            db.Entry(meetingRooms).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeetingRoomsExists(id))
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

        // POST: api/MeetingRooms
        [ResponseType(typeof(MeetingRooms))]
        public IHttpActionResult PostMeetingRooms(MeetingRooms meetingRooms)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MeetingRooms.Add(meetingRooms);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = meetingRooms.Id }, meetingRooms);
        }

        // DELETE: api/MeetingRooms/5
        [ResponseType(typeof(MeetingRooms))]
        public IHttpActionResult DeleteMeetingRooms(int id)
        {
            MeetingRooms meetingRooms = db.MeetingRooms.Find(id);
            if (meetingRooms == null)
            {
                return NotFound();
            }

            db.MeetingRooms.Remove(meetingRooms);
            db.SaveChanges();

            return Ok(meetingRooms);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MeetingRoomsExists(int id)
        {
            return db.MeetingRooms.Count(e => e.Id == id) > 0;
        }
    }
}