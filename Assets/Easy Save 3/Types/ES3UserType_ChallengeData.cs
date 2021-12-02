using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("challengeID", "challengeType", "challengeGoal", "description", "reward", "challengeProgress", "rewardClaimed")]
	public class ES3UserType_ChallengeData : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3UserType_ChallengeData() : base(typeof(ChallengeData)){ Instance = this; priority = 1; }


		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (ChallengeData)obj;
			
			writer.WritePrivateField("challengeID", instance);
			writer.WritePrivateField("challengeType", instance);
			writer.WritePrivateField("challengeGoal", instance);
			writer.WritePrivateField("description", instance);
			writer.WritePrivateFieldByRef("reward", instance);
			writer.WritePrivateField("challengeProgress", instance);
			writer.WritePrivateField("rewardClaimed", instance);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (ChallengeData)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "challengeID":
					reader.SetPrivateField("challengeID", reader.Read<System.Int32>(), instance);
					break;
					case "challengeType":
					reader.SetPrivateField("challengeType", reader.Read<ChallengeType>(), instance);
					break;
					case "challengeGoal":
					reader.SetPrivateField("challengeGoal", reader.Read<System.Int32>(), instance);
					break;
					case "description":
					reader.SetPrivateField("description", reader.Read<System.String>(), instance);
					break;
					case "reward":
					reader.SetPrivateField("reward", reader.Read<SeasonPassItemSO>(), instance);
					break;
					case "challengeProgress":
					reader.SetPrivateField("challengeProgress", reader.Read<System.Int32>(), instance);
					break;
					case "rewardClaimed":
					reader.SetPrivateField("rewardClaimed", reader.Read<System.Boolean>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new ChallengeData();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}


	public class ES3UserType_ChallengeDataArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_ChallengeDataArray() : base(typeof(ChallengeData[]), ES3UserType_ChallengeData.Instance)
		{
			Instance = this;
		}
	}
}