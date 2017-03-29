using System;
using Android.Content;
using Android.OS;
using Java.IO;
using Java.Lang;
using Java.Lang.Reflect;

namespace Plugin.Badge
{
	public class OppoBadgeProvider : BadgeProvider
	{
		//@TargetApi(Build.VERSION_CODES.HONEYCOMB)
		public override void SetBadge(int badgeNumber)
		{
			if (badgeNumber < -1)
			{
				return;
			}

	        if (badgeNumber == 0) {
				badgeNumber = -1;
			}

			try
			{
				var intent = new Intent("com.oppo.unsettledevent");
				intent.PutExtra("packageName", GetPackageName());
				intent.PutExtra("number", badgeNumber);
				intent.PutExtra("upgradeNumber", badgeNumber);
				mContext.SendBroadcast(intent);
			}
			catch (System.Exception ex)
			{
				int version = GetSupportVersion();
				if (version == 6)
				{
					try
					{
						var extras = new Bundle();
						extras.PutInt("app_badge_count", badgeNumber);
						mContext.ContentResolver.Call(Android.Net.Uri.Parse("content://com.android.badge/badge"), "setAppBadgeCount", null, extras);
					}
					catch (Throwable th)
					{
						System.Console.WriteLine("(OPPO) unable to set badge: " + th.Message);
					}
				}
			}	           
	    }

		private int GetSupportVersion()
		{
			int ROMVERSION = -1;

			try
			{
				ROMVERSION = ((Integer)ExecuteClassLoad(GetClass("com.color.os.ColorBuild"), "getColorOSVERSION", null, null)).IntValue();
			}
			catch (System.Exception e)
			{
				ROMVERSION = 0;
			}

			if (ROMVERSION == 0)
			{
				try
				{
					var str = GetSystemProperty("ro.build.version.opporom");
					if (str.StartsWith("V1.4", StringComparison.CurrentCulture))
					{
						return 3;
					}
					if (str.StartsWith("V2.0", StringComparison.CurrentCulture))
					{
						return 4;
					}
					if (str.StartsWith("V2.1", StringComparison.CurrentCulture))
					{
						return 5;
					}
				}
				catch (System.Exception ignored)
				{
					System.Console.WriteLine(ignored.Message);
				}
			}

			return ROMVERSION;
		}


		private object ExecuteClassLoad(Class cls, string str, Class[] clsArr, Java.Lang.Object[] objArr)
		{
			object obj = null;
			if (!(cls == null || CheckObjExists(str)))
			{
				var method = GetMethod(cls, str, clsArr);
				if (method != null)
				{
					method.Accessible = true;
					try
					{
						obj = method.Invoke(null, objArr);
					}
					catch (IllegalAccessException e)
					{
						e.PrintStackTrace();
					}
					catch (InvocationTargetException e)
					{
						e.PrintStackTrace();
					}
				}
			}
			return obj;
		}

		private Method GetMethod(Class cls, string str, Class[] clsArr)
		{
			Method method = null;
			if (cls == null || CheckObjExists(str))
			{
				return method;
			}
			try
			{
				cls.GetMethods();
				cls.GetDeclaredMethods();
				return cls.GetDeclaredMethod(str, clsArr);
			}
			catch (System.Exception e)
			{
				System.Console.WriteLine(e.Message);
				try
				{
					return cls.GetMethod(str, clsArr);
				}
				catch (System.Exception e2)
				{
					System.Console.WriteLine(e2.Message);
					return cls.Superclass != null ? GetMethod(cls.Superclass, str, clsArr) : method;
				}
			}
		}

		private Class GetClass(string str)
		{
			Class cls = null;
			try
			{
				cls = Class.ForName(str);
			}
			catch (ClassNotFoundException ignored)
			{
				System.Console.WriteLine(ignored.Message);
			}
			return cls;
		}


		private bool CheckObjExists(object obj)
		{
			return obj == null || obj.ToString().Equals("") || obj.ToString().Trim().Equals("null");
		}


		private string GetSystemProperty(string propName)
		{
			string line;
			BufferedReader input = null;
			try
			{
				var p = Runtime.GetRuntime().Exec("getprop " + propName);
				input = new BufferedReader(new InputStreamReader(p.InputStream), 1024);
				line = input.ReadLine();
				input.Close();
			}
			catch (IOException ex)
			{
				System.Console.WriteLine(ex.Message);
				return null;
			}
			finally
			{
				try
				{
					if (input != null)
					{
						input.Close();
					}
				}
				catch (IOException e)
				{

				}
			}
			return line;
		}
	}
}
