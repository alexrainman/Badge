using Plugin.Badge.Abstractions;
using Java.Lang;
using System;
using Plugin.CurrentActivity;

/*

<!--for apex-->
<uses-permission android:name="com.anddoes.launcher.permission.UPDATE_COUNT"/>

<!-- for android -->
<uses-permission android:name="com.android.launcher.permission.READ_SETTINGS"/>
<uses-permission android:name="com.android.launcher.permission.WRITE_SETTINGS"/>
<uses-permission android:name="com.android.launcher.permission.INSTALL_SHORTCUT" />
<uses-permission android:name="com.android.launcher.permission.UNINSTALL_SHORTCUT" />

<!-- HTC -->
<uses-permission android:name="com.htc.launcher.permission.READ_SETTINGS" />
<uses-permission android:name="com.htc.launcher.permission.UPDATE_SHORTCUT" /> 

<!-- Samsung -->
<uses-permission android:name="com.sec.android.provider.badge.permission.READ" />
<uses-permission android:name="com.sec.android.provider.badge.permission.WRITE" />

<!-- Sony -->
<uses-permission android:name="com.sonyericsson.home.permission.BROADCAST_BADGE" />
<uses-permission android:name="com.sonymobile.home.permission.PROVIDER_INSERT_BADGE" />

*/

namespace Plugin.Badge
{
	/// <summary>
	/// Implementation for Feature
	/// </summary>
	public class BadgeImplementation : IBadge
	{
		BadgeProviderFactory badgeFactory = new BadgeProviderFactory();

		/// <summary>
		/// Sets the badge.
		/// </summary>
		/// <param name="badgeNumber">The badge number.</param>
		public void SetBadge(int badgeNumber)
		{
			var mContext = CrossCurrentActivity.Current.Activity;

			if (mContext == null)
			{
				return;
			}

			//var badgeFactory = new BadgeProviderFactory();
			var badgeProvider = badgeFactory.GetBadgeProvider();

			try
			{
				badgeProvider.SetBadge(badgeNumber);
			}
			catch (UnsupportedOperationException e)
			{
				Console.WriteLine(string.Format("The home launcher with package {0} may not be supported by Badge plugin: ({1})", badgeProvider.GetPackageName(), e.Message));
			}
		}

		/// <summary>
		/// Clears the badge.
		/// </summary>
		public void ClearBadge()
		{
			SetBadge(0);
		}
	}
}