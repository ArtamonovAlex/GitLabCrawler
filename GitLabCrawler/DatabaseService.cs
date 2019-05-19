using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitLabCrawler
{
    public class DatabaseService
    {
        private NpgsqlConnection connection;

        public DatabaseService(string connectionString)
        {
            connection = new NpgsqlConnection(connectionString);
        }

        public int InsertGitlabProject(GitLabProject project)
        {
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand($"insert into project values ({project.id},'{project.name}','{project.description}', {project.creator_id}, '{project.created_at}','{project.web_url}','{project.avatar_url}')", connection);
            int result = 0;
            try
            {
                result = command.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                if (ex.Message.Substring(0,5) == "23505")
                {

                } else
                {
                    Console.WriteLine(ex.Message);
                }
            }
            connection.Close();
            return result;
        }

        public int InsertGitlabUser(GitLabUser user)
        {
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand($"insert into \"user\" values ({user.id},'{user.name}','{user.username}','{user.web_url}','{user.avatar_url}')", connection);
            int result = 0;
            try
            {
                result = command.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                if (ex.Message.Substring(0, 5) == "23505")
                {

                }
                else
                {
                    Console.WriteLine(ex.Message);
                }
            }
            connection.Close();
            return result;
        }

        public int InsertGitlabCommit(GitLabCommit commit)
        {
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand($"insert into commit values ('{commit.id}','{commit.title.Replace("'", "''")}','{commit.created_at}','{commit.message.Replace("'", "''")}',{commit.project_id}, '{commit.author_name}', '{commit.branches}')", connection);
            int result = 0;
            try
            {
                result = command.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                if (ex.Message.Substring(0, 5) == "23505")
                {

                }
                else
                {
                    Console.WriteLine(ex.Message);
                }
            }
            connection.Close();
            return result;
        }

        public int InsertGitlabDiff(GitLabDiff diff)
        {
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand($"insert into diff(old_path, new_path, new_file, renamed_file, deleted_file, diff, commit_id)" +
                $" values('{diff.old_path}','{diff.new_path}',{diff.new_file},{diff.renamed_file},{diff.deleted_file}, '{diff.diff.Replace("'", "''")}', '{diff.commit_id}')", connection);
            int result = 0;
            try
            {
                result = command.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                if (ex.Message.Substring(0, 5) == "23505")
                {

                }
                else
                {
                    Console.WriteLine(ex.Message);
                }
            }
            connection.Close();
            return result;
        }
    }
}
