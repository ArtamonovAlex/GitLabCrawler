using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GitLabCrawler
{
    public class RequestService
    {
        private string privateToken;

        public RequestService(string privateToken)
        {
            this.privateToken = privateToken;
        }

        public GitLabProject GetProject(long project_id)
        {
            try
            {
                WebRequest req = WebRequest.Create("https://gitlab.com/api/v4/projects/" + project_id + "?private_token=" + privateToken);
                StreamReader stream = new StreamReader(req.GetResponse().GetResponseStream());
                string response = stream.ReadToEnd();
                stream.Close();
                GitLabProject project = JsonConvert.DeserializeObject<GitLabProject>(response);
                return project;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public GitLabUser GetUser(long user_id)
        {
            try
            {
                WebRequest req = WebRequest.Create("https://gitlab.com/api/v4/users/" + user_id + "?private_token=" + privateToken);
                StreamReader stream = new StreamReader(req.GetResponse().GetResponseStream());
                string response = stream.ReadToEnd();
                stream.Close();
                GitLabUser creator = JsonConvert.DeserializeObject<GitLabUser>(response);
                return creator;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<GitLabCommit> GetProjectCommits(long project_id)
        {
            try
            {
                List<GitLabCommit> totalCommits = new List<GitLabCommit>();
                List<GitLabCommit> commits;
                int page = 1;
                int pages = 1;
                do
                {
                    WebRequest req = WebRequest.Create("https://gitlab.com/api/v4/projects/" + project_id + "/repository/commits" + "?private_token=" + privateToken + "&per_page=100&page=" + page);
                    WebResponse resp = req.GetResponse();
                    pages = int.Parse(resp.Headers["X-Total-Pages"]); /*if you need ALL commits*/
                    StreamReader stream = new StreamReader(resp.GetResponseStream());
                    string response = stream.ReadToEnd();
                    stream.Close();
                    commits = JsonConvert.DeserializeObject<List<GitLabCommit>>(response);
                    totalCommits.AddRange(commits);
                    page++;
                } while (page <= pages);

                foreach(GitLabCommit commit in totalCommits)
                {
                    //commit.branches = GetCommitBranches(project_id, commit.short_id);
                    commit.project_id = project_id;
                }

                return totalCommits;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private string GetCommitBranches(long project_id, string commit_id)
        {
            try
            {
                WebRequest req = WebRequest.Create("https://gitlab.com/api/v4/projects/" + project_id + "/repository/commits/"+ commit_id + "/refs?private_token=" + privateToken + "&per_page=100");
                StreamReader stream = new StreamReader(req.GetResponse().GetResponseStream());
                string response = stream.ReadToEnd();
                stream.Close();
                List<dynamic> refs = JsonConvert.DeserializeObject<List<dynamic>>(response);
                string branches = "";
                foreach(var refer in refs)
                {
                    if (refer.type == "branch") branches += refer.name + " ";
                }
                return branches;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<GitLabDiff> GetCommitDiffs(long project_id, string commit_id)
        {
            try
            {
                WebRequest req = WebRequest.Create("https://gitlab.com/api/v4/projects/" + project_id + "/repository/commits/" + commit_id + "/diff?private_token=" + privateToken + "&per_page=100");
                StreamReader stream = new StreamReader(req.GetResponse().GetResponseStream());
                string response = stream.ReadToEnd();
                stream.Close();
                List<GitLabDiff> diffs = JsonConvert.DeserializeObject<List<GitLabDiff>>(response);
                foreach (GitLabDiff diff in diffs)
                {
                    diff.commit_id = commit_id;
                }
                return diffs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
