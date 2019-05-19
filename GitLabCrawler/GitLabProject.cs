using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitLabCrawler
{
    public class GitLabProject
    {
        public long id;

        public string name;

        public string description;

        public DateTime created_at;

        public string web_url;

        public string avatar_url;

        public long creator_id;

        public GitLabProject() { }

        public GitLabProject(long id, string name, string description, DateTime created_at, string web_url, string avatar_url, long creator_id)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.created_at = created_at;
            this.web_url = web_url;
            this.avatar_url = avatar_url;
            this.creator_id = creator_id;
        }
    }
}
