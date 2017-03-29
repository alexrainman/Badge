using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Plugin.Badge;

namespace Demo.Droid
{
	[Activity(Label = "Demo.Droid", MainLauncher = true, Theme = "@style/MyTheme")]
	public class MainActivity : AppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			CrossBadge.Current.SetBadge(9);
		}
	}
}

