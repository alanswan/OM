﻿using OM.EF;
using OM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace OM.Controllers
{
    public class OddsController : Controller
    {
        private omproEntities db = new omproEntities();
        // GET: Odds
        public ActionResult Index()
        {
            OMViewModel viewModel = new OMViewModel()
            {
                Bookmakers = GetBookmakers(false),
                Exchanges = GetBookmakers(true)
            };
            viewModel.Bookmakers.Insert(0, new BookmakerViewModel()
            {
                BookmakerId = -1,
                BookmakerName = "All"
            });
            viewModel.Exchanges.Insert(0, new BookmakerViewModel()
            {
                BookmakerId = -1,
                BookmakerName = "All"
            });
            return View(viewModel);
        }

        public ActionResult Trial()
        {
            OMViewModel viewModel = new OMViewModel()
            {
                Bookmakers = GetBookmakers(false),
                Exchanges = GetBookmakers(true)
            };
            viewModel.Bookmakers.Insert(0, new BookmakerViewModel()
            {
                BookmakerId = -1,
                BookmakerName = "All"
            });
            viewModel.Exchanges.Insert(0, new BookmakerViewModel()
            {
                BookmakerId = -1,
                BookmakerName = "All"
            });
            return View(viewModel);
        }

        public ActionResult Mobile()
        {
            OMViewModel viewModel = new OMViewModel()
            {
                Bookmakers = GetBookmakers(false),
                Exchanges = GetBookmakers(true)
            };
            viewModel.Bookmakers.Insert(0, new BookmakerViewModel()
            {
                BookmakerId = -1,
                BookmakerName = "All"
            });
            viewModel.Exchanges.Insert(0, new BookmakerViewModel()
            {
                BookmakerId = -1,
                BookmakerName = "All"
            });
            return View(viewModel);
        }

        public ActionResult TrialMobile()
        {
            OMViewModel viewModel = new OMViewModel()
            {
                Bookmakers = GetBookmakers(false),
                Exchanges = GetBookmakers(true)
            };
            viewModel.Bookmakers.Insert(0, new BookmakerViewModel()
            {
                BookmakerId = -1,
                BookmakerName = "All"
            });
            viewModel.Exchanges.Insert(0, new BookmakerViewModel()
            {
                BookmakerId = -1,
                BookmakerName = "All"
            });
            return View(viewModel);
        }



        //public ActionResult GetOddsData()
        //{
        //    var odds = GetOdds();
        //    return Json(odds, JsonRequestBehavior.AllowGet);
        //}z

        public ActionResult GetOddsData( string bookmaker, string exchange)
        {
            //get uk date / time 
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var britishDate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, britishZone);
            
            bookmaker = bookmaker.Replace("\"", "");
            exchange = exchange.Replace("\"", "");
            //need to pass bookmaker/exchange in here
            var odds = (from m in db.Matches
                        join em in db.ExchangeMatches on new { m.Team1Id, m.Team2Id, m.Bet } equals new { em.Team1Id, em.Team2Id, em.Bet }
                        join b in db.Bookmakers on m.BookmakerId equals b.BookmakerId
                        join e in db.Bookmakers on em.BookmakerId equals e.BookmakerId
                        where m.Date >= britishDate.Date
                        select new OddsViewModel()
                        {
                            EventName = m.Name,
                            Competition = m.CompetitionName,
                            Bet = m.Bet,
                            Bookmaker = b.BookmakerName,
                            BookmakerOdds = m.Odds.Value,
                            ExchangeOdds = em.Odds.Value,
                            Exchange = e.BookmakerName,
                            Rating = 100 - ((em.Odds.Value - m.Odds.Value)*20),
                            MoneyInMarket = em.MoneyInMarket.Value,
                            Url = m.URL,
                            EventType = "football",
                            EventDate = m.Date,
                            EventTime = m.Time
                        });

            var odds2 = (from m in db.Matches
                         join em in db.ExchangeMatches on new { team1 = m.Team1Id, team2 = m.Team2Id, m.Bet } equals new { team1 = em.Team2Id, team2 = em.Team1Id, em.Bet }
                         join b in db.Bookmakers on m.BookmakerId equals b.BookmakerId
                         join e in db.Bookmakers on em.BookmakerId equals e.BookmakerId
                         where m.Date >= britishDate.Date
                         select new OddsViewModel()
                         {
                             EventName = m.Name,
                             Competition = m.CompetitionName,
                             Bet = m.Bet,
                             Bookmaker = b.BookmakerName,
                             BookmakerOdds = m.Odds.Value,
                             ExchangeOdds = em.Odds.Value,
                             Exchange = e.BookmakerName,
                             Rating = 100 - ((em.Odds.Value - m.Odds.Value)*20),
                             MoneyInMarket = em.MoneyInMarket.Value,
                             Url = m.URL,
                             EventType = "football",
                             EventDate = m.Date,
                             EventTime = m.Time
                         });

            var horses = (from m in db.Races
                          join em in db.ExchangeRaces on new {m.Horse, m.CompetitionName } equals new { em.Horse, em.CompetitionName }
                          join b in db.Bookmakers on m.BookmakerId equals b.BookmakerId
                          join e in db.Bookmakers on em.BookmakerId equals e.BookmakerId
                          where m.Date >= britishDate.Date
                          select new OddsViewModel()
                          {
                              EventName = m.Name,
                              Competition = "",
                              Bet = m.Horse,
                              Bookmaker = b.BookmakerName,
                              BookmakerOdds = m.Odds.Value,
                              ExchangeOdds = em.Odds.Value,
                              Exchange = e.BookmakerName,
                              Rating = 100 - ((em.Odds.Value - m.Odds.Value) * 20),
                              MoneyInMarket = em.MoneyInMarket.Value,
                              Url = m.URL,
                              EventType = "horse-racing",
                              EventDate = m.Date,
                              EventTime = m.Time
                          });

            if (bookmaker != "All")
            {
                odds = odds.Where(x => x.Bookmaker == bookmaker);
                odds2 = odds2.Where(x => x.Bookmaker == bookmaker);
                horses = horses.Where(x => x.Bookmaker == bookmaker);
            }
            if(exchange != "All")
            {
                odds = odds.Where(x => x.Exchange == exchange);
                odds2 = odds2.Where(x => x.Exchange == exchange);
                horses = horses.Where(x => x.Exchange == exchange);
            }

            //var count = odds.Union(odds2).Union(horses).Count();
            var count = odds.Union(odds2).Union(horses).Where(x => x.Rating < 100).Count();
           // var data = odds.Union(odds2).Union(horses).Where(x => x.Rating < 100).OrderByDescending(x => x.Rating).Skip(skip).Take(pageSize).ToList();
            //var data = odds.Union(odds2).Union(horses).OrderByDescending(x => x.Rating).ToList();//.Skip(skip).Take(pageSize).ToList();
            var data = odds.Union(odds2).Union(horses).Where(x => x.Rating < 100).OrderByDescending(x => x.Rating).ToList();//.Skip(skip).Take(pageSize).ToList();
            return Json(new { total = count, data = data }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetOddsDataTrial(string bookmaker, string exchange)
        {
            bookmaker = bookmaker.Replace("\"", "");
            exchange = exchange.Replace("\"", "");
            //need to pass bookmaker/exchange in here
            var odds = (from m in db.Matches
                        join em in db.ExchangeMatches on new { m.Team1Id, m.Team2Id, m.Bet } equals new { em.Team1Id, em.Team2Id, em.Bet }
                        join b in db.Bookmakers on m.BookmakerId equals b.BookmakerId
                        join e in db.Bookmakers on em.BookmakerId equals e.BookmakerId
                        where m.BookmakerId == 102 || m.BookmakerId == 103
                        select new OddsViewModel()
                        {
                            EventName = m.Name,
                            Competition = m.CompetitionName,
                            Bet = m.Bet,
                            Bookmaker = b.BookmakerName,
                            BookmakerOdds = m.Odds.Value,
                            ExchangeOdds = em.Odds.Value,
                            Exchange = e.BookmakerName,
                            Rating = 100 - ((em.Odds.Value - m.Odds.Value) * 20),
                            MoneyInMarket = em.MoneyInMarket.Value,
                            Url = m.URL,
                            EventType = "football",
                            EventDate = m.Date,
                            EventTime = m.Time
                        });

            var odds2 = (from m in db.Matches
                         join em in db.ExchangeMatches on new { team1 = m.Team1Id, team2 = m.Team2Id, m.Bet } equals new { team1 = em.Team2Id, team2 = em.Team1Id, em.Bet }
                         join b in db.Bookmakers on m.BookmakerId equals b.BookmakerId
                         join e in db.Bookmakers on em.BookmakerId equals e.BookmakerId
                         where m.BookmakerId == 102 || m.BookmakerId == 103
                         select new OddsViewModel()
                         {
                             EventName = m.Name,
                             Competition = m.CompetitionName,
                             Bet = m.Bet,
                             Bookmaker = b.BookmakerName,
                             BookmakerOdds = m.Odds.Value,
                             ExchangeOdds = em.Odds.Value,
                             Exchange = e.BookmakerName,
                             Rating = 100 - ((em.Odds.Value - m.Odds.Value) * 20),
                             MoneyInMarket = em.MoneyInMarket.Value,
                             Url = m.URL,
                             EventType = "football",
                             EventDate = m.Date,
                             EventTime = m.Time
                         });

            var horses = (from m in db.Races
                          join em in db.ExchangeRaces on new { m.Horse, m.CompetitionName } equals new { em.Horse, em.CompetitionName }
                          join b in db.Bookmakers on m.BookmakerId equals b.BookmakerId
                          join e in db.Bookmakers on em.BookmakerId equals e.BookmakerId
                          where m.BookmakerId == 102 || m.BookmakerId == 103
                          select new OddsViewModel()
                          {
                              EventName = m.Name,
                              Competition = "",
                              Bet = m.Horse,
                              Bookmaker = b.BookmakerName,
                              BookmakerOdds = m.Odds.Value,
                              ExchangeOdds = em.Odds.Value,
                              Exchange = e.BookmakerName,
                              Rating = 100 - ((em.Odds.Value - m.Odds.Value) * 20),
                              MoneyInMarket = em.MoneyInMarket.Value,
                              Url = m.URL,
                              EventType = "horse-racing",
                              EventDate = m.Date,
                              EventTime = m.Time
                          });

            if (bookmaker != "All")
            {
                odds = odds.Where(x => x.Bookmaker == bookmaker);
                odds2 = odds2.Where(x => x.Bookmaker == bookmaker);
                horses = horses.Where(x => x.Bookmaker == bookmaker);
            }
            if (exchange != "All")
            {
                odds = odds.Where(x => x.Exchange == exchange);
                odds2 = odds2.Where(x => x.Exchange == exchange);
                horses = horses.Where(x => x.Exchange == exchange);
            }

            //var count = odds.Union(odds2).Union(horses).Count();
            var count = odds.Union(odds2).Union(horses).Where(x => x.Rating < 100).Count();
            // var data = odds.Union(odds2).Union(horses).Where(x => x.Rating < 100).OrderByDescending(x => x.Rating).Skip(skip).Take(pageSize).ToList();
            //var data = odds.Union(odds2).Union(horses).OrderByDescending(x => x.Rating).ToList();//.Skip(skip).Take(pageSize).ToList();
            var data = odds.Union(odds2).Union(horses).Where(x => x.Rating < 100).OrderByDescending(x => x.Rating).ToList();//.Skip(skip).Take(pageSize).ToList();
            return Json(new { total = count, data = data }, JsonRequestBehavior.AllowGet);
        }

        public List<BookmakerViewModel> GetBookmakers(bool exchangeFlag)
        {
            return (from b in db.Bookmakers
                        where b.ExchangeFlag == exchangeFlag
                    select new BookmakerViewModel()
                    {
                        BookmakerName = b.BookmakerName,
                        BookmakerId = b.BookmakerId
                    }).OrderBy(x => x.BookmakerName).ToList();
        }

        public List<OddsViewModel> GetOdds(int pageSize, int skip)
        {
            var odds = (from m in db.Matches
                        join em in db.ExchangeMatches on new { m.Team1Id, m.Team2Id, m.Bet } equals new { em.Team1Id, em.Team2Id, em.Bet }
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