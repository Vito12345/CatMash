using System.Linq;
using System.Threading.Tasks;
using CatMash.Business;
using CatMash.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatMash.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICatManager _catManager;

        public HomeController(ICatManager catManager)
        {
            _catManager = catManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetRandomCats()
        {
            var cats = await _catManager.GetRandomCats(2);
            return new JsonResult(cats);
        }

        [HttpPost]
        public async Task<IActionResult> Vote([FromBody] VoteModel voteModel)
        {
            await _catManager.RegisterVote(new Domain.Cat { Id = voteModel.CatId });
            var cats = await _catManager.GetRandomCats(2);
            return new JsonResult(cats);
        }

        public async Task<ActionResult> Scores()
        {
            var cats = await _catManager.GetAllCats();
            return View(cats.OrderByDescending(c => c.Votes));
        }
    }
}
