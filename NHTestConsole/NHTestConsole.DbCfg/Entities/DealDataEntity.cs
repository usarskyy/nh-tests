using System;
using System.Diagnostics;


namespace NHTestConsole.DbSimpleCfg.Entities
{
  [Serializable]
  [DebuggerDisplay("ID:[{ID}], Type:[{Type.Name}], Merchant:[{Merchant.Name}], From: [{From.Count}], To: [{To.Count}]")]
  public class DealDataEntity
  {
    public virtual int ID { get; set; }

    public virtual string ExternalID { get; set; }

    public virtual bool IsDeleted { get; set; }

    public virtual bool IsApproved { get; set; }

    public virtual bool IsShared { get; set; }

    public virtual int? ApplicationID { get; set; }

    public virtual int MerchantID { get; set; }

    public virtual DateTime? ExpiresOn { get; set; }

    public virtual DateTime CreatedOn { get; set; }

    public virtual string CreatedBy { get; set; }

    public virtual DateTime ModifiedOn { get; set; }

    public virtual string ModifiedBy { get; set; }

    public virtual string AgentNotes { get; set; }

    public virtual bool SpecialOffer { get; set; }
    public virtual bool LuxuryProperty { get; set; }

    public virtual int? TotalRooms { get; set; }

    public virtual decimal? TotalArea { get; set; }
  }
}