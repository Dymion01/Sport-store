using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_store.Controllers
{
 
    public class ChatController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }
    }
}
