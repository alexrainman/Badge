using Android.Content;
using System.Collections.Generic;
using Android.Content.PM;
using Plugin.CurrentActivity;

namespace Plugin.Badge
{
	public class BadgeProviderFactory {

		Dictionary<string, BadgeProvider> providers;

		/// <summary>
		/// Badge provider factory constructor
		/// </summary>
	    public BadgeProviderFactory() {
	        
			providers = new Dictionary<string, BadgeProvider>();

			providers.Add("org.adw.launcher", new AdwBadgeProvider());
			providers.Add("org.adwfreak.launcher", new AdwBadgeProvider());
			providers.Add("com.anddoes.launcher", new ApexBadgeProvider());
			providers.Add("com.asus.launcher", new AsusBadgeProvider());
			providers.Add("com.htc.launcher", new HtcBadgeProvider());
			//providers.Add("com.huawei.android.launcher", new HuaweiBadgeProvider());
			providers.Add("com.teslacoilsw.launcher", new NovaBadgeProvider());
			providers.Add("com.oppo.launcher", new OppoBadgeProvider());
			providers.Add("com.sec.android.app.launcher", new SamsungBadgeProvider());
			providers.Add("com.sec.android.app.twlauncher", new SamsungBadgeProvider());
			providers.Add("com.sonyericsson.home", new SonyBadgeProvider());
			providers.Add("com.sonymobile.home", new SonyBadgeProvider());
			providers.Add("com.miui.miuilite", new XiaomiBadgeProvider());
			providers.Add("com.miui.home", new XiaomiBadgeProvider());
			providers.Add("com.miui.miuihome", new XiaomiBadgeProvider());
			providers.Add("com.miui.miuihome2", new XiaomiBadgeProvider());
			providers.Add("com.miui.mihome", new XiaomiBadgeProvider());
			providers.Add("com.miui.mihome2", new XiaomiBadgeProvider());
			providers.Add("com.i.miui.launcher", new XiaomiBadgeProvider());
			providers.Add("com.zui.launcher", new ZukBadgeProvider());
	    }

		/// <summary>
		/// Get badge provider
		/// </summary>
	    public BadgeProvider GetBadgeProvider() {
			var currentPackage = GetHomePackage();
			return providers.ContainsKey (currentPackage) ? providers [currentPackage] : new DefaultBadgeProvider ();
	    }

		public string GetHomePackage()
		{
			var intent = new Intent(Intent.ActionMain);
			intent.AddCategory(Intent.CategoryHome);
			var mContext = CrossCurrentActivity.Current.Activity;
			var resolveInfo = mContext.PackageManager.ResolveActivity(intent, PackageInfoFlags.MatchDefaultOnly);
			if (resolveInfo != null && resolveInfo.ActivityInfo != null && resolveInfo.ActivityInfo.PackageName != null)
			{
				return resolveInfo.ActivityInfo.PackageName;
			}
			return mContext.PackageName;
		}
	}
}