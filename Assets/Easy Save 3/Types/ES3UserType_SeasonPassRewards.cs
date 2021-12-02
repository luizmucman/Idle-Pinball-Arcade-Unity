using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("isClaimed", "rewardItem")]
	public class ES3UserType_SeasonPassRewards : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3UserType_SeasonPassRewards() : base(typeof(SeasonPassRewards)){ Instance = this; priority = 1; }


		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (SeasonPassRewards)obj;
			
			writer.WriteProperty("isClaimed", instance.isClaimed, ES3Type_bool.Instance);
			writer.WritePropertyByRef("rewardItem", instance.rewardItem);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (SeasonPassRewards)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "isClaimed":
						instance.isClaimed = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "rewardItem":
						instance.rewardItem = reader.Read<SeasonPassItemSO>();
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new SeasonPassRewards();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}


	public class ES3UserType_SeasonPassRewardsArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_SeasonPassRewardsArray() : base(typeof(SeasonPassRewards[]), ES3UserType_SeasonPassRewards.Instance)
		{
			Instance = this;
		}
	}
}