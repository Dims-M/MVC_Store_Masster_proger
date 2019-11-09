using MVC_Store_Masster_proger.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Store_Masster_proger.Models.ViewModels.Pages
{
    /// <summary>
    /// Получаем данные из класса Db и переедает во вьюху. Ни каких прямых связей с БД
    /// </summary>
    public class PageVM
    {
        //без параметров
        public PageVM()
        {
        }

            //Консруктор примимающий класс PagesDTO. Получаем данные из БД
            public PageVM(PageDTO row )
        {
            Id = row.Id;
            Title = row.Title;
            Slug = row.Slug;
            Body = row.Body;
            Sorting = row.Sorting;
            HasSidebar = row.HasSidebar;
        }


        //свойства для работы с БД
        public int Id { get; set; }
        [Required] // анатация. Это свойство должно обязателб юыть заполненно
        [StringLength(50, MinimumLength = 3)] // обязательная длина строки. Макс и минимум
        public string Title { get; set; }
        public string Slug { get; set; }
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 3)]
        public string Body { get; set; }
        public int Sorting { get; set; }
        public bool HasSidebar { get; set; }

    }
}