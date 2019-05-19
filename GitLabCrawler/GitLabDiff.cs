using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitLabCrawler
{
    public class GitLabDiff
    {
        public string diff;

        public string old_path;

        public string new_path;

        public bool new_file;

        public bool renamed_file;

        public bool deleted_file;

        public string commit_id;

        public GitLabDiff()
        {
        }

        public GitLabDiff(string diff, string old_path, string new_path, bool new_file, bool renamed_file, bool deleted_file, string commit_id)
        {
            this.diff = diff;
            this.old_path = old_path;
            this.new_path = new_path;
            this.new_file = new_file;
            this.renamed_file = renamed_file;
            this.deleted_file = deleted_file;
            this.commit_id = commit_id;
        }
    }
}
