using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("machineGUID", "machineName", "machineCost", "isUnlocked", "isPlaying", "isCurrentEvent", "coinsPerSecond", "accumulatedCoins", "machineImage", "awayCheckPoint")]
	public class ES3UserType_MachineData : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3UserType_MachineData() : base(typeof(MachineData)){ Instance = this; priority = 1; }


		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (MachineData)obj;
			
			writer.WriteProperty("machineGUID", instance.machineGUID, ES3Type_string.Instance);
			writer.WriteProperty("machineName", instance.machineName, ES3Type_string.Instance);
			writer.WriteProperty("machineCost", instance.machineCost, ES3Type_double.Instance);
			writer.WriteProperty("isUnlocked", instance.isUnlocked, ES3Type_bool.Instance);
			writer.WriteProperty("isPlaying", instance.isPlaying, ES3Type_bool.Instance);
			writer.WriteProperty("isCurrentEvent", instance.isCurrentEvent, ES3Type_bool.Instance);
			writer.WriteProperty("coinsPerSecond", instance.coinsPerSecond, ES3Type_double.Instance);
			writer.WritePropertyByRef("machineImage", instance.machineImage);
			writer.WriteProperty("awayCheckPoint", instance.awayCheckPoint, ES3Type_DateTime.Instance);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (MachineData)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "machineGUID":
						instance.machineGUID = reader.Read<System.String>(ES3Type_string.Instance);
						break;
					case "machineName":
						instance.machineName = reader.Read<System.String>(ES3Type_string.Instance);
						break;
					case "machineCost":
						instance.machineCost = reader.Read<System.UInt64>(ES3Type_double.Instance);
						break;
					case "isUnlocked":
						instance.isUnlocked = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "isPlaying":
						instance.isPlaying = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "isCurrentEvent":
						instance.isCurrentEvent = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "coinsPerSecond":
						instance.coinsPerSecond = reader.Read<System.UInt64>(ES3Type_double.Instance);
						break;
					case "machineImage":
						instance.machineImage = reader.Read<UnityEngine.Sprite>(ES3Type_Sprite.Instance);
						break;
					case "awayCheckPoint":
						instance.awayCheckPoint = reader.Read<System.DateTime>(ES3Type_DateTime.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new MachineData();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}


	public class ES3UserType_MachineDataArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_MachineDataArray() : base(typeof(MachineData[]), ES3UserType_MachineData.Instance)
		{
			Instance = this;
		}
	}
}