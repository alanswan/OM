//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OM.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class ExchangeMatch
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public string Name { get; set; }
        public int BookmakerId { get; set; }
        public int CompetitionId { get; set; }
        public int Team1Id { get; set; }
        public string Team1Name { get; set; }
        public int Team2Id { get; set; }
        public string Team2Name { get; set; }
        public System.DateTime Date { get; set; }
        public System.DateTime LastUpdated { get; set; }
        public string Time { get; set; }
        public string CompetitionName { get; set; }
        public string Bet { get; set; }
        public Nullable<decimal> Odds { get; set; }
        public Nullable<decimal> MoneyInMarket { get; set; }
        public string URL { get; set; }
        public string MobileURL { get; set; }
        public Nullable<System.DateTime> UploadDate { get; set; }
        public Nullable<System.Guid> UploadID { get; set; }
    }
}
