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

        ////Метод редактирования страницы
        //// GET: Admin/Page/EditPage
        //public ActionResult EditPage()
        //{

        //    return View();
        //}

        //Метод создания страницы
        // GET: Admin/Page/AddPage
        [HttpGet]
        public ActionResult AddPage()
        {

            return View();
        }

        // POST: Admin/Page/AddPage
        [HttpPost]
        public ActionResult AddPage(PageVM model)
        {

            //проверка моделди на коректность заполнения от рользователя
            if (!ModelState.IsValid) // если пришли не фалидные значения.
            {
                return View(model); //то возврат вьюхи
            }

            using (Db db = new Db()) // открытие бд
            {
                //переменная для крадкого описания страницы(slug)
                string slug;

                // Класс для работы с бд PageDTO
                PageDTO dto = new PageDTO();

                //Присвоим заголовок  модели. Все названия страниц. Пишем с большой буквы
                dto.Title = model.Title.ToUpper();

                //убедится что заголовок(имя страници является уникальным) и описание существует? И присваеваем модели для записи в бд
                if (string.IsNullOrWhiteSpace(model.Slug)) // если model.Slug имеет пустое значение 
                {
                    slug = model.Title.Replace(" ", "-").ToLower(); //присваивае с заменой всех пробелов на  дефиса
                }
                else //  если в моделе model.Slug что то имеется. То все равно добавляем дефисы
                {
                    slug = model.Title.Replace(" ", "-").ToLower();
                }

                //убеждаемся в уникальности заголовка
                //делаем запрос к бд. Ппроходим по все заголовка в бд. И ищем совпадения с пришедщоой моделью. Которую нам отправил пользователь из формы
                if (db.Pages.Any(x=> x.Title == model.Title))
                {
                    ModelState.AddModelError("", "Этот Title уже присуствует в БД "); //Создаем ошибку в сосоянии модели
                    return View(model); // отправляем пользователю на дороботку
                }

                //проверяем на уникальность
                else if (db.Pages.Any(x => x.Slug == model.Slug)) 
                {
                    ModelState.AddModelError("", "Этот Slug уже присуствует в БД "); //Создаем ошибку в сосоянии модели
                    return View(model); // отправляем пользователю на дороботку
                }

                // присвоиваем полученные даанные из модели
                dto.Slug = slug;
                dto.Body = model.Body;
                dto.HasSidebar = model.HasSidebar;
                dto.Sorting = 100; //это значит что будетт добавленно в конец списка

                //сохраняем в бд
                db.Pages.Add(dto);
                db.SaveChanges();
            }

            //передаем сообщение о записи в бд
            TempData["SM"] = "Данные удачно добавлены";

            //переадресация  странцы. на главную страницу индекса
            return RedirectToAction("Index");
        }

        //Admin/Pages
        /// <summary>
        /// Редактирование страницы
        /// </summary>
        /// <returns></returns>
        [HttpGet]  //будем отправлять пользователю в браузер
        public ActionResult EditPage( int id)
        {

            //обьявляем моднль страницы.(заполняетя будет из бд)
            PageVM model;

            using (Db db = new Db()) // соединение с бд
            {
                //получаем данные из формы клиента и заполняем моднл страницы
                PageDTO dto = db.Pages.Find(id); // получаем данную страницу по айди из бд

                //проверяем. доступна ли страница
                if (dto == null)
                {
                    return Content("Данная страница не доступна");
                }

                //инициализируем модель данными
                model = new PageVM(dto);
            }

            //отправляем в браузер
            return View(model);
        }
         
    }
}