using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewBookingofmeetingrooms;

namespace NewBookingofmeetingrooms.Controllers
{
    public class MeetingRoomsController : Controller
    {
        private BookingOfMeetingRoomsDBEntities db = new BookingOfMeetingRoomsDBEntities();

        // GET: MeetingRooms
        public ActionResult Index()
        {
            return View(db.MeetingRooms.ToList());
        }

        // GET: MeetingRooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeetingRooms meetingRooms = db.MeetingRooms.Find(id);
            if (meetingRooms == null)
            {
                return HttpNotFound();
            }
            return View(meetingRooms);
        }

        // GET: MeetingRooms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MeetingRooms/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NumberChair,Name,Projector,Blackboard,FreedomStatus,DateReserv,Info")] MeetingRooms meetingRooms)
        {
            if (ModelState.IsValid)
            {
                db.MeetingRooms.Add(meetingRooms);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(meetingRooms);
        }

        // GET: MeetingRooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeetingRooms meetingRooms = db.MeetingRooms.Find(id);
            if (meetingRooms == null)
            {
                return HttpNotFound();
            }
            return View(meetingRooms);
        }

        // POST: MeetingRooms/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NumberChair,Name,Projector,Blackboard,FreedomStatus,DateReserv,Info")] MeetingRooms meetingRooms)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meetingRooms).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(meetingRooms);
        }

        // GET: MeetingRooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeetingRooms meetingRooms = db.MeetingRooms.Find(id);
            if (meetingRooms == null)
            {
                return HttpNotFound();
            }
            return View(meetingRooms);
        }

        // POST: MeetingRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MeetingRooms meetingRooms = db.MeetingRooms.Find(id);
            db.MeetingRooms.Remove(meetingRooms);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
