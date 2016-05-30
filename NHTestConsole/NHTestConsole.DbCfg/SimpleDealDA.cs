using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using NHTestConsole.DbSimpleCfg.Entities;


namespace NHTestConsole.DbSimpleCfg
{
  public class SimpleDealDA
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
                                       ,[DealTypeID]
                                 FROM [dbo].[vwDeals2]
                                 WHERE [DealTypeID] = @dealTypeId";
    private readonly string _connectionString;

    public SimpleDealDA()
    {
      _connectionString = ConfigurationManager.ConnectionStrings["adminDb"].ConnectionString;
    }

    public IList<DealDataEntity> LoadDeals(short dealTypeId)
    {
      var result = new List<DealDataEntity>();

      using (var conn = new SqlConnection(_connectionString))
      {
        using (var cmd = new SqlCommand(SQL, conn))
        {
          cmd.CommandType = CommandType.Text;
          cmd.Parameters.AddWithValue("dealTypeId", dealTypeId);

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
      var result = new DealDataEntity
      {
        AgentNotes = GetNullableString(reader, "AgentNotes"),
        ApplicationID = GetNullableInt(reader, "ApplicationID"),
        CreatedBy = reader.GetString(reader.GetOrdinal("CreatedBy")),
        CreatedOn = reader.GetDateTime(reader.GetOrdinal("CreatedOn")),
        ExpiresOn = GetNullableDateTime(reader, "ExpiresOn"),
        ExternalID = reader.GetString(reader.GetOrdinal("ExternalID")),
        ID = reader.GetInt32(reader.GetOrdinal("DealID")),
        IsApproved = reader.GetBoolean(reader.GetOrdinal("IsApproved")),
        IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted")),
        IsShared = reader.GetBoolean(reader.GetOrdinal("IsShared")),
        MerchantID = reader.GetInt32(reader.GetOrdinal("MerchantID")),
        DealTypeID = reader.GetInt16(reader.GetOrdinal("DealTypeID")),
        LuxuryProperty = reader.GetBoolean(reader.GetOrdinal("LuxuryProperty")),
        SpecialOffer = reader.GetBoolean(reader.GetOrdinal("SpecialOffer")),
        TotalRooms = GetNullableShort(reader, "TotalRooms"),
        TotalArea = GetNullableDecimal(reader, "TotalArea")
      };


      return result;
    }

    private static string GetNullableString(IDataReader reader, string name)
    {
      var idx = reader.GetOrdinal(name);
      return reader.IsDBNull(idx) ? null : reader.GetString(idx);
    }

    private static int? GetNullableInt(IDataReader reader, string name)
    {
      var idx = reader.GetOrdinal(name);
      return reader.IsDBNull(idx) ? (int?) null : reader.GetInt32(idx);
    }

    private static decimal? GetNullableDecimal(IDataReader reader, string name)
    {
      var idx = reader.GetOrdinal(name);
      return reader.IsDBNull(idx) ? (decimal?) null : reader.GetDecimal(idx);
    }

    private static short? GetNullableShort(IDataReader reader, string name)
    {
      var idx = reader.GetOrdinal(name);
      return reader.IsDBNull(idx) ? (short?) null : reader.GetInt16(idx);
    }

    private static DateTime? GetNullableDateTime(IDataReader reader, string name)
    {
      var idx = reader.GetOrdinal(name);
      return reader.IsDBNull(idx) ? (DateTime?) null : reader.GetDateTime(idx);
    }
  }
}