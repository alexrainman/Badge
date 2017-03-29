using System;
using Android.OS;

namespace Plugin.Badge
{
	public class ZukBadgeProvider : BadgeProvider
	{
		public override void SetBadge(int badgeNumber)
		{
			if (badgeNumber < 0)
			{
				return;
			}

			try
			{
				var extra = new Bundle();
				extra.PutInt("app_badge_count", badgeNumber);
				mContext.ContentResolver.Call(Android.Net.Uri.Parse("content://com.android.badge/badge"), "setAppBadgeCount", null, extra);
			} catch (Java.Lang.IllegalArgumentException ex) {
				Console.WriteLine("(ZUK) unable to set badge: " + ex.Message);
			} catch (Exception ex) {
				Console.WriteLine("(ZUK) unable to set badge: " + ex.Message);
			}
		}

		public override void ClearBadge()
		{
			SetBadge(0);
		}
	}
}
