using Android.App;
using Android.Widget;
using Android.OS;

namespace GitHubApp.Droid
{
	[Activity (Label = "Top Repos", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

            var api = new ApiInterface();

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            var repos = api.GetRepos();
            var repoListView = FindViewById<ListView>(Resource.Id.repoListView);
            repoListView.Adapter = new RepoAdapter(this, repos);
		}
	}
}