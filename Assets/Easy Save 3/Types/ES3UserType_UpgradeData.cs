using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("level")]
	public class ES3UserType_UpgradeData : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3UserType_UpgradeData() : base(typeof(UpgradeData)){ Instance = this; priority = 1; }


		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (UpgradeData)obj;
			
			writer.WriteProperty("level", instance.level, ES3Type_int.Instance);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (UpgradeData)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "level":
						instance.level = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new UpgradeData();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}


	public class ES3UserType_UpgradeDataArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_UpgradeDataArray() : base(typeof(UpgradeData[]), ES3UserType_UpgradeData.Instance)
		{
			Instance = this;
		}
	}
}