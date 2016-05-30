using FluentNHibernate.Mapping;

using NHTestConsole.DbComplexCfg.Entities;


namespace NHTestConsole.DbComplexCfg.Mappings
{
  public class DealMap : ClassMap<DealDataEntity>
  {
    public DealMap()
    {
      Table("vwDeals2");

      Cache.IncludeAll()
           .ReadWrite();


      Id(x => x.ID).Column("DealID");

      Map(x => x.ExternalID).Length(50)
        .Not.Nullable()
        .UniqueKey("UC_Deals_ExternalID");

      Map(x => x.CreatedBy).Length(50).Not.Nullable();
      Map(x => x.CreatedOn).Not.Nullable();

      Map(x => x.ModifiedBy).Length(50).Not.Nullable();
      Map(x => x.ModifiedOn).Not.Nullable();

      Map(x => x.IsDeleted).Not.Nullable();
      Map(x => x.IsApproved).Not.Nullable();
      Map(x => x.IsShared).Not.Nullable();
      Map(x => x.ExpiresOn).Nullable();
      Map(x => x.AgentNotes).Nullable();
      Map(x => x.DealTypeID).Nullable();

      Map(x => x.LuxuryProperty)
        .Column("LuxuryProperty")
        .Nullable()
        .Not.Insert()
        .Not.Update();

      Map(x => x.SpecialOffer)
        .Column("SpecialOffer")
        .Nullable()
        .Not.Insert()
        .Not.Update();

      Map(x => x.TotalArea)
        .Column("TotalArea")
        .Nullable()
        .Not.Insert()
        .Not.Update();

      Map(x => x.TotalRooms)
        .Column("TotalRooms")
        .Nullable()
        .Not.Insert()
        .Not.Update();

      References(x => x.Application)
        .Column("ApplicationID")
        .LazyLoad()
        .Nullable();

      References(x => x.Merchant)
        .Column("MerchantID")
        .Fetch.Join()
        .LazyLoad()
        .Not.Nullable();
    }
  }
}