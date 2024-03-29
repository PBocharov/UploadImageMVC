﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UploadImageMVC.Models;
using System.IO;

namespace UploadImageMVC.Controllers
{
    public class ImageController : Controller
    {

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Image image)
        {
            string fileName = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
            string extension = Path.GetExtension(image.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            image.ImagePath = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image"), fileName);
            image.ImageFile.SaveAs(fileName);
            using (DBModels db = new DBModels ())
            {
                db.Image.Add(image);
                db.SaveChanges();
            }
            ModelState.Clear();

            return View();
        }

        [HttpGet]
        public ActionResult View(int id)
        {
            Image image = new Image();
            using (DBModels db = new DBModels ())
            {
                image = db.Image.Where(x => x.ImageID == id).FirstOrDefault();
            }
            return View(image);
        }
    }
}