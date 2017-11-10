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
    public class ReservationsController : Controller
    {
        private BookingOfMeetingRoomsDBEntities db = new BookingOfMeetingRoomsDBEntities();

        // GET: Reservations
        public ActionResult Index()
        {
            var reservations = db.Reservations.Include(r => r.MeetingRooms).Where(p => p.Status == true).OrderByDescending(o => o.DateStart);
            return View(reservations.ToList());
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservations reservations = db.Reservations.Find(id);
            if (reservations == null)
            {
                return HttpNotFound();
            }
            return View(reservations);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            ViewBag.MeetingRoom_Id = new SelectList(db.MeetingRooms, "Id", "Name");
            ViewBag.User_Id = new SelectList(db.Users, "Id", "UserName");
            return View();
        }

        // POST: Reservations/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DateStart,DateFinish,Status,MeetingRoom_Id,User_Id")] Reservations reservations)
        {
            if (ModelState.IsValid)
            {
                db.Reservations.Add(reservations);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MeetingRoom_Id = new SelectList(db.MeetingRooms, "Id", "Name", reservations.MeetingRoom_Id);
            ViewBag.User_Id = new SelectList(db.Users, "Id", "UserName", reservations.User_Id);
            return View(reservations);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservations reservations = db.Reservations.Find(id);
            if (reservations == null)
            {
                return HttpNotFound();
            }
            ViewBag.MeetingRoom_Id = new SelectList(db.MeetingRooms, "Id", "Name", reservations.MeetingRoom_Id);
            ViewBag.User_Id = new SelectList(db.Users, "Id", "UserName", reservations.User_Id);
            return View(reservations);
        }

        // POST: Reservations/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DateStart,DateFinish,Status,MeetingRoom_Id,User_Id")] Reservations reservations)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservations).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MeetingRoom_Id = new SelectList(db.MeetingRooms, "Id", "Name", reservations.MeetingRoom_Id);
            ViewBag.User_Id = new SelectList(db.Users, "Id", "UserName", reservations.User_Id);
            return View(reservations);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservations reservations = db.Reservations.Find(id);
            if (reservations == null)
            {
                return HttpNotFound();
            }
            return View(reservations);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservations reservations = db.Reservations.Find(id);
            db.Reservations.Remove(reservations);
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
