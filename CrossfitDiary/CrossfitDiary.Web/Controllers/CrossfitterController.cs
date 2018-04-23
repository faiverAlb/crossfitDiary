using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using CrossfitDiary.Service;
using CrossfitDiary.Service.Interfaces;
using CrossfitDiary.Web.MvcHelpers;
using CrossfitDiary.Web.ViewModels;
using Microsoft.AspNet.Identity;

namespace CrossfitDiary.Web.Controllers
{
    public partial class CrossfitterController : Controller
    {
        public CrossfitterController()
        {
        }

        public virtual ActionResult ManageWorkout(int? workoutId, bool isEditing = false)
        {
            ViewBag.Title = "Manage Workout";
            return View();
        }

        public virtual ActionResult Pride()
        {
            ViewBag.Title = "What was done";
            return View();
        }
        

    }
}
