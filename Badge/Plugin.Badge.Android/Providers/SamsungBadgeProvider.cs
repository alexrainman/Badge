using Android.Content;
using Android.Database;
using Android.OS;

namespace Plugin.Badge
{
	class SamsungBadgeProvider : BadgeProvider {

		static string CONTENT_URI = "content://com.sec.badge/apps?notify=true";
		static string[] CONTENT_PROJECTION = new string[] { "_id", "class" };

		DefaultBadgeProvider defaultBadger;

		public SamsungBadgeProvider()
		{
			if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
			{
				defaultBadger = new DefaultBadgeProvider();
			}
		}

		public override void SetBadge(int badgeNumber)
		{
			if (defaultBadger != null && defaultBadger.IsSupported())
			{
				defaultBadger.SetBadge(badgeNumber);
			}
			else
			{
				var mUri = Android.Net.Uri.Parse(CONTENT_URI);
				var contentResolver = mContext.ContentResolver;
				ICursor cursor = null;
				try
				{
					cursor = contentResolver.Query(mUri, CONTENT_PROJECTION, "package=?", new string[] { GetPackageName() }, null);
					if (cursor != null)
					{
						var entryActivityName = GetClassName();
						bool entryActivityExist = false;
						while (cursor.MoveToNext())
						{
							var id = cursor.GetInt(0);
							var contentValues = GetContentValues(GetComponentName(), badgeNumber, false);
							contentResolver.Update(mUri, contentValues, "_id=?", new string[] { id.ToString() });
							if (entryActivityName.Equals(cursor.GetString(cursor.GetColumnIndex("class"))))
							{
								entryActivityExist = true;
							}
						}

						if (!entryActivityExist)
						{
							var contentValues = GetContentValues(GetComponentName(), badgeNumber, true);
							contentResolver.Insert(mUri, contentValues);
						}
					}
				}
				finally
				{
					if (cursor != null && !cursor.IsClosed)
					{
						cursor.Close();
					}
				}
			}
		}

		public override void ClearBadge()
		{
			SetBadge(0);
		}

		private ContentValues GetContentValues(ComponentName componentName, int badgeCount, bool isInsert)
		{
			var contentValues = new ContentValues();
			if (isInsert)
			{
				contentValues.Put("package", componentName.PackageName);
				contentValues.Put("class", componentName.ClassName);
			}
			contentValues.Put("badgecount", badgeCount);
			return contentValues;
		}
	}
}
