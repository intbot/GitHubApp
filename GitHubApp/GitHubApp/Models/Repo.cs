using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubApp.Models
{
    public class Repo
    {
        public string AvatarUrl { get; internal set; }
        public string Description { get; internal set; }
        public string FullName { get; internal set; }
        public string Owner { get; internal set; }
        public int Stars { get; internal set; }
    }
}
