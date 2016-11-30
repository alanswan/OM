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

        public ActionResult Match(int matchId)
        {
            var match = db.Matches.Where(x => x.MatchId == matchId).First();
            OddsComparisonPageViewModel viewModel = new OddsComparisonPageViewModel()
            {
                Competition = match.CompetitionName,
                MatchDate = match.Date,
                MatchTime = match.Time,
                MatchName = match.Name,
                MatchId = matchId
            };
            return View("Match", viewModel);

        }

        //public ActionResult GetOddsData()
        //{
        //    var odds = GetOdds();
        //    return Json(odds, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GetOddsData(int matchId)
        {
            var odds = (from m1 in db.Matches //bet365
                       // join m2 in db.Matches on new { MatchId = matchId, Bet = m1.Team1Name, Bookmaker = 21 } equals new { m2.MatchId, m2.Bet, Bookmaker = m2.BookmakerId }
                        join m3 in db.Matches on new { MatchId = matchId, Bet = m1.Team1Name, Bookmaker = 81 } equals new { m3.MatchId, m3.Bet, Bookmaker = m3.BookmakerId }
                        join m4 in db.Matches on new { MatchId = matchId, Bet = m1.Team1Name, Bookmaker = 34 } equals new { m4.MatchId, m4.Bet, Bookmaker = m4.BookmakerId }
                        join m5 in db.Matches on new { MatchId = matchId, Bet = m1.Team1Name, Bookmaker = 42 } equals new { m5.MatchId, m5.Bet, Bookmaker = m5.BookmakerId }
                        join m6 in db.Matches on new { MatchId = matchId, Bet = m1.Team1Name, Bookmaker = 38 } equals new { m6.MatchId, m6.Bet, Bookmaker = m6.BookmakerId }
                        join m7 in db.Matches on new { MatchId = matchId, Bet = m1.Team1Name, Bookmaker = 93 } equals new { m7.MatchId, m7.Bet, Bookmaker = m7.BookmakerId }
                        where m1.MatchId == matchId & m1.Bet == m1.Team1Name & m1.BookmakerId == 8
                        select new OddsComparisonViewModel()
                        {
                            EventName = m1.Name,
                            BetName = m1.Bet,
                            Ladbrokes = m3.Odds.Value,
                            Bet365 = m1.Odds.Value,
                            StanJames = m4.Odds.Value,
                            Eight88Sport = m6.Odds.Value,
                            BetVictor = m5.Odds.Value,
                            SkyBet = m7.Odds.Value
                            //One88Bet = m2.Odds.Value
                        }).Distinct().Take(1);

            var odds2 = (from m1 in db.Matches //bet365
                        //join m2 in db.Matches on new { MatchId = matchId, Bet = m1.Team2Name, Bookmaker = 21 } equals new { m2.MatchId, m2.Bet, Bookmaker = m2.BookmakerId }
                        join m3 in db.Matches on new { MatchId = matchId, Bet = m1.Team2Name, Bookmaker = 81 } equals new { m3.MatchId, m3.Bet, Bookmaker = m3.BookmakerId }
                         join m4 in db.Matches on new { MatchId = matchId, Bet = m1.Team2Name, Bookmaker = 34 } equals new { m4.MatchId, m4.Bet, Bookmaker = m4.BookmakerId }
                         join m5 in db.Matches on new { MatchId = matchId, Bet = m1.Team2Name, Bookmaker = 42 } equals new { m5.MatchId, m5.Bet, Bookmaker = m5.BookmakerId }
                         join m6 in db.Matches on new { MatchId = matchId, Bet = m1.Team2Name, Bookmaker = 38 } equals new { m6.MatchId, m6.Bet, Bookmaker = m6.BookmakerId }
                         join m7 in db.Matches on new { MatchId = matchId, Bet = m1.Team2Name, Bookmaker = 93 } equals new { m7.MatchId, m7.Bet, Bookmaker = m7.BookmakerId }
                         where m1.MatchId == matchId & m1.Bet == m1.Team2Name & m1.BookmakerId == 8
                        select new OddsComparisonViewModel()
                        {
                            EventName = m1.Name,
                            BetName = m1.Bet,
                            Ladbrokes = m3.Odds.Value,
                            Bet365 = m1.Odds.Value,
                            StanJames = m4.Odds.Value,
                            Eight88Sport = m6.Odds.Value,
                            BetVictor = m5.Odds.Value,
                            SkyBet = m7.Odds.Value
                            //One88Bet = m2.Odds.Value
                        }).Distinct().Take(1);

            var odds3 = (from m1 in db.Matches //bet365
                       // join m2 in db.Matches on new { MatchId = matchId, Bet = "Draw", Bookmaker = 21 } equals new { m2.MatchId, m2.Bet, Bookmaker = m2.BookmakerId }
                        join m3 in db.Matches on new { MatchId = matchId, Bet = "Draw", Bookmaker = 81 } equals new { m3.MatchId, m3.Bet, Bookmaker = m3.BookmakerId }
                         join m4 in db.Matches on new { MatchId = matchId, Bet = "Draw", Bookmaker = 34 } equals new { m4.MatchId, m4.Bet, Bookmaker = m4.BookmakerId }
                         join m5 in db.Matches on new { MatchId = matchId, Bet = "Draw", Bookmaker = 42 } equals new { m5.MatchId, m5.Bet, Bookmaker = m5.BookmakerId }
                         join m6 in db.Matches on new { MatchId = matchId, Bet = "Draw", Bookmaker = 38 } equals new { m6.MatchId, m6.Bet, Bookmaker = m6.BookmakerId }
                         join m7 in db.Matches on new { MatchId = matchId, Bet = "Draw", Bookmaker = 93 } equals new { m7.MatchId, m7.Bet, Bookmaker = m7.BookmakerId }
                         where m1.MatchId == matchId & m1.Bet == "Draw" & m1.BookmakerId == 8
                        select new OddsComparisonViewModel()
                        {
                            EventName = m1.Name,
                            BetName = m1.Bet,
                            Ladbrokes = m3.Odds.Value,
                            Bet365 = m1.Odds.Value,
                            StanJames = m4.Odds.Value,
                            Eight88Sport = m6.Odds.Value,
                            BetVictor = m5.Odds.Value,
                            SkyBet = m7.Odds.Value
                            //One88Bet = m2.Odds.Value
                        }).Distinct().Take(1);

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