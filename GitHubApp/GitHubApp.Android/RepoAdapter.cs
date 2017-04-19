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
    class ItemViewHolder : Java.Lang.Object
    {
        public ImageViewAsync AvatarImage { get; internal set; }
        public TextView RepoDescrTextView { get; internal set; }
        public TextView RepoNameTextView { get; internal set; }
        public TextView RepoOwnerTextView { get; internal set; }
    }

    public class RepoAdapter : BaseAdapter<Repo>
    {
        public List<Repo> Repos;
        private readonly Activity _context;

        public override Repo this[int position] => Repos[position];

        public override int Count => Repos.Count;

        public override long GetItemId(int position) => position;

        public RepoAdapter(Activity activity, IEnumerable<Repo> repos)
        {
            Repos = repos.ToList();
            _context = activity;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var repo = Repos[position];

            //var view = convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.RepoListItem, null);
            //view.FindViewById<TextView>(Resource.Id.repoDescrTextView).Text = repo.Description;
            //view.FindViewById<TextView>(Resource.Id.repoNameTextView).Text = repo.FullName;
            //view.FindViewById<TextView>(Resource.Id.repoOwnerTextView).Text = $"Built by: {repo.Owner} - Stars: {repo.Stars}";

            //var avatarImage = view.FindViewById<ImageViewAsync>(Resource.Id.avatarImage);
            //ImageService.Instance.LoadUrl(repo.AvatarUrl)
            //   .Retry(3, 200)
            //   .DownSample(100, 100)
            //   .Into(avatarImage);

            var view = convertView;
            ItemViewHolder holder = null;
            if (view != null)
                holder = view.Tag as ItemViewHolder;

            if (holder == null)
            {
                view = _context.LayoutInflater.Inflate(Resource.Layout.RepoListItem, null);
                holder = new ItemViewHolder();
                holder.RepoDescrTextView = view.FindViewById<TextView>(Resource.Id.repoDescrTextView);
                holder.RepoNameTextView = view.FindViewById<TextView>(Resource.Id.repoNameTextView);
                holder.RepoOwnerTextView = view.FindViewById<TextView>(Resource.Id.repoOwnerTextView);
                holder.AvatarImage = view.FindViewById<ImageViewAsync>(Resource.Id.avatarImage);

                view.Tag = holder;
            }

            holder.RepoDescrTextView.Text = repo.Description;
            holder.RepoNameTextView.Text = repo.FullName;
            holder.RepoOwnerTextView.Text = $"Built by: {repo.Owner} - Stars: {repo.Stars}";

            ImageService.Instance.LoadUrl(repo.AvatarUrl)
                .Retry(3, 200)
                .DownSample(100, 100)
                .Into(holder.AvatarImage);
            
            return view;
        }
    }
}