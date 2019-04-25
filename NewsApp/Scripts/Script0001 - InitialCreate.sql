create table authors
(
id uniqueidentifier not null primary key,
FullName nvarchar(50) not null
)
go
create table articles
(
id uniqueidentifier not null primary key,
newsTitle nvarchar(50) not null,
textNews nvarchar(max) not null,
authorId uniqueidentifier not null,
CONSTRAINT [FK_Articless_ToAuthors] FOREIGN KEY ([authorId]) REFERENCES [dbo].[authors] ([Id])
)
go
create table users
(
id uniqueidentifier not null primary key,
login nvarchar(50) not null,
password nvarchar(50) not null
)
go
create table comments
(
id uniqueidentifier not null primary key,
articleId uniqueidentifier not null,
userId uniqueidentifier not null,
textComment NVARCHAR(MAX) NOT NULL, 
 CONSTRAINT [FK_Comments_ToUser] FOREIGN KEY ([userId]) REFERENCES [dbo].[users] ([Id]),
 CONSTRAINT [FK_Comments_ToArticle] FOREIGN KEY ([articleId]) REFERENCES [dbo].[articles] ([Id])
)