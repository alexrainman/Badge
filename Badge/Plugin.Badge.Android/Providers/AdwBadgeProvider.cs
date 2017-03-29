using System;
using Android.Content;

namespace Plugin.Badge
{
	class AdwBadgeProvider : BadgeProvider {

		public override void SetBadge(int badgeNumber) {
			
			if (badgeNumber < 0)
			{
				return;
			}

			try
			{
				var intent = new Intent("org.adw.launcher.counter.SEND");
				intent.PutExtra("PNAME", GetPackageName());
				intent.PutExtra("CNAME", GetClassName());
				intent.PutExtra("COUNT", badgeNumber);
				mContext.SendBroadcast(intent);
			}
			catch (Exception ex)
			{
				Console.WriteLine("(ADW) unable to set badge: " + ex.Message);
			}
		}

		public override void ClearBadge() {
			SetBadge(0);
		}
	}
}