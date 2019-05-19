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
        static int Main(string[] args)
        {
            string privateToken = "oQykY3_B5miv4xCtyD94";
            if (args.Length > 2 || args.Length < 1) {
                Console.WriteLine("Wrong amount of attributes. Usage GitLabCrawler.exe projectId 20/all");
                return -1;
            }
            if (!long.TryParse(args[0], out long project_id)) {
                Console.WriteLine("Can't parse project id.");
                return -2;
            }
            if (args[1] != "20" && args[1] != "all") {
                Console.WriteLine("Can't parse number of commits");
                return -2;
            }

            RequestService reqService = new RequestService(privateToken);
            DatabaseService dbService = new DatabaseService();

            Console.WriteLine("Getting info about project " + project_id + "...");
            GitLabProject project = reqService.GetProject(project_id);
            if (project == null)
            {
                Console.WriteLine("Project not found");
                return -3;
            }
            Console.WriteLine("Got info about project №" + project_id);



            Console.WriteLine("Getting info about project creator...");
            GitLabUser creator = reqService.GetUser(project.creator_id);
            if (creator != null)
            {
                Console.WriteLine("Got info about creator");
            }
            else
            {
                Console.WriteLine("Can't get user's info");
            }
            Console.WriteLine("Adding user to database...");
            dbService.InsertGitlabUser(creator);
            Console.WriteLine("Adding project to database...");
            dbService.InsertGitlabProject(project);

            Console.WriteLine("Getting info about commits...");
            List<GitLabCommit> commits;
            if (args[1] == "20")
            {
                commits = reqService.Get20ProjectCommits(project_id);
            } else { 
                commits = reqService.GetAllProjectCommits(project_id);
            }
            if (commits != null)
            {
                Console.WriteLine($"Total commits in list: {commits.Count}");

            }
            else
            {
                Console.WriteLine("Can't get list of commits");
            }

            Console.WriteLine("Getting diffs...");
            Console.WriteLine("Adding commits to database...");
            long diffCounter = 0;
            foreach (GitLabCommit commit in commits)
            {
                dbService.InsertGitlabCommit(commit);
                List<GitLabDiff> diffs = reqService.GetCommitDiffs(project_id, commit.id);
                foreach(GitLabDiff diff in diffs)
                {
                    dbService.InsertGitlabDiff(diff);
                }
                diffCounter += diffs.Count;
            }
            Console.WriteLine($"Got {diffCounter} diffs for {commits.Count} commits");
            Console.WriteLine("All information added into database.");
            return 0;
        }
    }
}
