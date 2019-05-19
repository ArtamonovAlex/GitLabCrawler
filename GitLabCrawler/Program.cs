using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GitLabCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            string privateToken = "oQykY3_B5miv4xCtyD94";
            long project_id;
            string input = Console.ReadLine();
            project_id = long.Parse(input);
            

            RequestService reqService = new RequestService(privateToken);

            Console.WriteLine("Getting info about project " + project_id + "...\n");
            GitLabProject project = reqService.GetProject(project_id);
            if (project == null)
            {
                Console.WriteLine("Press any key to finish program");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("Got info about project №" + project_id);

            Console.WriteLine("Getting info about project creator...\n");
            GitLabUser creator = reqService.GetUser(project.creator_id);
            if (creator != null)
            {
                Console.WriteLine("Got info about creator");
            }
            else
            {
                Console.WriteLine("Can't get user's info\n");
            }

            Console.WriteLine("Getting info about commits...\n");
            List<GitLabCommit> commits = reqService.GetProjectCommits(project_id);
            if (commits != null)
            {
                Console.WriteLine($"Total commits in list: {commits.Count}");

            }
            else
            {
                Console.WriteLine("Can't get list of commits");
            }

            Console.WriteLine("Getting diffs...");
            long diffCounter = 0;
            foreach (GitLabCommit commit in commits)
            {
                List<GitLabDiff> diffs = reqService.GetCommitDiffs(project_id, commit.short_id);
                diffCounter += diffs.Count;
            }
            Console.WriteLine($"Got {diffCounter} diffs for {commits.Count} commits");
            Console.ReadLine();
            return;
        }
    }
}
