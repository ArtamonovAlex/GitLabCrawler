using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitLabCrawler
{
    public class GitLabUser
    {
        public long id;

        public string name;

        public string username;

        public string web_url;

        public string avatar_url;

        public GitLabUser()
        {
        }

        public GitLabUser(long id, string name, string username, string web_url, string avatar_url)
        {
            this.id = id;
            this.name = name;
            this.username = username;
            this.web_url = web_url;
            this.avatar_url = avatar_url;
        }
    }
}
