using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using DbUp;
using System.Reflection;


namespace NewsApp
{
    enum ListAdmins //список авторов которые при входе могут писать новостную статью
    {
        admin = 0,

    }
    class Program
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["appConnectionNews"].ConnectionString;
        //Создать приложение для написания новостей и комментариев к ним. Используйте Dapper. Подключите миграции.
        static void Main(string[] args)
        {
            /*      CheckMigrations();
                  User user = new User
                  {
                      Login = "den",
                      Password = "123"
                  };
                 using (var connection = new SqlConnection(_connectionString))
                 {
                     connection.Execute("insert into users values(@Id, @Login,@Password)", user);
                 }*/
            Console.WriteLine("1) Вход(логин и пароль для написания статьи - (admin 123))");
            Console.WriteLine("2) Регистрация");
            int choiceRegistretion = int.Parse(Console.ReadLine());

                 if(choiceRegistretion == 1) {

            Console.Write("Login: ");
            string loginUser = Console.ReadLine();
            Console.Write("Password: ");
            string passwordUser = Console.ReadLine();
            User user = new User
            {
                Login = loginUser,
                Password = passwordUser
            };
            using (var connection = new SqlConnection(_connectionString))
            {
                List<User> users = new List<User>();
                ShowUsers(connection, users);
                for (int i = 0; i < users.Count; i++)
                {
                    foreach (string str in Enum.GetNames(typeof(ListAdmins)))
                    {

                        if (users[i].Login == loginUser && users[i].Password == passwordUser && loginUser == str)
                        {
                            Console.Clear();
                            Console.WriteLine("Вход выполнен успешно!(Вход с дополнительными правами)");
                            Console.ReadLine();
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("1) Написать статью");
                                Console.WriteLine("2) Просмотрть статьи");
                                int choice = int.Parse(Console.ReadLine());
                                if (choice == 1)
                                {
                                    Author author = new Author { FullName = loginUser };
                                    List<Author> authors = new List<Author>();
                                    ShowAuthors(connection, authors);
                                    int countAuthorsInTable = 0;
                                    Guid authorId = Guid.NewGuid();
                                    for (int j = 0; j < authors.Count; j++)
                                    {
                                        if (authors[j].FullName == loginUser)
                                        {
                                            authorId = authors[j].Id;
                                            countAuthorsInTable++;
                                        }

                                    }
                                    if (countAuthorsInTable == 0)
                                    {
                                        connection.Execute("insert into authors values(@Id, @FullName)", author);
                                        authorId = author.Id;
                                    }

                                    Console.WriteLine("Введите название статьи: ");
                                    string newsTitle = Console.ReadLine();

                                    Console.WriteLine("Введите статью: ");
                                    string textNews = Console.ReadLine();

                                    Article article = new Article
                                    {
                                        NewsTitle = newsTitle,
                                        TextNews = textNews,
                                        AuthorId = authorId
                                    };

                                    connection.Execute("insert into articles values(@Id, @NewsTitle,@TextNews,@AuthorId)", article);
                                    Console.Clear();
                                    Console.WriteLine("Статья выложена!");
                                    Console.ReadLine();


                                }
                                else if (choice == 2)
                                {
                                    Console.Clear();
                                    List<Article> articles = new List<Article>();
                                    ShowArticles(connection, articles);
                                    for (int j = 0; j < articles.Count; j++)
                                    {
                                        Console.WriteLine(j + 1 + ") " + articles[j].NewsTitle);

                                    }
                                    Console.WriteLine("Выберите статью для просмотра:");
                                    int choiceArticle = int.Parse(Console.ReadLine());
                                    for (int j = 0; j < articles.Count; j++)
                                    {
                                        if (choiceArticle - 1 == j)
                                        {
                                            Console.WriteLine("Название статьи: " + articles[j].NewsTitle);
                                            Console.WriteLine("Статья: ");
                                            Console.WriteLine(articles[j].TextNews);
                                            List<Author> authors = new List<Author>();
                                            ShowAuthors(connection, authors);
                                            string authorName = "...";
                                            for (int k = 0; k < authors.Count; k++)
                                            {
                                                if (authors[k].Id == articles[j].AuthorId)
                                                {
                                                    authorName = authors[k].FullName;
                                                }
                                            }
                                            Console.WriteLine("Автор: " + authorName);

                                            Console.WriteLine("Ваш комментарий: ");
                                            string textComment = Console.ReadLine();

                                            Comment comment = new Comment
                                            {
                                                TextComment = textComment,
                                                ArticleId = articles[j].Id,
                                                UserId = users[i].Id
                                            };

                                            connection.Execute("insert into comments values(@Id, @ArticleId,@UserId,@TextComment)", comment);
                                            Console.Clear();
                                            Console.WriteLine("Спасибо за комментарий!");
                                            Console.ReadLine();
                                            Console.Clear();
                                            Console.WriteLine("Посмотреть все комментарии?");
                                            Console.WriteLine("1 Да");
                                            Console.WriteLine("2 Нет");
                                            int yesOrNo = int.Parse(Console.ReadLine());
                                            if (yesOrNo == 1)
                                            {
                                                List<Comment> commentaries = new List<Comment>();
                                                ShowComments(connection, commentaries);
                                                for (int c = 0; c < commentaries.Count; c++)
                                                {
                                                    if (commentaries[c].ArticleId == articles[j].Id)
                                                    {
                                                        Console.WriteLine("Комментарий:");
                                                        Console.WriteLine(commentaries[c].TextComment);

                                                    }
                                                }
                                            }

                                        }
                                    }
                                    Console.ReadLine();
                                }
                            }
                        }
                        else if (users[i].Login == loginUser && users[i].Password == passwordUser)
                        {
                            while (true) { 
                                Console.Clear();
                            Console.WriteLine("Вход выполнен успешно!");
                            Console.ReadLine();
                            Console.WriteLine("1) Просмотрть статьи");
                            int choice = int.Parse(Console.ReadLine());
                            if (choice == 1)
                            {
                                Console.Clear();
                                List<Article> articles = new List<Article>();
                                ShowArticles(connection, articles);
                                for (int j = 0; j < articles.Count; j++)
                                {
                                    Console.WriteLine(j + 1 + ") " + articles[j].NewsTitle);

                                }
                                Console.WriteLine("Выберите статью для просмотра:");
                                int choiceArticle = int.Parse(Console.ReadLine());
                                for (int j = 0; j < articles.Count; j++)
                                {
                                    if (choiceArticle - 1 == j)
                                    {
                                        Console.WriteLine("Название статьи: " + articles[j].NewsTitle);
                                        Console.WriteLine("Статья: ");
                                        Console.WriteLine(articles[j].TextNews);
                                        List<Author> authors = new List<Author>();
                                        ShowAuthors(connection, authors);
                                        string authorName = "...";
                                        for (int k = 0; k < authors.Count; k++)
                                        {
                                            if (authors[k].Id == articles[j].AuthorId)
                                            {
                                                authorName = authors[k].FullName;
                                            }
                                        }
                                        Console.WriteLine("Автор: " + authorName);

                                        Console.WriteLine("Ваш комментарий: ");
                                        string textComment = Console.ReadLine();

                                        Comment comment = new Comment
                                        {
                                            TextComment = textComment,
                                            ArticleId = articles[j].Id,
                                            UserId = users[i].Id
                                        };

                                        connection.Execute("insert into comments values(@Id, @ArticleId,@UserId,@TextComment)", comment);
                                        Console.Clear();
                                        Console.WriteLine("Спасибо за комментарий!");
                                        Console.ReadLine();
                                        Console.Clear();
                                        Console.WriteLine("Посмотреть все комментарии?");
                                        Console.WriteLine("1 Да");
                                        Console.WriteLine("2 Нет");
                                        int yesOrNo = int.Parse(Console.ReadLine());
                                        if (yesOrNo == 1)
                                        {
                                            List<Comment> commentaries = new List<Comment>();
                                            ShowComments(connection, commentaries);
                                            for (int c = 0; c < commentaries.Count; c++)
                                            {
                                                if (commentaries[c].ArticleId == articles[j].Id)
                                                {
                                                    Console.WriteLine("Комментарий:");
                                                    Console.WriteLine(commentaries[c].TextComment);

                                                }
                                            }
                                        }

                                    }
                                }
                                Console.ReadLine();
                            }
                        }
                        }
                    }
                }
            }


            }
                 else if(choiceRegistretion == 2)
            {
                Console.WriteLine("Введите login: ");
                string login = Console.ReadLine();
                Console.WriteLine("Введите password: ");
                string password = Console.ReadLine();

                User user = new User
                {
                    Login = login,
                    Password = password
                };
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Execute("insert into users values(@Id, @Login,@Password)", user);
                    Console.WriteLine("Вы зарегестрированы!");
                    Console.ReadLine();
                }
            }

        }

        private static void ShowUsers(SqlConnection connection, List<User> users)
        {
            string sql = "select* from users";
            var sqlDataReader = connection.ExecuteReader(sql);
            while (sqlDataReader.Read())
            {
                Guid id = (Guid)sqlDataReader["Id"];
                string login = sqlDataReader["Login"].ToString();
                string password = sqlDataReader["Password"].ToString();

                users.Add(new User
                {
                    Id = id,
                    Login = login,
                    Password = password
                });
            }
            sqlDataReader.Close();

        }
        private static void ShowArticles(SqlConnection connection, List<Article> articles)
        {
            string sql = "select* from articles";
            var sqlDataReader = connection.ExecuteReader(sql);
            while (sqlDataReader.Read())
            {
                Guid id = (Guid)sqlDataReader["Id"];
                string newsTitle = sqlDataReader["NewsTitle"].ToString();
                string textNews = sqlDataReader["TextNews"].ToString();
                Guid authorId = (Guid)sqlDataReader["AuthorId"];
                articles.Add(new Article
                {
                    Id = id,
                    NewsTitle = newsTitle,
                    TextNews = textNews,
                    AuthorId = authorId
                });
            }
            sqlDataReader.Close();
        }
        private static void ShowComments(SqlConnection connection, List<Comment> comments)
        {
            string sql = "select* from comments";
            var sqlDataReader = connection.ExecuteReader(sql);
            while (sqlDataReader.Read())
            {
                Guid id = (Guid)sqlDataReader["Id"];
                string textComment = sqlDataReader["TextComment"].ToString();
                Guid articleId = (Guid)sqlDataReader["ArticleId"];
                Guid userId = (Guid)sqlDataReader["UserId"];
                comments.Add(new Comment
                {
                    Id = id,
                    ArticleId = articleId,
                    UserId = userId,
                    TextComment = textComment

                });
            }
            sqlDataReader.Close();
        }


        private static void ShowAuthors(SqlConnection connection, List<Author> authors)
        {
            string sql = "select* from authors";
            var sqlDataReader = connection.ExecuteReader(sql);
            while (sqlDataReader.Read())
            {
                Guid id = (Guid)sqlDataReader["Id"];
                string fullName = sqlDataReader["FullName"].ToString();

                authors.Add(new Author
                {
                    Id = id,
                    FullName = fullName
                });
            }
            sqlDataReader.Close();

        }

        private static void CheckMigrations()
        {
            EnsureDatabase.For.SqlDatabase(_connectionString);

            var upgrader = DeployChanges.To
           .SqlDatabase(_connectionString)
           .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
           .LogToConsole()
           .Build();

            var result = upgrader.PerformUpgrade();
            if (!result.Successful) throw new Exception("Ошибка соединения");
        }
    }
}
