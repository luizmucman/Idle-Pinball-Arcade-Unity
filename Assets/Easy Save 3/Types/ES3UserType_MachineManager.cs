using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("equippedBallCount", "maxEquippedBalls")]
	public class ES3UserType_MachineManager : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_MachineManager() : base(typeof(MachineManager)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (MachineManager)obj;
			
			writer.WriteProperty("equippedBallCount", instance.equippedBallCount);
			writer.WriteProperty("maxEquippedBalls", instance.maxEquippedBalls, ES3Type_int.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (MachineManager)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "equippedBallCount":
						instance.equippedBallCount = reader.Read<System.Collections.Generic.List<System.Int32>>();
						break;
					case "maxEquippedBalls":
						instance.maxEquippedBalls = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_MachineManagerArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_MachineManagerArray() : base(typeof(MachineManager[]), ES3UserType_MachineManager.Instance)
		{
			Instance = this;
		}
	}
}