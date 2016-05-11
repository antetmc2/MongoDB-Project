using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoTest.Models;

namespace MongoTest.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Portal()
        {
            ViewBag.Message = "Your application description page.";

            PortalMongoModel model = new PortalMongoModel();
            List<BsonDocument> Last10Articles = new List<BsonDocument>();

            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("nmbp");
            var col1 = database.GetCollection<BsonDocument>("post");
            var col2 = database.GetCollection<BsonDocument>("LastPost");

            var LastPostBsonDoc = col2.Find(new BsonDocument()).ToList();
            var LastPostID = LastPostBsonDoc[0].Elements.ToList()[1].Value.ToString();

            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(LastPostID));

            //var doc = col1.Find(new BsonDocument()).ToList();

            BsonDocument lastDoc = new BsonDocument();

            while (Last10Articles.Count < 10)
            {
                lastDoc = col1.Find(filter).FirstOrDefault();

                Last10Articles.Add(lastDoc);
                LastPostID = lastDoc.Elements.ToList()[6].Value.ToString();
                if(LastPostID != "") filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(LastPostID));
            }

            model.Articles = Last10Articles;

            var count = col1.Count(new BsonDocument());

            return View(model);
        }

        [HttpPost]
        public ActionResult Portal(string CommentForArticle, string chosenArticle)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("nmbp");
            var coll = database.GetCollection<BsonDocument>("post");

            var filt = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(chosenArticle));
            var upd = Builders<BsonDocument>.Update.Push("Comments", new BsonDocument { { "comment", CommentForArticle }, { "time", DateTime.Now } });

            var result = coll.UpdateOne(filt, upd);

            return RedirectToAction("Portal");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}