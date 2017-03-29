using Plugin.Badge.Abstractions;
using UIKit;

namespace Plugin.Badge
{
    /// <summary>
    /// Implementation for Badge
    /// </summary>
    public class BadgeImplementation : IBadge
    {
		/// <summary>
		/// Clears the badge.
		/// </summary>
		public void ClearBadge()
		{
			UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
		}

		/// <summary>
		/// Sets the badge.
		/// </summary>
		/// <param name="badgeNumber">The badge number.</param>
		public void SetBadge(int badgeNumber)
		{
			UIApplication.SharedApplication.ApplicationIconBadgeNumber = badgeNumber;
		}
    }
}