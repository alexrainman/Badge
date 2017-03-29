using System;
using Android.Content;

namespace Plugin.Badge
{
	class SonyBadgeProvider : BadgeProvider
	{


		public override void SetBadge(int badgeNumber)
		{
			if (badgeNumber < 0)
			{
				return;
			}

			if (mContext.PackageManager.ResolveContentProvider("com.sonymobile.home.resourceprovider", 0) != null)
			{
				try
				{
					var mQueryHandler = new MyHandler(mContext.ContentResolver);
					var contentValues = new ContentValues();
					contentValues.Put("badge_count", badgeNumber);
					contentValues.Put("package_name", GetPackageName());
					contentValues.Put("activity_name", GetClassName());
					// The badge must be inserted on a background thread
					mQueryHandler.StartInsert(0, null, Android.Net.Uri.Parse("content://com.sonymobile.home.resourceprovider/badge"), contentValues);
				}
				catch (Exception ex)
				{
					Console.WriteLine("(SONY) unable to set badge: " + ex.Message);
				}
			}
			else
			{
				try
				{
					var intent = new Intent();
					intent.SetAction("com.sonyericsson.home.action.UPDATE_BADGE");
					intent.PutExtra("com.sonyericsson.home.intent.extra.badge.PACKAGE_NAME", GetPackageName());
					intent.PutExtra("com.sonyericsson.home.intent.extra.badge.ACTIVITY_NAME", GetClassName());
					intent.PutExtra("com.sonyericsson.home.intent.extra.badge.SHOW_MESSAGE", badgeNumber > 0);
					intent.PutExtra("com.sonyericsson.home.intent.extra.badge.MESSAGE", badgeNumber.ToString());
					mContext.SendBroadcast(intent);
				}
				catch (Exception ex)
				{
					Console.WriteLine("(SONY) unable to set badge: " + ex.Message);
				}
			}
		}

		public override void ClearBadge()
		{
			SetBadge(0);
		}
	}

	public class MyHandler : AsyncQueryHandler
	{
		public MyHandler(ContentResolver cr) : base(cr)
		{
		}
	}
}