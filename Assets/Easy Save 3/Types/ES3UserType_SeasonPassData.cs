using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("isPremium", "seasonPassPoints", "seasonPassLvl", "seasonPassTiers", "seasonPassPointReqs")]
	public class ES3UserType_SeasonPassData : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3UserType_SeasonPassData() : base(typeof(SeasonPassData)){ Instance = this; priority = 1; }


		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (SeasonPassData)obj;
			
			writer.WriteProperty("isPremium", instance.isPremium, ES3Type_bool.Instance);
			writer.WriteProperty("seasonPassLvl", instance.seasonPassLvl, ES3Type_int.Instance);
			writer.WriteProperty("seasonPassTiers", instance.seasonPassTiers, ES3Internal.ES3TypeMgr.GetES3Type(typeof(System.Collections.Generic.List<SeasonPassTier>)));
			writer.WriteProperty("seasonPassPointReqs", instance.seasonPassPointReqs, ES3Internal.ES3TypeMgr.GetES3Type(typeof(System.Collections.Generic.List<System.Int32>)));
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (SeasonPassData)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "isPremium":
						instance.isPremium = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "seasonPassLvl":
						instance.seasonPassLvl = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "seasonPassTiers":
						instance.seasonPassTiers = reader.Read<System.Collections.Generic.List<SeasonPassTier>>();
						break;
					case "seasonPassPointReqs":
						instance.seasonPassPointReqs = reader.Read<System.Collections.Generic.List<double>>();
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new SeasonPassData();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}


	public class ES3UserType_SeasonPassDataArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_SeasonPassDataArray() : base(typeof(SeasonPassData[]), ES3UserType_SeasonPassData.Instance)
		{
			Instance = this;
		}
	}
}