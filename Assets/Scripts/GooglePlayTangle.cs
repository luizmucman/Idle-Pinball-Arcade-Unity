#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("6BYyhEAvexAUZ5Tab/H+lniGn4QRT7kcmjM3EBg6LoMnAwYgJOJBD1/21YGbVuaA2nV0+Jz+Zgms/4kGIlJm/+ODk66VGuPotx8x1VrYLyJPzMLN/U/Mx89PzMzNWj1w+V7sqhX9jMi/FrBRecQ4MmfVUiRb0ti5fM3Jc9a022kbswZOEO+c8JbymTeUzFOjBAZy/riaI1PkseYsnhhNjHrwDhIXSe3o2/e9K8UJp0bS8SdW/U/M7/3Ay8TnS4VLOsDMzMzIzc4ApUpsZ5LsvPrS/69Vdrmd+GfUbbOCf8Xw1Qh7cto7fk8A0DcLjfUqxJGt1LcwJcWOvM1oOZK6LS9+1b81t79pi3lE+sahcmMd0a47vksHzX/gFtx+TTUsMs/OzM3M");
        private static int[] order = new int[] { 10,8,7,5,8,8,8,11,12,10,13,12,12,13,14 };
        private static int key = 205;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
