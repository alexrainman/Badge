using System;
using Android.Content;

namespace Plugin.Badge
{
	class HtcBadgeProvider : BadgeProvider {

	    public override void SetBadge(int badgeNumber) {

			if (badgeNumber < 0)
			{
				return;
			}

	        try
			{
				//var launchIntent = mContext.PackageManager.GetLaunchIntentForPackage(mContext.PackageName);
				//var componentName = launchIntent.Component;

				/*var intent2 = new Intent("com.htc.launcher.action.UPDATE_SHORTCUT");
				intent2.PutExtra("packagename", componentName.PackageName);
				intent2.PutExtra("count", badgeNumber);
				mContext.SendBroadcast(intent2);

				var intent1 = new Intent("com.htc.launcher.action.SET_NOTIFICATION");
				intent1.PutExtra("com.htc.launcher.extra.COMPONENT", componentName.FlattenToShortString());
				intent1.PutExtra("com.htc.launcher.extra.COUNT", badgeNumber);
				mContext.SendBroadcast(intent1);*/

				var intent = new Intent("com.htc.launcher.action.UPDATE_SHORTCUT");
				intent.PutExtra("packagename", GetPackageName());
				intent.PutExtra("count", badgeNumber);
				mContext.SendBroadcast(intent);

				var setNotificationIntent = new Intent("com.htc.launcher.action.SET_NOTIFICATION");
				var componentName = new ComponentName(GetPackageName(), GetClassName());
				setNotificationIntent.PutExtra("com.htc.launcher.extra.COMPONENT", componentName.FlattenToShortString());
				setNotificationIntent.PutExtra("com.htc.launcher.extra.COUNT", badgeNumber);
				mContext.SendBroadcast(setNotificationIntent);

			} catch (Exception ex) {
				Console.WriteLine("(HTC) unable to set badge: " + ex.Message);
			}
	    }
			
	    public override void ClearBadge() {
	        SetBadge(0);
	    }
	}
}