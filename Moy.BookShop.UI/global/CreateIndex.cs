using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Index;
using Lucene.Net.Store;
using System.IO;
using Lucene.Net.Documents;
using System.Configuration;

namespace Moy.BookShop.UI.global
{
    public class CreateIndex
    {
        public static CreateIndex index = new CreateIndex();

        private static readonly string indexPath = ConfigurationManager.AppSettings["indexPath"];

        Queue<Job> queen = new Queue<Job>();

        private CreateIndex()
        {
        }

        public void Enqueue(int id, string title, string content)
        {
            Job job = new Job()
            {
                Id = id,
                Content = content,
                Title = title,
                JobType = JobType.Add
            };
            queen.Enqueue(job);
        }

        public void Enqueue(int id)
        {
            Job job = new Job()
            {
                Id = id,
                JobType = JobType.Delete
            };
            queen.Enqueue(job);
        }

        public void Start()
        {
            System.Threading.Thread th = new System.Threading.Thread(InsertIndex);
            th.IsBackground = true;
            th.Start();
        }

        private void InsertIndex()
        {
            while (true)
            {
                if (queen.Count <= 0)
                {
                    System.Threading.Thread.Sleep(5000);
                    continue;
                }
                else
                {
                    Dequeue();
                }
            }
        }

        private void Dequeue()
        {
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NativeFSLockFactory());

            bool isExist = IndexReader.IndexExists(directory);
            if (isExist)
            {
                if (IndexWriter.IsLocked(directory))
                {
                    IndexWriter.Unlock(directory);
                }
            }
            IndexWriter writer = new IndexWriter(directory, new PanGuAnalyzer(), !isExist,
                Lucene.Net.Index.IndexWriter.MaxFieldLength.UNLIMITED);

            while (queen.Count > 0)
            {
                Job job = queen.Dequeue();

                writer.DeleteDocuments(new Term("id", job.Id.ToString()));

                if (job.JobType == JobType.Delete)
                {
                    continue;
                }

                Document document = new Document();

                document.Add(new Field("id", job.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("title", job.Title, Field.Store.YES, Field.Index.NOT_ANALYZED,
                    Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));
                document.Add(new Field("content", job.Content, Field.Store.YES, Field.Index.ANALYZED,
                    Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));

                writer.AddDocument(document);

                writer.Close();

                directory.Close();
            }

        }

    }

    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public JobType JobType { get; set; }
    }

    public enum JobType
    {
        Add,
        Delete
    }
}