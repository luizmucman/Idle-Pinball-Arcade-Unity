using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("boostID", "boostImg", "boostAmt", "endTime", "inUse", "qtyOwned", "boostLength")]
	public class ES3UserType_BoostData : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3UserType_BoostData() : base(typeof(BoostData)){ Instance = this; priority = 1; }


		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (BoostData)obj;
			
			writer.WriteProperty("boostID", instance.boostID, ES3Type_string.Instance);
			writer.WritePropertyByRef("boostImg", instance.boostImg);
			writer.WriteProperty("boostAmt", instance.boostAmt, ES3Type_double.Instance);
			writer.WriteProperty("endTime", instance.endTime, ES3Type_DateTime.Instance);
			writer.WriteProperty("inUse", instance.inUse, ES3Type_bool.Instance);
			writer.WriteProperty("qtyOwned", instance.qtyOwned, ES3Type_int.Instance);
			writer.WriteProperty("boostLength", instance.boostLength, ES3Type_double.Instance);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (BoostData)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "boostID":
						instance.boostID = reader.Read<System.String>(ES3Type_string.Instance);
						break;
					case "boostImg":
						instance.boostImg = reader.Read<UnityEngine.Sprite>(ES3Type_Sprite.Instance);
						break;
					case "boostAmt":
						instance.boostAmt = reader.Read<System.Double>(ES3Type_double.Instance);
						break;
					case "endTime":
						instance.endTime = reader.Read<System.DateTime>(ES3Type_DateTime.Instance);
						break;
					case "inUse":
						instance.inUse = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "qtyOwned":
						instance.qtyOwned = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "boostLength":
						instance.boostLength = reader.Read<System.Double>(ES3Type_double.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new BoostData();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}


	public class ES3UserType_BoostDataArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_BoostDataArray() : base(typeof(BoostData[]), ES3UserType_BoostData.Instance)
		{
			Instance = this;
		}
	}
}