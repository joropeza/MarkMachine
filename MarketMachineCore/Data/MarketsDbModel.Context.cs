﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MarketMachineCore.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MarketsDBEntities : DbContext
    {
        public MarketsDBEntities()
            : base("name=MarketsDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Gap> Gaps { get; set; }
        public virtual DbSet<Market> Markets { get; set; }
        public virtual DbSet<MarketType> MarketTypes { get; set; }
        public virtual DbSet<Candle> Candles { get; set; }
        public virtual DbSet<DailyCandle> DailyCandles { get; set; }
    }
}
