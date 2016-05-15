using PhotoGallery.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoGallery.Controllers
{
    public class GalleryController : Controller
    {
        private ApplicationDbContext _context;

        public GalleryController()
        {
            _context = new ApplicationDbContext();
        }
       
        // GET: Gallery
        public ActionResult Index()
        {

            var uploadedFiles = _context.Pictures;

            //var files = Directory.GetFiles(Server.MapPath("~/UploadedFiles"));

            //foreach (var file in files)
            //{
            //    var fileInfo = new FileInfo(file);

            //    var picture = new Picture() { Name = Path.GetFileName(file) };
            //    picture.Size = fileInfo.Length;

            //    picture.Path = ("~/UploadedFiles/") + Path.GetFileName(file);
            //    uploadedFiles.Add(picture);
            //}

            return View(uploadedFiles);
           
        }

        // GET: Gallery/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Gallery/Details/5
        public ActionResult Download(int id)
        {
            var picture = _context.Pictures.Find(id);
            if (picture!=null)
            {
                return File(Path.GetFileName(picture.Name), picture.ContentType);
            }

            return HttpNotFound("File not found");
        }

        // GET: Gallery/Create
        public ActionResult Create()
        {
            var model = new Picture();

            return View(model);
        }

        // POST: Gallery/Create
        [HttpPost]
        public ActionResult Create(Picture model)
        {
            try
            {

                // TODO: Add insert logic here
                foreach (string file in Request.Files)
                {
                    var postedFile = Request.Files[file];
                    //var postedFile = model.Image;
                    if (postedFile?.ContentLength>0)
                    {
                        var fileSaveName = string.Format("{0}-{1}", DateTime.Now.ToString(), postedFile.FileName);

                        postedFile.SaveAs(Server.MapPath("~/UploadedFiles/") + Path.GetFileName(fileSaveName));

                        var picture = new Picture()
                        {
                            Name = model.Name,
                            Path = string.Format("~/UploadedFiles/{0}", fileSaveName),
                            ContentType = postedFile.ContentType
                        };

                        _context.Entry<Picture>(picture).State = System.Data.Entity.EntityState.Added;

                    }
                    
                }
                _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Gallery/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Gallery/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Gallery/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Gallery/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
