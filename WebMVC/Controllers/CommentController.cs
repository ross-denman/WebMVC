using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebMVC.Data;
using WebMVC.Models;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    public class CommentController : NameController
    {
        // GET: Comment
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CommentService(userId);
            var model = service.GetComments();
            return View(model);
        }

        // GET: Comment/Details/5
        public ActionResult Details(int id)
        {
            var svc = CreateCommentService();
            var model = svc.GetCommentById(id);
            /*Comment comment = db.Comments.Find(id);*/
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: Comment/Create
        public ActionResult Create()
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CommentService(userId);
            ViewBag.PostId = new SelectList(ctx.Posts, "PostId", "PostName");
            return View();
        }


        // POST: Comment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommentCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateCommentService();

            if (service.CreateComment(model))
            {
                TempData["SaveResult"] = "Your comment was created.";
                return RedirectToAction("index");
            };

            ModelState.AddModelError("", "Comment could not be created.");
            return View(model);
        }

        // GET: Comment/Edit/5
        public ActionResult Edit(int id)
        {
            var service = CreateCommentService();
            var detail = service.GetCommentById(id);
            var model = new CommentEdit
            {
                CommentContent = detail.CommentContent
            };
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Comment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CommentEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.CommentId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateCommentService();

            if (service.UpdateComment(model))
            {
                TempData["Save Result"] = "Your comment was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your comment could not be updated.");
            return View(model);
        }

        // GET: Comment/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateCommentService();
            var model = svc.GetCommentById(id);
            if (model is null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteComment(int id)
        {
            var service = CreateCommentService();
            service.DeleteComment(id);
            TempData["SaveResult"] = "Your comment was deleted";
            return RedirectToAction("Index");
        }

        private CommentService CreateCommentService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CommentService(userId);
            return service;
        }
    }
}