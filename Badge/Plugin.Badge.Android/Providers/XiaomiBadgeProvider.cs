
using System;
using Android.Content;
using Java.Lang;
using Java.Lang.Reflect;

namespace Plugin.Badge
{
	/**
	 * @author leolin
	 * 
	 * ported to C# by Alex Rainman
	 */
	class XiaomiBadgeProvider : BadgeProvider {

		public override void SetBadge(int badgeNumber) {
			try {
				try
				{
					var miuiNotificationClass = Class.ForName("android.app.MiuiNotification");
					var miuiNotification = miuiNotificationClass.NewInstance();
					var field = miuiNotification.Class.GetDeclaredField("messageCount");
					field.Accessible = true;
					field.Set(miuiNotification, badgeNumber == 0 ? "" : badgeNumber.ToString());
				}
				catch (System.Exception ex)
				{
					Console.WriteLine("(XIAOMI) unable to set badge: " + ex.Message);
				}
			} catch (System.Exception e) {
				try
				{
					var localIntent = new Intent("android.intent.action.APPLICATION_MESSAGE_UPDATE");
					localIntent.PutExtra("android.intent.extra.update_application_component_name", GetPackageName() + "/" + GetClassName());
					localIntent.PutExtra("android.intent.extra.update_application_message_text", badgeNumber == 0 ? "" : badgeNumber.ToString());
					mContext.SendBroadcast(localIntent);
				}
				catch (System.Exception ex)
				{
					Console.WriteLine("(XIAOMI) unable to set badge: " + ex.Message);
				}
			}
		}

		public override void ClearBadge() {
			SetBadge(0);
		}
	}
}

