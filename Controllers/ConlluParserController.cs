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
            ViewBag.Corpus = collection["corpus"];
            ViewBag.Dataset = collection["dataset"];
            var divideWithEmptyLines = collection["Params.DivideWithEmptyLines"].StartsWith("true");

            var viewModel = Preview(collection);
            switch (submit)
            {
                case "Preview":
                    return View(viewModel);
                case "Download":
                    var options = CreateParamsObject(collection);
                    var fileName = GetFileName(options);
                    return File(Encoding.UTF8.GetBytes(viewModel.Corpus.GetOutput(divideWithEmptyLines)), "text/plain", fileName);
            }
            return View();
        }

        private string GetFileName(ConlluParserParams options)
        {
            var modifOptions = "";
            if (options.ToLowerCase) modifOptions += "L";
            if (options.RemovePunctuation) modifOptions += "P";
            if (options.RemoveComments) modifOptions += "C";
            if (options.RemoveRangeLines) modifOptions += "R";
            if (options.RemoveSubindexLines) modifOptions += "S";
            if (options.DivideWithEmptyLines) modifOptions += "E";
            if (!string.IsNullOrWhiteSpace(modifOptions)) modifOptions = "_" + modifOptions;

            return $"en_{options.CorpusName}-ud-{options.Dataset}{modifOptions}.conllu";
        }

        private ConlluParserParams CreateParamsObject(FormCollection collection)
        {
            var corpusName = collection["corpus"];
            var dataset = collection["dataset"];
            return new ConlluParserParams
            {
                ConlluFilePath = Server.MapPath($"~/App_Data/UD_English_All/en_{corpusName}-ud-{dataset}.conllu"),
                ToLowerCase = collection["Params.ToLowerCase"].Contains("true"),
                RemovePunctuation = collection["Params.RemovePunctuation"].Contains("true"),
                RemoveComments = collection["Params.RemoveComments"].Contains("true"),
                RemoveRangeLines = collection["Params.RemoveRangeLines"].Contains("true"),
                RemoveSubindexLines = collection["Params.RemoveSubindexLines"].Contains("true"),
                DivideWithEmptyLines = collection["Params.DivideWithEmptyLines"].Contains("true"),
                CorpusName = corpusName,
                Dataset = dataset
            };
        }

        public ConlluParserViewModel Preview(FormCollection collection)
        {
            var options = CreateParamsObject(collection);
            var parsedCorpus = Parser.ParseConlluFile(options);

            return new ConlluParserViewModel
            {
                Params = options,
                Corpus = parsedCorpus
            };
        }
    }
}
