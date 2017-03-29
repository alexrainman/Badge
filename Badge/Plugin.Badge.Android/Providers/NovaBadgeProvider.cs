using System;
using Android.Content;

namespace Plugin.Badge
{
	class NovaBadgeProvider : BadgeProvider {

		public override void SetBadge(int badgeNumber) {

			if (badgeNumber < 0)
			{
				return;
			}

			try {
				var contentValues = new ContentValues();
				contentValues.Put("tag", GetPackageName() + "/" + GetClassName());
				contentValues.Put("count", badgeNumber);
				mContext.ContentResolver.Insert(Android.Net.Uri.Parse("content://com.teslacoilsw.notifier/unread_count"), contentValues);
			} catch (Java.Lang.IllegalArgumentException ex) {
				/* Fine, TeslaUnread is not installed. */
				Console.WriteLine("(NOVA) unable to set badge: " + ex.Message);
			} catch (Exception ex) {
				/* Some other error, possibly because the format of the ContentValues are incorrect. */
				Console.WriteLine("(NOVA) unable to set badge: " + ex.Message);
			}
		}

		public override void ClearBadge() {
			SetBadge(0);
		}
	}
}

