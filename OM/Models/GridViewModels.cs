using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OM.Models
{

    public class OMViewModel
    {
        public List<BookmakerViewModel> Bookmakers { get; set; }
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
    }
}