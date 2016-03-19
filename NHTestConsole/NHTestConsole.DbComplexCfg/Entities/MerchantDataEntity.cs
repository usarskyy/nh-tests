using System;
using System.Collections.ObjectModel;


namespace NHTestConsole.DbComplexCfg.Entities
{
  [Serializable]
  public class MerchantDataEntity
  {
    public MerchantDataEntity()
    {
      ChildApplications = new Collection<ApplicationDataEntity>();
    }

    public virtual int ID { get; set; }

    public virtual string Name { get; set; }

    public virtual string Description { get; set; }

    public virtual string ExternalID { get; set; }

    public virtual string CreatedBy { get; set; }

    public virtual DateTime CreatedOn { get; set; }

    public virtual string Street { get; set; }

    public virtual Collection<ApplicationDataEntity> ChildApplications { get; set; } 
  }
}