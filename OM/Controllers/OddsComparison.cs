using OM.EF;
using OM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace OM.Controllers
{
    public class OddsComparisonController : Controller
    {
        private omproEntities db = new omproEntities();
        // GET: Odds
        public ActionResult Index()
        {
            //OMViewModel viewModel = new OMViewModel()
            //{
            //    Bookmakers = GetBookmakers()
            //};
            //viewModel.Bookmakers.Insert(0, new BookmakerViewModel()
            //{
            //    BookmakerId = -1,
            //    BookmakerName = "All"
            //});
            return View();
        }

        public ActionResult ManchesterUnitedvAFCBournemouth()
        {
            return View("Index");
        }

        //public ActionResult GetOddsData()
        //{
        //    var odds = GetOdds();
        //    return Json(odds, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GetOddsData()
        {
            var odds = (from m1 in db.Matches
                        join m2 in db.Matches on new { MatchId = 5655037, Bet = "Manchester United" } equals new { m2.MatchId, m2.Bet }
                        join m3 in db.Matches on new { MatchId = 294038724, Bet = "Man Utd" } equals new { m3.MatchId, m3.Bet }
                        where m1.MatchId == 363618136 & m1.Bet == "Manchester United"
                        select new OddsComparisonViewModel()
                        {
                            EventName = m1.Name,
                            BetName = m1.Bet,
                            WilliamHill = m1.Odds.Value,
                            Betfred = m2.Odds.Value,
                            Coral = m3.Odds.Value
                        });

            var odds2 = (from m1 in db.Matches
                        join m2 in db.Matches on new { MatchId = 5655037, Bet = "AFC Bournemouth" } equals new { m2.MatchId, m2.Bet }
                        join m3 in db.Matches on new { MatchId = 294038725, Bet = "Bournemouth" } equals new { m3.MatchId, m3.Bet }
                        where m1.MatchId == 363618136 & m1.Bet == "AFC Bournemouth"
                         select new OddsComparisonViewModel()
                        {
                            EventName = m1.Name,
                            BetName = m1.Bet,
                            WilliamHill = m1.Odds.Value,
                            Betfred = m2.Odds.Value,
                            Coral = m3.Odds.Value
                        });

            var odds3 = (from m1 in db.Matches
                        join m2 in db.Matches on new { MatchId = 5655037, Bet = "Draw" } equals new { m2.MatchId, m2.Bet }
                        join m3 in db.Matches on new { MatchId = 294038723, Bet = "Draw" } equals new { m3.MatchId, m3.Bet }
                        where m1.MatchId == 363618136 & m1.Bet == "Draw"
                        select new OddsComparisonViewModel()
                        {
                            EventName = m1.Name,
                            BetName = m1.Bet,
                            WilliamHill = m1.Odds.Value,
                            Betfred = m2.Odds.Value,
                            Coral = m3.Odds.Value
                        });

            var count = odds.Union(odds2).Union(odds3).Count();
            var data = odds.Union(odds2).Union(odds3).ToList();
            return Json(new { total = count, data = data }, JsonRequestBehavior.AllowGet);
        }

        public List<BookmakerViewModel> GetBookmakers()
        {
            return (from b in db.Bookmakers
                    select new BookmakerViewModel()
                    {
                        BookmakerName = b.BookmakerName,
                        BookmakerId = b.BookmakerId
                    }).ToList();
        }

        public List<OddsViewModel> GetOdds(int pageSize, int skip)
        {
            var odds = (from m in db.Matches
                        join em in db.ExchangeMatches on new { m.Team1Id, m.Team2Id } equals new { em.Team1Id, em.Team2Id }
                        join b in db.Bookmakers on m.BookmakerId equals b.BookmakerId
                        join e in db.Bookmakers on em.BookmakerId equals e.BookmakerId
                        select new OddsViewModel()
                        {
                            EventName = m.Name,
                            Bet = m.Bet,
                            Bookmaker = b.BookmakerName,
                            BookmakerOdds = m.Odds.Value,
                            ExchangeOdds = em.Odds.Value,
                            Exchange = e.BookmakerName,
                            Rating = 100 - (em.Odds.Value - m.Odds.Value),
                            MoneyInMarket = em.MoneyInMarket.Value,
                            Url = m.URL
                        });

            var odds2 = (from m in db.Matches
                         join em in db.ExchangeMatches on new { team1 = m.Team1Id, team2 = m.Team2Id, m.Bet } equals new { team1 = em.Team2Id, team2 = em.Team1Id, em.Bet }
                         join b in db.Bookmakers on m.BookmakerId equals b.BookmakerId
                         join e in db.Bookmakers on em.BookmakerId equals e.BookmakerId
                         select new OddsViewModel()
                         {
                             EventName = m.Name,
                             Bet = m.Bet,
                             Bookmaker = b.BookmakerName,
                             BookmakerOdds = m.Odds.Value,
                             ExchangeOdds = em.Odds.Value,
                             Exchange = e.BookmakerName,
                             Rating = 100 - (em.Odds.Value - m.Odds.Value),
                             MoneyInMarket = em.MoneyInMarket.Value,
                             Url = m.URL
                         });
            var count = odds.Union(odds2).Count();
            return odds.Union(odds2).Where(x => x.Rating < 101).OrderByDescending(x=>x.Rating).Skip(skip).Take(pageSize).ToList();
            //foreach (var odd in odds.Where(x => x.Rating > 100).ToList())
            //{
            //    odds.Remove(odd);
            //};

            //odds = odds.OrderByDescending(x => x.Rating).Skip(skip).Take(pageSize).ToList();

           // return odds;
        }

        public List<OddsViewModel> GetOdds(int bookmakerId)
        {
            var odds = (from m in db.Matches
                        join em in db.ExchangeMatches on new { m.Team1Id, m.Team2Id } equals new { em.Team1Id, em.Team2Id }
                        join b in db.Bookmakers on m.BookmakerId equals b.BookmakerId
                        join e in db.Bookmakers on em.BookmakerId equals e.BookmakerId
                        where m.BookmakerId == bookmakerId
                        select new OddsViewModel()
                        {
                            EventName = m.Name,
                            Bet = m.Bet,
                            Bookmaker = b.BookmakerName,
                            BookmakerOdds = m.Odds.Value,
                            ExchangeOdds = em.Odds.Value,
                            Exchange = e.BookmakerName,
                            Rating = 100 - (em.Odds.Value - m.Odds.Value),
                            MoneyInMarket = em.MoneyInMarket.Value
                        }).ToList();

            var odds2 = (from m in db.Matches
                         join em in db.ExchangeMatches on new { team1 = m.Team1Id, team2 = m.Team2Id, m.Bet } equals new { team1 = em.Team2Id, team2 = em.Team1Id, em.Bet }
                         join b in db.Bookmakers on m.BookmakerId equals b.BookmakerId
                         join e in db.Bookmakers on em.BookmakerId equals e.BookmakerId
                         where m.BookmakerId == bookmakerId
                         select new OddsViewModel()
                         {
                             EventName = m.Name,
                             Bet = m.Bet,
                             Bookmaker = b.BookmakerName,
                             BookmakerOdds = m.Odds.Value,
                             ExchangeOdds = em.Odds.Value,
                             Exchange = e.BookmakerName,
                             Rating = 100 - (em.Odds.Value - m.Odds.Value),
                             MoneyInMarket = em.MoneyInMarket.Value
                         }).ToList();

            odds.AddRange(odds2);
            foreach (var odd in odds.Where(x => x.Rating > 100).ToList())
            {
                odds.Remove(odd);
            };

            odds = odds.OrderByDescending(x => x.Rating).ToList();

            return odds;
        }

    }
}