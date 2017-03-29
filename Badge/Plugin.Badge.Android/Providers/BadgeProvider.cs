using Android.Content;
using Plugin.CurrentActivity;

namespace Plugin.Badge
{
	public class BadgeProvider {

		/// <summary>
		/// Badge provider context
		/// </summary>
		protected Context mContext
		{
			get
			{
				return CrossCurrentActivity.Current.Activity;
			}
		}

		/// <summary>
		/// Virtual set badge
		/// </summary>
		public virtual void SetBadge(int badgeNumber){
		}

		/// <summary>
		/// Virtual remove badge
		/// </summary>
		public virtual void ClearBadge(){
		}

		/// <summary>
		/// Get component name
		/// </summary>
		public ComponentName GetComponentName()
		{
			return mContext.PackageManager.GetLaunchIntentForPackage(GetPackageName()).Component;
		}

		/// <summary>
		/// Get package name
		/// </summary>
	    public string GetPackageName()
		{
			return mContext.PackageName;
	    }

		/// <summary>
		/// Get main activity class name
		/// </summary>
	    protected string GetClassName()
		{
			return mContext.PackageManager.GetLaunchIntentForPackage(GetPackageName()).Component.ClassName;
	    }
	}
}