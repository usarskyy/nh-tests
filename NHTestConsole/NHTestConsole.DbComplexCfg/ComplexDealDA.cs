using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using NHTestConsole.DbComplexCfg.Entities;


namespace NHTestConsole.DbComplexCfg
{
  public class ComplexDealDA
  {
    private const string SQL = @"
                                  SELECT DISTINCT
	                                  m.[merchantid],
	                                  m.[name],
	                                  m.[externalid],
	                                  m.[cityid],
	                                  m.[street],
	                                  m.[description],
	                                  m.[createdon],
	                                  m.[createdby],
	                                  m.[owner],
	                                  m.[parentapplicationid],
	                                  m.[activeresourceusageplanid]
                                  FROM [dbo].[merchants] as m
                                  INNER JOIN [dbo].[deals] as d
	                                  ON d.merchantid = m.merchantid
                                  WHERE d.dealtypeid = @dealtypeid

                                  SELECT DISTINCT
	                                  a.[applicationid],
	                                  a.[name],
	                                  a.[externalid],
	                                  a.[parentmerchantid],
	                                  a.[domainname],
	                                  a.[createdby],
	                                  a.[createdon],
	                                  a.[isapproved],
	                                  a.[isactive],
	                                  a.[skinid]
                                  FROM [dbo].[applications] as a
                                  INNER JOIN [dbo].[deals] as d
	                                  ON d.applicationid = a.applicationid
                                  WHERE d.dealtypeid = @dealtypeid

                                  SELECT
	                                  [dealid],
	                                  [externalid],
	                                  [isdeleted],
	                                  [isapproved],
	                                  [isshared],
	                                  [applicationid],
	                                  [merchantid],
	                                  [contactagentid],
	                                  [listingagentid],
	                                  [createdon],
	                                  [createdby],
	                                  [modifiedon],
	                                  [modifiedby],
	                                  [expireson],
	                                  [agentnotes],
	                                  [totalrooms],
	                                  [totalarea],
	                                  [specialoffer],
	                                  [luxuryproperty]
                                  FROM [dbo].[vwdeals2]
                                  WHERE dealtypeid = @dealtypeid
                                  ";

    public IList<DealDataEntity> LoadDeals(short dealTypeId)
    {
      var result = new List<DealDataEntity>();

      using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["adminDb"].ConnectionString))
      {
        using (var cmd = new SqlCommand(SQL, conn))
        {
          cmd.CommandType = CommandType.Text;
          cmd.Parameters.AddWithValue("dealtypeid", dealTypeId);

          conn.Open();

          using (var reader = cmd.ExecuteReader())
          {

            var merchants = new List<MerchantDataEntity>();

            while (reader.Read())
            {
              merchants.Add(DeserializeMerchant(reader));
            }

            if (!reader.NextResult())
            {
              throw new Exception("Applications result set is missing");
            }

            var applications = new List<ApplicationDataEntity>();

            while (reader.Read())
            {
              applications.Add(DeserializeApplication(reader));
            }

            if (!reader.NextResult())
            {
              throw new Exception("Deals result set is missing");
            }

            while (reader.Read())
            {
              result.Add(DeserializeDeal(reader, merchants, applications));
            }
          }
        }
      }

      return result;
    }

    private static MerchantDataEntity DeserializeMerchant(SqlDataReader reader)
    {
      var result = new MerchantDataEntity();

      result.ID = reader.GetInt32(reader.GetOrdinal("MerchantID"));
      result.ExternalID = reader.GetString(reader.GetOrdinal("ExternalID"));
      result.Name = reader.GetString(reader.GetOrdinal("Name"));
      result.CreatedBy = reader.GetString(reader.GetOrdinal("CreatedBy"));
      result.CreatedOn = reader.GetDateTime(reader.GetOrdinal("CreatedOn"));
      result.Description = reader.GetString(reader.GetOrdinal("Description"));

      var streetIdx = reader.GetOrdinal("Street");
      result.Street = reader.IsDBNull(streetIdx) ? null : reader.GetString(streetIdx);

      // can't map it because we will need to load another rule set
      result.ChildApplications = null;
      
      return result;
    }

    private static ApplicationDataEntity DeserializeApplication(SqlDataReader reader)
    {
      var result = new ApplicationDataEntity();

      result.ID = reader.GetInt32(reader.GetOrdinal("ApplicationID"));
      result.ExternalID = reader.GetString(reader.GetOrdinal("ExternalID"));
      result.CreatedBy = reader.GetString(reader.GetOrdinal("CreatedBy"));
      result.CreatedOn = reader.GetDateTime(reader.GetOrdinal("CreatedOn"));
      result.DomainName = reader.GetString(reader.GetOrdinal("DomainName"));
      result.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
      result.IsApproved = reader.GetBoolean(reader.GetOrdinal("IsApproved"));
      result.Name = reader.GetString(reader.GetOrdinal("Name"));

      return result;
    }

    private static DealDataEntity DeserializeDeal(SqlDataReader reader, IList<MerchantDataEntity> merchants, IList<ApplicationDataEntity> applications)
    {
      var result = new DealDataEntity();

      var agentNotesIdx = reader.GetOrdinal("AgentNotes");
      result.AgentNotes = reader.IsDBNull(agentNotesIdx) ? null : reader.GetString(agentNotesIdx);
      
      var appIdx = reader.GetOrdinal("ApplicationID");
      var appId = reader.IsDBNull(appIdx) ? (int?) null : reader.GetInt32(appIdx);
      result.Application = appId.HasValue ? applications.Single(x => x.ID == appId) : null;

      var merchantID = reader.GetInt32(reader.GetOrdinal("MerchantID"));
      result.Merchant = merchants.Single(x => x.ID == merchantID);

      result.CreatedBy = reader.GetString(reader.GetOrdinal("CreatedBy"));
      result.CreatedOn = reader.GetDateTime(reader.GetOrdinal("CreatedOn"));

      var expiresOnIdx = reader.GetOrdinal("ExpiresOn");
      result.ExpiresOn = reader.IsDBNull(expiresOnIdx) ? (DateTime?) null : reader.GetDateTime(expiresOnIdx);

      result.ExternalID = reader.GetString(reader.GetOrdinal("ExternalID"));
      result.ID = reader.GetInt32(reader.GetOrdinal("DealID"));
      result.IsApproved = reader.GetBoolean(reader.GetOrdinal("IsApproved"));
      result.IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted"));
      result.IsShared = reader.GetBoolean(reader.GetOrdinal("IsShared"));
      result.LuxuryProperty = reader.GetBoolean(reader.GetOrdinal("LuxuryProperty"));
      result.SpecialOffer = reader.GetBoolean(reader.GetOrdinal("SpecialOffer"));
      
      var totalRoomsIdx = reader.GetOrdinal("TotalRooms");
      result.TotalRooms = reader.IsDBNull(totalRoomsIdx) ? (short?) null : reader.GetInt16(totalRoomsIdx);

      var totalAreaIdx = reader.GetOrdinal("TotalArea");
      result.TotalArea = reader.IsDBNull(totalAreaIdx) ? (decimal?) null : reader.GetDecimal(totalAreaIdx);
      

      return result;
    }
  }
}