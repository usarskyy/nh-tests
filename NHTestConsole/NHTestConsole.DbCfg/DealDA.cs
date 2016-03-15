using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using NHTestConsole.DbSimpleCfg.Entities;


namespace NHTestConsole.DbSimpleCfg
{
  public class DealDA
  {
    private const string SQL = @"SELECT [DealID]
                                       ,[ExternalID]
                                       ,[IsDeleted]
                                       ,[IsApproved]
                                       ,[IsShared]
                                       ,[ApplicationID]
                                       ,[MerchantID]
                                       ,[ContactAgentID]
                                       ,[ListingAgentID]
                                       ,[CreatedOn]
                                       ,[CreatedBy]
                                       ,[ModifiedOn]
                                       ,[ModifiedBy]
                                       ,[ExpiresOn]
                                       ,[AgentNotes]
                                       ,[TotalRooms]
                                       ,[TotalArea]
                                       ,[SpecialOffer]
                                       ,[LuxuryProperty]
                                 FROM [dbo].[vwDeals]
                                 WHERE [MerchantID] = @merchantId";

    public IList<DealDataEntity> LoadDeals(int merchantId)
    {
      var result = new List<DealDataEntity>();

      using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["adminDb"].ConnectionString))
      {
        using (var cmd = new SqlCommand(SQL, conn))
        {
          cmd.CommandType = CommandType.Text;
          cmd.Parameters.AddWithValue("merchantId", merchantId);

          conn.Open();

          using (var reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              result.Add(Deserialize(reader));
            }
          }
        }
      }

      return result;
    }

    private static DealDataEntity Deserialize(SqlDataReader reader)
    {
      var result = new DealDataEntity();

      var agentNotesIdx = reader.GetOrdinal("AgentNotes");
      result.AgentNotes = reader.IsDBNull(agentNotesIdx) ? null : reader.GetString(agentNotesIdx);
      
      var appIdx = reader.GetOrdinal("ApplicationID");
      result.ApplicationID = reader.IsDBNull(appIdx) ? (int?) null : reader.GetInt32(appIdx);

      result.CreatedBy = reader.GetString(reader.GetOrdinal("CreatedBy"));
      result.CreatedOn = reader.GetDateTime(reader.GetOrdinal("CreatedOn"));

      var expiresOnIdx = reader.GetOrdinal("ExpiresOn");
      result.ExpiresOn = reader.IsDBNull(expiresOnIdx) ? (DateTime?) null : reader.GetDateTime(expiresOnIdx);

      result.ExternalID = reader.GetString(reader.GetOrdinal("ExternalID"));
      result.ID = reader.GetInt32(reader.GetOrdinal("DealID"));
      result.IsApproved = reader.GetBoolean(reader.GetOrdinal("IsApproved"));
      result.IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted"));
      result.IsShared = reader.GetBoolean(reader.GetOrdinal("IsShared"));
      result.MerchantID = reader.GetInt32(reader.GetOrdinal("MerchantID"));
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