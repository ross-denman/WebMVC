using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebMVC.Data;
using WebMVC.Models;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    public class PostController : NameController
    {
        // GET: Post
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new PostService(userId);
            var model = service.GetPosts();
            return View(model);
        }

        // GET: Post/Details/5
        public ActionResult Details(int id)
        {
            var svc = CreatePostService();
            var model = svc.GetPostById(id);
            if (model is null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: Post/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreatePostService();

            if (service.CreatePost(model))
            {
                TempData["SaveResult"] = "Your post was created.";
                return RedirectToAction("index");
            };

            ModelState.AddModelError("", "Post could not be created.");
            return View(model);
        }

        // GET: Post/Edit/5
        public ActionResult Edit(int id)
        {
            var service = CreatePostService();
            var detail = service.GetPostById(id);
            var model = new PostEdit
            {
                PostId = detail.PostId,
                PostName = detail.PostName,
                PostCoverImage = detail.PostCoverImage,
                PostContent = detail.PostContent
            };
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PostEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.PostId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreatePostService();

            if (service.UpdatePost(model))
            {
                TempData["Save Result"] = "Your post was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your post could not be updated.");
            return View(model);
        }

        // GET: Post/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreatePostService();
            var model = svc.GetPostById(id);
            if (model is null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreatePostService();
            service.DeletePost(id);
            TempData["SaveResult"] = "Your post was deleted";
            return RedirectToAction("Index");
        }

        private PostService CreatePostService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new PostService(userId);
            return service;
        }
    }
}