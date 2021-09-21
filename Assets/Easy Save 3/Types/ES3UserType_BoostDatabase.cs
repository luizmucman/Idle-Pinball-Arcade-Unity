using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("database")]
	public class ES3UserType_BoostDatabase : ES3ScriptableObjectType
	{
		public static ES3Type Instance = null;

		public ES3UserType_BoostDatabase() : base(typeof(BoostDatabase)){ Instance = this; priority = 1; }


		protected override void WriteScriptableObject(object obj, ES3Writer writer)
		{
			var instance = (BoostDatabase)obj;
			
			writer.WriteProperty("database", instance.database, ES3Internal.ES3TypeMgr.GetES3Type(typeof(System.Collections.Generic.List<BoostData>)));
		}

		protected override void ReadScriptableObject<T>(ES3Reader reader, object obj)
		{
			var instance = (BoostDatabase)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "database":
						instance.database = reader.Read<System.Collections.Generic.List<BoostData>>();
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_BoostDatabaseArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_BoostDatabaseArray() : base(typeof(BoostDatabase[]), ES3UserType_BoostDatabase.Instance)
		{
			Instance = this;
		}
	}
}