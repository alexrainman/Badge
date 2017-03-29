using System;
using Android.Content;

namespace Plugin.Badge
{
	class AsusBadgeProvider : BadgeProvider {
		
		public override void SetBadge(int badgeNumber) {

			if (badgeNumber < 0)
			{
				return;
			}

			try
			{
				var intent = new Intent("android.intent.action.BADGE_COUNT_UPDATE");
				intent.PutExtra("badge_count_package_name", GetPackageName());
				intent.PutExtra("badge_count_class_name", GetClassName());
				intent.PutExtra("badge_count", badgeNumber);
				intent.PutExtra("badge_vip_count", 0);
				mContext.SendBroadcast(intent);
			}
			catch (Exception ex)
			{
				Console.WriteLine("(ASUS) unable to set badge: " + ex.Message);
			}
		}

		public override void ClearBadge() {
			SetBadge(0);
		}
	}
}

