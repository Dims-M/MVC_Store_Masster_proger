using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVC_Store_Masster_proger.Models.Data
{
    /// <summary>
    /// Связь с Базой данных
    /// </summary>
    public class Db : DbContext
    {
        /// <summary>
        /// Связь между таблицей бд и моделью PageDTO
        /// </summary>
        public DbSet<PageDTO> Pages { get; set; }
    }
}