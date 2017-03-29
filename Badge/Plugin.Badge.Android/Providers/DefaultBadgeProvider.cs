using System;
using Android.Content;

namespace Plugin.Badge
{
	class DefaultBadgeProvider : BadgeProvider {

		string INTENT_ACTION = "android.intent.action.BADGE_COUNT_UPDATE";

		public override void SetBadge(int badgeNumber) {

			if (badgeNumber < 0)
			{
				return;
			}

			try
			{
				var intent = new Intent(INTENT_ACTION);
				intent.PutExtra("badge_count_package_name", GetPackageName());
				intent.PutExtra("badge_count_class_name", GetClassName());
				intent.PutExtra("badge_count", badgeNumber);
				mContext.SendBroadcast(intent);
			}
			catch (Exception ex)
			{
				Console.WriteLine("(DEFAULT) unable to set badge: " + ex.Message);
			}
		}

		public override void ClearBadge() {
			SetBadge(0);
		}

		public bool IsSupported()
		{
			var intent = new Intent(INTENT_ACTION);
			var packageManager = mContext.PackageManager;
			var receivers = packageManager.QueryBroadcastReceivers(intent, 0);
			return receivers != null && receivers.Count > 0;
		}
	}
}