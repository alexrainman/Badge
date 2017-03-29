using System;

namespace Plugin.Badge.Abstractions
{
    /// <summary>
    /// Interface for Badge
    /// </summary>
    public interface IBadge
    {
	    /// <summary>
        /// Clears the badge.
        /// </summary>
        void ClearBadge();

	    /// <summary>
	    /// Sets the badge.
	    /// </summary>
	    /// <param name="badgeNumber">The badge number.</param>
	    void SetBadge(int badgeNumber);
    }
}
