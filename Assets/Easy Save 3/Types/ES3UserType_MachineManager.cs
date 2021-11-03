using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute()]
	public class ES3UserType_MachineManager : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_MachineManager() : base(typeof(MachineManager)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (MachineManager)obj;
			
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (MachineManager)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
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