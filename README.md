# GitLabCrawler
It is an app to get information about the project uploaded on GitLab and put it into the database.
## Getting started
* Clone repo and build the app
* In the directory of the executable file create files ***auth-token.txt*** and ***connection-data.txt***
### Auth-token.txt
You need to copy there your GitLab API personal access token.
How to get it you can read [here](https://docs.gitlab.com/ee/user/profile/personal_access_tokens.html).
### Connection-data.txt
This file keeps information about the connection to database in the folowing format (without "*"):
```
Server=*your server*;Port=*database port*;User Id=*user of the database*;Password=*his password*;Database=*name of the database*;
```
### Database
Your database must have this entities
```
create table IF NOT EXISTS "user"(
	id int primary key ,
	name text not null,
	username text not null,
	web_url text not null,
	avatar_url text
);

create table IF NOT EXISTS "project"(
	id int primary key,
	name text not null,
	description text,
	user_id int REFERENCES "user" (id),
	created_at text not null,
	web_url text not null,
	avatar_url text
);

create table IF NOT EXISTS "commit"(
	id text primary key,
	title text,
	created_at text not null, 
	message text,
	project_id  int not null REFERENCES "project" (id),
	author_name text not null,
	branches text not null
);

create table IF NOT EXISTS "Diff"(
	id serial primary key,
	old_path text not null,
	new_path text not null,
	new_file bool not null,
	renamed_file bool not null,
	deleted_file bool not null,
	diff text not null,
	commit_id text not null REFERENCES "commit" (id)
);
```
## Usage
Launch requires two attributes:
* Id of the project you want
* One of the values in {20, all} where "20" will get only last 20 commits, while "all" puts in the database all commits
### For example:
```
GitLabCrawler.exe 13083 20
```
### Or:
```
GitLabCrawler.exe 13083 all
```
