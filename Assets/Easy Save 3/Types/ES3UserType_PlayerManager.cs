using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("playerSettingsData", "isAdFree", "is2xAllIncome", "is2xIdleIncome", "globalCoinMultiplier", "boostDatabase", "boostInventory", "playerCoins", "playerGems", "ballInventory", "ticketInventory", "ticketSlotCount", "equippedTickets")]
	public class ES3UserType_PlayerManager : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_PlayerManager() : base(typeof(PlayerManager)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (PlayerManager)obj;
			
			writer.WriteProperty("playerSettingsData", instance.playerSettingsData, ES3Internal.ES3TypeMgr.GetES3Type(typeof(PlayerSettingsData)));
			writer.WriteProperty("isAdFree", instance.isAdFree, ES3Type_bool.Instance);
			writer.WriteProperty("is2xAllIncome", instance.is2xAllIncome, ES3Type_bool.Instance);
			writer.WriteProperty("is2xIdleIncome", instance.is2xIdleIncome, ES3Type_bool.Instance);
			writer.WriteProperty("globalCoinMultiplier", instance.globalCoinMultiplier, ES3Type_float.Instance);
			writer.WritePropertyByRef("boostDatabase", instance.boostDatabase);
			writer.WriteProperty("boostInventory", instance.boostInventory, ES3Internal.ES3TypeMgr.GetES3Type(typeof(System.Collections.Generic.List<BoostData>)));
			writer.WriteProperty("playerCoins", instance.playerCoins, ES3Type_ulong.Instance);
			writer.WriteProperty("playerGems", instance.playerGems, ES3Type_int.Instance);
			writer.WriteProperty("ballInventory", instance.ballInventory, ES3Internal.ES3TypeMgr.GetES3Type(typeof(System.Collections.Generic.List<ItemData>)));
			writer.WriteProperty("ticketInventory", instance.ticketInventory, ES3Internal.ES3TypeMgr.GetES3Type(typeof(System.Collections.Generic.List<ItemData>)));
			writer.WriteProperty("ticketSlotCount", instance.ticketSlotCount, ES3Type_int.Instance);
			writer.WriteProperty("equippedTickets", instance.equippedTickets, ES3Internal.ES3TypeMgr.GetES3Type(typeof(System.Collections.Generic.List<ItemData>)));
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (PlayerManager)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "playerSettingsData":
						instance.playerSettingsData = reader.Read<PlayerSettingsData>();
						break;
					case "isAdFree":
						instance.isAdFree = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "is2xAllIncome":
						instance.is2xAllIncome = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "is2xIdleIncome":
						instance.is2xIdleIncome = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "globalCoinMultiplier":
						instance.globalCoinMultiplier = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "boostDatabase":
						instance.boostDatabase = reader.Read<BoostDatabase>(ES3UserType_BoostDatabase.Instance);
						break;
					case "boostInventory":
						instance.boostInventory = reader.Read<System.Collections.Generic.List<BoostData>>();
						break;
					case "playerCoins":
						instance.playerCoins = reader.Read<System.UInt64>(ES3Type_ulong.Instance);
						break;
					case "playerGems":
						instance.playerGems = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "ballInventory":
						instance.ballInventory = reader.Read<System.Collections.Generic.List<ItemData>>();
						break;
					case "ticketInventory":
						instance.ticketInventory = reader.Read<System.Collections.Generic.List<ItemData>>();
						break;
					case "ticketSlotCount":
						instance.ticketSlotCount = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "equippedTickets":
						instance.equippedTickets = reader.Read<System.Collections.Generic.List<ItemData>>();
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_PlayerManagerArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_PlayerManagerArray() : base(typeof(PlayerManager[]), ES3UserType_PlayerManager.Instance)
		{
			Instance = this;
		}
	}
}