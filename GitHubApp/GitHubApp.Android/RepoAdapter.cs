using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Views;
using Android.Widget;
using GitHubApp.Models;
using FFImageLoading;
using FFImageLoading.Views;

namespace GitHubApp.Droid
{
    public class RepoAdapter : BaseAdapter<Repo>
    {
        private List<Repo> _repos;
        private readonly Activity _context;

        public override Repo this[int position] => _repos[position];

        public override int Count => _repos.Count;

        public override long GetItemId(int position) => position;

        public RepoAdapter(Activity activity, IEnumerable<Repo> repos)
        {
            _repos = repos.ToList();
            _context = activity;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            var repo = _repos[position];

            if(view == null)
            {
                view = _context.LayoutInflater.Inflate(Resource.Layout.RepoListItem, null);
                view.FindViewById<TextView>(Resource.Id.repoDescrTextView).Text = repo.Description;
                view.FindViewById<TextView>(Resource.Id.repoNameTextView).Text = repo.FullName;
                view.FindViewById<TextView>(Resource.Id.repoOwnerTextView).Text = $"Built by: {repo.Owner} - Stars: {repo.Stars}";

                var avatarImage = view.FindViewById<ImageViewAsync>(Resource.Id.avatarImage);
                ImageService.Instance.LoadUrl(repo.AvatarUrl)
                   .Retry(3, 200)
                   .DownSample(100, 100)
                   .Into(avatarImage);
            }

            return view;
        }
    }
}