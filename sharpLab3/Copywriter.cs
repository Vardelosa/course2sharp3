using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace sharpLab3
{
    [Serializable]
    class Copywriter : Person
    {
        private int rating;
        public List<Article> ListOfArticles { get; set; } = new List<Article>();
        public string Nickname { get; set; }
        public Level Level { get; set; }
        public int Rating
        {
            get
            {

                return rating;
            }
            set
            {
                if (value <= 0 | value > 5)
                {
                    throw new ArgumentOutOfRangeException();
                }
                rating = value;
            }

        }

        public Copywriter(Person person, string nickname, Level level, int rating)
            : base(person.Name, person.Surname, person.Data)
        {
            Nickname = nickname;
            Level = level;
            Rating = rating;
        }
        public Copywriter()
        {
            Name = "Unknown";
            Surname = "Unknown";
            Data = new DateTime();
            Nickname = "Unknown";
            Level = 0;
            Rating = 1;
        }
        public Copywriter DeepCopy()
        {
            using (var ms = new MemoryStream())
            {
                if (GetType().IsSerializable)
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(ms, this);
                    ms.Position = 0;
                    return (Copywriter)formatter.Deserialize(ms);
                }
                return null;
            }
        }
        
        public bool Save(string filename)
        {
            try
            {
                using (FileStream fs = File.Create(filename+".dat"))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, this);
                    return true;
                }
            }
            catch(Exception)
            {
                return false;
            }
}
        public bool Load(string filename)
        {
            try
            {
                using (FileStream fs = new FileStream(filename + ".dat", FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    Copywriter c1 = (Copywriter)formatter.Deserialize(fs);
                    AuthorInfo = c1.AuthorInfo;
                    Nickname = c1.Nickname;
                    Level = c1.Level;
                    Rating = c1.Rating;
                    ListOfArticles = c1.ListOfArticles;
                    return true;
                }
            }
            catch(Exception)
            {
                return false;
            }
        }
        public static bool Save(string filename, Copywriter SerializableObject)
        {
            try
            {
                using (FileStream fs = File.Create(filename + ".dat"))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, SerializableObject);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool Load(string filename, Copywriter SerializableObject)
        {
            try
            {
                using(FileStream fs = File.Open(filename + ".dat", FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    Copywriter c1 = (Copywriter)formatter.Deserialize(fs);
                    SerializableObject.AuthorInfo = c1.AuthorInfo;
                    SerializableObject.Nickname = c1.Nickname;
                    SerializableObject.Level = c1.Level;
                    SerializableObject.Rating = c1.Rating;
                    SerializableObject.ListOfArticles = c1.ListOfArticles;                  
                    return true;
                }
            }
            catch(Exception)
            {
                return false;
            }
        }
        public bool AddFromConsole()
        {
            Console.WriteLine("\nВведите данные в виде: *название_статьи/кол-во символов/дата*\n" +
                "ВНИМАНИЕ: Дата должна быть введена в виде дд.мм.гггг");
            string info = Console.ReadLine();
            try
            {
                string[] words = info.Split('/');
                string[] date = words[2].Split('.');
                if (words.Length!=3|date.Length!=3)
                {
                    throw new Exception();
                }
                int[] results = new int[4];
                bool success = Int32.TryParse(words[1], out results[0]);
                if (success)
                {
                    for(int i = 0; i < 3; i++)
                    {
                        bool succ = Int32.TryParse(date[i], out results[1 + i]);
                        if(!succ)
                        {
                            throw new Exception();
                        }
                    }
                    Article added = new Article(words[0], results[0], new DateTime(results[3], results[2], results[1]));
                    AddArticle(added);
                }
                else
                {
                    throw new Exception();
                }
                return true;
            }
            catch(Exception)
            {
                Console.WriteLine("Follow the instructions");
                return false;
            }
            
        }
        public IEnumerator<object> GetEnumerator()
        {
            foreach (var v in ListOfArticles) { yield return v; }
        }
        public int Middle
        {
            get
            {
                int sum = 0;
                int n;
                foreach (Article p in ListOfArticles)
                {
                    sum = p.ArSymbols + sum;
                }
                n = sum / ListOfArticles.Count;
                return n;
            }
        }
        public Person AuthorInfo
        {
            get
            {
                return new Person(Name, Surname, Data);
            }
            set
            {
                this.Name = value.Name;
                this.Surname = value.Surname;
                this.Data = value.Data;
            }
        }
        public Article LastArticle
        {
            get
            {
                if (ListOfArticles.Count <= 0)
                {
                    return null;
                }
                else
                {
                    return ListOfArticles[ListOfArticles.Count - 1];
                }
            }
        }


        public void AddArticles(params Article[] articles)
        {
            for (int i = 0; i < articles.Length; i++)
            {
                ListOfArticles.Add(articles[i]);
            }
        }
        public void AddArticle(Article article)
        {
            ListOfArticles.Add(article);
        }

        public override string ToString()
        {
            string s = "Author: " + AuthorInfo + ". " + "Date: " + data + "\r\n Nickname: " + Nickname + "\r\n Level: " + Level + "\r\n Rating: " + Rating;

            s = s + " \r\nList of articles: " + "\r\n";
            foreach (Article p in ListOfArticles)
            {
                s = s + p.ToString() + "\r\n";
            }

            return s;
        }
        public new string ToShortString()
        {
            string s = "\n Nickname: " + Nickname + "\nRating: " + Rating;
            return s;
        }

    }
}
