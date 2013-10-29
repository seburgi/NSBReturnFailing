using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using NsbReturnFailing.Contracts;

namespace NsbReturnFailing.Web.Controllers
{
    public class HomeController : Controller
    {
        private static readonly Dictionary<int, CommandResultModel> CommandResults = new Dictionary<int, CommandResultModel>();

        public ActionResult Index()
        {
            return View(CommandResults);
        }

        [HttpPost]
        public async Task<ActionResult> Index(string data)
        {
            int id = CommandResults.Count;
            CommandResults.Add(id, new CommandResultModel
                                       {
                                           Id = id,
                                           CommandSent = DateTime.Now,
                                       });

            int returnedId = await MvcApplication.Bus
                                                 .Send(new MyCommand
                                                           {
                                                               Id = id,
                                                               Data = data
                                                           })
                                                 .Register();

            CommandResults[returnedId].ResultReceived = DateTime.Now;

            return View("Index", CommandResults);
        }
    }

    public class CommandResultModel
    {
        public DateTime CommandSent { get; set; }
        public int Id { get; set; }
        public DateTime? ResultReceived { get; set; }
    }
}