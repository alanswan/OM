using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OM.Models
{

    public class OMViewModel
    {
        public List<BookmakerViewModel> Bookmakers { get; set; }
        public List<BookmakerViewModel> Exchanges { get; set; }
    }

    public class OMMobileViewModel
    {
        public List<BookmakerViewModel> Bookmakers { get; set; }
        public List<BookmakerViewModel> Exchanges { get; set; }
        public List<OddsViewModel> Odds { get; set; }
    }

    public class OddsComparisonPageViewModel
    {
        public int MatchId { get; set; }
        public string MatchName { get; set; }
        public DateTime MatchDate { get; set; }
        public string MatchTime { get; set; }
        public string Competition { get; set; }
    }

    public class OddsViewModel
    {
        public string EventName { get; set; }
        public string Bet { get; set; }
        public string Bookmaker { get; set; }
        public decimal BookmakerOdds { get; set; }
        public string Exchange { get; set; }
        public decimal ExchangeOdds { get; set; }
        public decimal Rating { get; set; }
        public decimal MoneyInMarket { get; set; }
        public string Url { get; set; }
        public string EventType { get; set; }
        public DateTime EventDate { get; set; }
        public string EventTime { get; set; }
        public string Competition { get; set; }
    }

    public class BookmakerViewModel
    {
        public string BookmakerName { get; set; }
        public int BookmakerId { get; set; }
    }

    public class OddsComparisonViewModel
    {
        public string EventName { get; set; }
        public string BetName { get; set; }
        public decimal WilliamHill { get; set; }
        public decimal Betfred { get; set; }
        public decimal Coral { get; set; }
        public decimal Ladbrokes { get; set; }
        public decimal Bet365 { get; set; }
        public decimal Eight88Sport { get; set; }
        public decimal StanJames { get; set; }
        public decimal BetVictor { get; set; }
        public decimal SkyBet { get; set; }
    }
}