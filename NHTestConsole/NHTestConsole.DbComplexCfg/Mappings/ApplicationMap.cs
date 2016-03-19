using FluentNHibernate.Mapping;

using NHTestConsole.DbComplexCfg.Entities;


namespace NHTestConsole.DbComplexCfg.Mappings
{
  public class ApplicationMap : ClassMap<ApplicationDataEntity>
  {
    public ApplicationMap()
    {
      Cache.IncludeAll()
           .ReadWrite();

      Table("Applications");

      Id(x => x.ID).Column("ApplicationID");

      Map(x => x.Name).Length(50).Not.Nullable();
      Map(x => x.ExternalID).Length(50)
                            .Unique()
                            .UniqueKey("UQ_Applications_ExternalID")
                            .Not.Nullable()
                            .Not.Update();

      Map(x => x.DomainName).Length(50)
                            .Unique()
                            .UniqueKey("UQ_Applications_DomainName")
                            .Not.Nullable()
                            .Not.Update();

      Map(x => x.CreatedBy).Length(50)
                           .Not.Nullable()
                           .Not.Update();

      Map(x => x.CreatedOn).Not.Nullable()
                           .Not.Update();

      Map(x => x.IsApproved).Not.Nullable();
      Map(x => x.IsActive).Not.Nullable();

      References(x => x.ParentMerchant)
        .Column("ParentMerchantID")
        .LazyLoad()
        .Not.Nullable();
    }
  }
}