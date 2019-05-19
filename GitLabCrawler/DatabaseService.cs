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
        private string connectionString = "Server=localhost;Port=9999;User Id=s242274;Password=bld868;Database=studs;";
        private NpgsqlConnection connection;

        public DatabaseService()
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
                Console.WriteLine(ex.ErrorCode);
            }
            connection.Close();
            return result;
        }

        public int InsertGitlabUser(GitLabUser user)
        {
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand($"insert into \"user\" values ({user.id},'{user.name}','{user.username}','{user.web_url}','{user.avatar_url}')", connection);
            int result = command.ExecuteNonQuery();
            connection.Close();
            return result;
        }
    }
}
