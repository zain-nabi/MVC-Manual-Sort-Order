using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_Amalfi_Sort_Order.Models;

namespace MVC_Amalfi_Sort_Order.Controllers
{
    public class metricsController : Controller
    {
        private dbRestaurantEntities db = new dbRestaurantEntities();

        // GET: metrics
        public ActionResult Index()
        {
            return View(db.metrics.OrderBy(x => x.display_order).ToList());
        }

        // GET: metrics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            metric metric = db.metrics.Find(id);
            if (metric == null)
            {
                return HttpNotFound();
            }
            return View(metric);
        }

        // GET: metrics/Create
        public ActionResult Create()
        {
            ViewBag.display_order = new SelectList(db.metrics.ToList(), "display_order", "display_order");
            return View();
        }

        // POST: metrics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "metricID,description,campaignID,display_order")] metric metric)
        {
            if (ModelState.IsValid)
            {
                db.metrics.Add(metric);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: metrics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            metric metric = db.metrics.Find(id);
            if (metric == null)
            {
                return HttpNotFound();
            }
            ViewBag.display_order = new SelectList(db.metrics.OrderBy(x => x.display_order).ToList(), "display_order", "display_order");
            return View(metric);
        }

        // POST: metrics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "metricID,description,campaignID,display_order")] metric metric)
        {
            var currentDisplay = db.metrics.Find(metric.metricID);

            int? newIndex = metric.display_order;

            int? oldIndex = currentDisplay.display_order;

            var oldIndexRange = db.metrics.Where(x => x.display_order >= oldIndex && x.display_order <= newIndex).ToList();

            var newIndexRange = db.metrics.Where(x => x.display_order >= newIndex && x.display_order <= oldIndex).ToList();


            if (ModelState.IsValid)
            {

                if (oldIndexRange.Count != 0)
                {
                    if (newIndex < oldIndex)
                    {
                        foreach (var item in oldIndexRange)
                        {
                            if (item.display_order == oldIndex)
                            {
                                item.display_order = newIndex;
                            }
                            else
                            {
                                item.display_order += 1;
                            }
                            db.Entry(item).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        foreach (var item in oldIndexRange)
                        {
                            if (item.display_order == oldIndex)
                            {
                                item.display_order = newIndex;
                            }
                            else
                            {
                                item.display_order -= 1;
                            }
                            db.Entry(item).State = EntityState.Modified;
                        }
                    }
                    // Return success
                }
                else
                {
                    if (newIndex < oldIndex)
                    {
                        foreach (var item in newIndexRange)
                        {
                            if (item.display_order == oldIndex)
                            {
                                item.display_order = newIndex;
                            }
                            else
                            {
                                item.display_order += 1;
                            }
                            db.Entry(item).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        foreach (var item in newIndexRange)
                        {
                            if (item.display_order == oldIndex)
                            {
                                item.display_order = newIndex;
                            }
                            else
                            {
                                item.display_order -= 1;
                            }
                            db.Entry(item).State = EntityState.Modified;
                        }
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(metric);
        }

        // GET: metrics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            metric metric = db.metrics.Find(id);
            if (metric == null)
            {
                return HttpNotFound();
            }
            return View(metric);
        }

        // POST: metrics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            metric metric = db.metrics.Find(id);
            db.metrics.Remove(metric);
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
