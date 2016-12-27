﻿using System.Linq;
using BlogSystem.Web.Infrastructure;
using BlogSystem.Web.Infrastructure.Mapping;
using BlogSystem.Web.ViewModels.Comment;

namespace BlogSystem.Web.Controllers
{
    using System.Net;
    using System.Web.Mvc;
    using Data.Models;
    using Data.UnitOfWork;
    using InputModels.Comments;
    using Infrastructure.Identity;

    [Authorize]
    public class CommentsController : BaseController
    {
        private readonly IBlogSystemData data;
        private readonly ICurrentUser currentUser;

        public CommentsController(IBlogSystemData data, ICurrentUser currentUser)
        {
            this.data = data;
            this.currentUser = currentUser;
        }

        [PassRouteValuesToViewData]
        public PartialViewResult All(int id, int maxComments = 999, int startFrom = 0)
        {
            var comments = this.data.Comments
                .All()
                .Where(c => c.PostId == id && !c.IsDeleted)
                .OrderByDescending(c => c.CreatedOn)
                .Skip(startFrom)
                .Take(maxComments)
                .To<CommentViewModel>();

            return this.PartialView(comments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, CreateCommentInputModel commentInputModel)
        {
            if (this.ModelState.IsValid)
            {
                var comment = new Comment
                {
                    Content = commentInputModel.Content,
                    PostId = id,
                    User = this.currentUser.Get(),
                    UserId = this.currentUser.Get().Id
                };

                this.data.Comments.Add(comment);
                this.data.SaveChanges();

                return this.RedirectToAction("Post", "Posts", new
                {
                    id = id
                });
            }

            return this.Content("Content is required");
        }

        [HttpGet]
        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var comment = this.data.Comments.Find(id);

            if (comment == null)
            {
                return this.HttpNotFound();
            }

            if (comment.UserId != this.currentUser.Get().Id)
            {
                return this.HttpNotFound();
            }

            var model = new EditCommentInputModel
            {
                Id = comment.Id,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                BlogPostId = comment.PostId,
                UserId = comment.UserId
            };

            return this.View(model);
        }

        // POST: Comments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditCommentInputModel commentInputModel)
        {
            if (this.ModelState.IsValid)
            {
                var comment = this.data.Comments.Find(commentInputModel.Id);

                comment.Id = commentInputModel.Id;
                comment.Content = commentInputModel.Content;
                comment.CreatedOn = commentInputModel.CreatedOn;
                comment.PostId = commentInputModel.BlogPostId;
                comment.UserId = commentInputModel.UserId;

                this.data.Comments.Update(comment);
                this.data.SaveChanges();

                return this.RedirectToAction("Post", "Posts", new
                {
                    id = commentInputModel.BlogPostId
                });
            }

            return this.View(commentInputModel);
        }
    }
}