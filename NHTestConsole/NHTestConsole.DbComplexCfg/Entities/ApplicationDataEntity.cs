using System;


namespace NHTestConsole.DbComplexCfg.Entities
{
  [Serializable]
  public class ApplicationDataEntity
  {
    public virtual int ID { get; set; }

    public virtual string ExternalID { get; set; }

    public virtual string Name { get; set; }

    public virtual string DomainName { get; set; }

    public virtual DateTime CreatedOn { get; set; }

    public virtual string CreatedBy { get; set; }

    public virtual bool IsApproved { get; set; }

    public virtual bool IsActive { get; set; }
  }
}