using MVC_Store_Masster_proger.Models.Data;
using MVC_Store_Masster_proger.Models.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Store_Masster_proger.Areas.Admin.Controllers
{
    /// <summary>
    /// Контолер. Где будут отображатся все страници сайта
    /// </summary>
    public class PagesController : Controller
    {
        // GET: Admin/Page
        public ActionResult Index()
        {
            //получение из бд список  имеющихся страниц
            List<PageVM> pageList;

            //заполнить список из бд 
            using(Db db  = new Db())
            {// Выборка из базы данных.  приводим к массиву. мортируем.выбираем. создаем обьект PageVM и приводим его к типу листа
                pageList = db.Pages.ToArray().OrderBy(x => x.Sorting).Select(x => new PageVM(x)).ToList();
            }

            //вернуть в представление

            return View(pageList);
        }

        //Медо редактирования страницы
        // GET: Admin/Page/EditPage
        public ActionResult EditPage()
        {

            return View();
        }

    }
}