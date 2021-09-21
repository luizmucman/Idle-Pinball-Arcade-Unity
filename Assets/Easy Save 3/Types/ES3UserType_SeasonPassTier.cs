using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("containsFreeReward", "premiumReward", "freeReward")]
	public class ES3UserType_SeasonPassTier : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3UserType_SeasonPassTier() : base(typeof(SeasonPassTier)){ Instance = this; priority = 1; }


		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (SeasonPassTier)obj;
			
			writer.WriteProperty("containsFreeReward", instance.containsFreeReward, ES3Type_bool.Instance);
			writer.WriteProperty("premiumReward", instance.premiumReward, ES3UserType_SeasonPassRewards.Instance);
			writer.WriteProperty("freeReward", instance.freeReward, ES3UserType_SeasonPassRewards.Instance);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (SeasonPassTier)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "containsFreeReward":
						instance.containsFreeReward = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "premiumReward":
						instance.premiumReward = reader.Read<SeasonPassRewards>(ES3UserType_SeasonPassRewards.Instance);
						break;
					case "freeReward":
						instance.freeReward = reader.Read<SeasonPassRewards>(ES3UserType_SeasonPassRewards.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new SeasonPassTier();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}


	public class ES3UserType_SeasonPassTierArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_SeasonPassTierArray() : base(typeof(SeasonPassTier[]), ES3UserType_SeasonPassTier.Instance)
		{
			Instance = this;
		}
	}
}