using Plugin.Badge.Abstractions;
#if WINDOWS_PHONE
using System.Linq;
using Microsoft.Phone.Shell;
#else
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
#endif

namespace Plugin.Badge
{
    /// <summary>
    /// Implementation for Feature
    /// </summary>
    public class BadgeImplementation : IBadge
    {
        /// <summary>
        /// Sets the badge.
        /// </summary>
        /// <param name="badgeNumber">The badge number.</param>
        /// <param name="title">The title. Used only by Android</param>
        public void SetBadge(int badgeNumber)
        {
#if WINDOWS_PHONE
            SetBadgeWP8(badgeNumber);
#else
            //https://docs.microsoft.com/en-us/windows/uwp/controls-and-patterns/tiles-and-notifications-badges
            var badgeXml = BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeNumber);
            var badgeElement = (XmlElement)badgeXml.SelectSingleNode("/badge");
            badgeElement.SetAttribute("value", badgeNumber.ToString());
            //XmlDocument badgeXml = new XmlDocument();
            //badgeXml.LoadXml(string.Format("<badge value='{0}'/>", badgeNumber));
            var badge = new BadgeNotification(badgeXml);
            BadgeUpdateManager.CreateBadgeUpdaterForApplication().Update(badge);
            BadgeUpdateManager.CreateBadgeUpdaterForApplication().Update(badge);
#endif
        }

#if WINDOWS_PHONE
        private void SetBadgeWP8(int badgeNumber)
        {
            // Application Tile is always the first Tile, even if it is not pinned to Start. 
            var TileToFind = ShellTile.ActiveTiles.First();
            // Application should always be found 
            if (TileToFind != null)
            {
                // set the properties to update for the Application Tile 
                // Empty strings for the text values and URIs will result in the property being cleared. 
                StandardTileData NewTileData = new StandardTileData
                {
                    Count = badgeNumber
                };
                // Update the Application Tile 
                TileToFind.Update(NewTileData);
            }
        }
#endif

        /// <summary>
        /// Clears the badge.
        /// </summary>
        public void ClearBadge()
        {
#if WINDOWS_PHONE
            SetBadgeWP8(0);
#else
            BadgeUpdateManager.CreateBadgeUpdaterForApplication().Clear();
#endif
        }
    }
}