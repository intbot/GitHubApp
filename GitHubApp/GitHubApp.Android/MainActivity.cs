using Android.App;
using Android.Widget;
using Android.OS;

namespace GitHubApp.Droid
{
	[Activity (Label = "Top Repos", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
        ApiInterface api = new ApiInterface();

        protected async override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            var repos = await api.GetReposAsync();
            var repoListView = FindViewById<ListView>(Resource.Id.repoListView);
            repoListView.Adapter = new RepoAdapter(this, repos);

            var weekButton = FindViewById<Button>(Resource.Id.weekButtom);
            weekButton.Click += (sender, e) => ButtonClick(sender, e, 7);

            var monthButton = FindViewById<Button>(Resource.Id.monthButton);
            monthButton.Click += (sender, e) => ButtonClick(sender, e, 30);

            var yearButton = FindViewById<Button>(Resource.Id.yearButton);
            yearButton.Click += (sender, e) => ButtonClick(sender, e, 365);
        }

        private async void ButtonClick(object sender, System.EventArgs e, int days)
        {
            var repos = await api.GetReposAsync(days);
            var repoListView = FindViewById<ListView>(Resource.Id.repoListView);

            var adapter = repoListView.Adapter as RepoAdapter;
            adapter.Repos = repos;
            adapter.NotifyDataSetChanged();
        }
    }
}