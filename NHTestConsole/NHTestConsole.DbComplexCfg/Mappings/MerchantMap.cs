using FluentNHibernate.Mapping;

using NHTestConsole.DbComplexCfg.Entities;


namespace NHTestConsole.DbComplexCfg.Mappings
{
  public class MerchantMap : ClassMap<MerchantDataEntity>
  {
    public MerchantMap()
    {
      Table("Merchants");

      Cache.IncludeAll()
           .ReadWrite();

      Id(x => x.ID).Column("MerchantID").GeneratedBy.Identity();

      Map(x => x.Name).Length(50).Not.Nullable();
      Map(x => x.Street).Length(50).Nullable();
      Map(x => x.Description).Length(int.MaxValue).Nullable();
      Map(x => x.ExternalID).Length(50).Unique().UniqueKey("UQ_Merchants_ExternalID").Not.Nullable().Not.Update();
      Map(x => x.CreatedBy).Length(50).Not.Nullable().Not.Update();
      Map(x => x.CreatedOn).Not.Nullable().Not.Update();

      HasMany(x => x.ChildApplications)
        .KeyColumn("ParentMerchantID")
        .AsSet()
        .Cascade.None()
        .LazyLoad()
        .Inverse();
    }
  }
}