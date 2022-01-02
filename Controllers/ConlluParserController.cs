using ConlluParser.Models;
using System.Text;
using System.Web.Mvc;
using Parser = ConlluParser.Services.ConlluParser;

namespace ConlluParser.Controllers
{
    public class ConlluParserController : Controller
    {
        // GET: ConlluParser
        public ActionResult Index()
        {
            var viewModel = new ConlluParserViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(string submit, FormCollection collection)
        {
            var viewModel = Preview(collection);
            switch (submit)
            {
                case "Preview":
                    return View(viewModel);
                case "Generate":
                    return File(Encoding.UTF8.GetBytes(viewModel.Corpus.ToString()), "text/plain", "corpus.conllu");
            }
            return View();
        }

        public ConlluParserViewModel Preview(FormCollection collection)
        {
            var options = new ConlluParserParams
            {
                ConlluFilePath = collection["Params.ConlluFilePath"],
                RemovePunctuation = collection["Params.RemovePunctuation"].Contains("true"),
                RemoveComments = collection["Params.RemoveComments"].Contains("true"),
                RemoveRangeLines = collection["Params.RemoveRangeLines"].Contains("true"),
                RemoveSubindexLines = collection["Params.RemoveSubindexLines"].Contains("true"),
                ToLowerCase = collection["Params.ToLowerCase"].Contains("true")
            };
            var parsedCorpus = Parser.ParseConlluFile(options);

            return new ConlluParserViewModel
            {
                Params = options,
                Corpus = parsedCorpus
            };
        }
    }
}
