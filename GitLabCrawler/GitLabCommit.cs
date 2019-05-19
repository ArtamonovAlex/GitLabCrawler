using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitLabCrawler
{
    public class GitLabCommit
    {
        public string id;

        public string short_id;

        public long project_id;

        public string title;

        public string message;

        public string author_name;

        public DateTime created_at;

        public string branches;

        public GitLabCommit()
        {
        }

        public GitLabCommit(string id, string short_id, long project_id, string title, string message, string author_name, DateTime created_at, string branches)
        {
            this.id = id;
            this.short_id = short_id;
            this.project_id = project_id;
            this.title = title;
            this.message = message;
            this.author_name = author_name;
            this.created_at = created_at;
            this.branches = branches;
        }
    }
}
