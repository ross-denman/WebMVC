using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMVC.Data;
using WebMVC.Models;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    public class ReplyController : NameController
    {
        // GET: Reply
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ReplyService(userId);
            var model = service.GetReplies();
            return View(model);
        }
        // GET: Reply/Details/5
        public ActionResult Details(int id)
        {
            var svc = CreateReplyService();
            var model = svc.GetReplyById(id);
       
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: Reply/Create
        public ActionResult Create()
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ReplyService(userId);
            ViewBag.PostId = new SelectList(ctx.Posts, "PostId", "PostName");
            return View();
        }


        // POST: Reply/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReplyCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateReplyService();

            if (service.CreateReply(model))
            {
                TempData["SaveResult"] = "Your comment was created.";
                return RedirectToAction("index");
            };

            ModelState.AddModelError("", "Comment could not be created.");
            return View(model);
        }

        // GET: Reply/Edit/5
        public ActionResult Edit(int id)
        {
            var service = CreateReplyService();
            var detail = service.GetReplyById(id);
            var model = new ReplyEdit
            {
                ReplyContent = detail.ReplyContent
            };
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Reply/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ReplyEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ReplyId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateReplyService();

            if (service.UpdateReply(model))
            {
                TempData["Save Result"] = "Your comment was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your comment could not be updated.");
            return View(model);
        }

        // GET: Reply/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateReplyService();
            var model = svc.GetReplyById(id);
            if (model is null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Reply/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteReply(int id)
        {
            var service = CreateReplyService();
            service.DeleteReply(id);
            TempData["SaveResult"] = "Your comment was deleted";
            return RedirectToAction("Index");
        }

        private ReplyService CreateReplyService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ReplyService(userId);
            return service;
        }
    }
}