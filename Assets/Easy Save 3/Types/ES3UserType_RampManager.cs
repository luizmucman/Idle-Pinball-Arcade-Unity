using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("upgradeData")]
	public class ES3UserType_RampManager : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_RampManager() : base(typeof(RampManager)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (RampManager)obj;
			
			writer.WriteProperty("upgradeData", instance.upgradeData);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (RampManager)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "upgradeData":
						instance.upgradeData = reader.Read<UpgradeData>();
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_RampManagerArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_RampManagerArray() : base(typeof(RampManager[]), ES3UserType_RampManager.Instance)
		{
			Instance = this;
		}
	}
}