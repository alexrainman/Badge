using System;
using Android.Content;

namespace Plugin.Badge
{
	class ApexBadgeProvider : BadgeProvider {

		public override void SetBadge(int badgeNumber) {

			if (badgeNumber < 0)
			{
				return;
			}

			try
			{
				var intent = new Intent("com.anddoes.launcher.COUNTER_CHANGED");
				intent.PutExtra("package", GetPackageName());
				intent.PutExtra("class", GetClassName());
				intent.PutExtra("count", badgeNumber);
				mContext.SendBroadcast(intent);
			}
			catch (Exception ex)
			{
				Console.WriteLine("(APEX) unable to set badge: " + ex.Message);
			}
		}

		public override void ClearBadge() {
			SetBadge(0);
		}
	}
}

