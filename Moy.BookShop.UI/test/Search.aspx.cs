using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Index;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lucene.Net.Documents;
using Lucene.Net.Search;
using Moy.BookShop.Common;

namespace Moy.BookShop.UI.test
{
    public partial class Search : System.Web.UI.Page
    {
        protected string indexPath = ConfigurationManager.AppSettings["indexPath"];
        protected List<SearchResult> list = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["btnSearch"]))
            {
                SearchInfo();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["btnCreate"]))
            {
                CreateIndex();
            }
        }

        private void SearchInfo()
        {
            string kw = Request["content"];
            kw = kw.ToLower();
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NoLockFactory());
            IndexReader reader = IndexReader.Open(directory, true);
            IndexSearcher searcher = new IndexSearcher(reader);

            PhraseQuery query = new PhraseQuery();
            foreach (var word in CommonHelper.SplitWord(kw))
            {
                query.Add(new Term("content", word));
            }
           
            query.SetSlop(100);
            
            TopScoreDocCollector collector = TopScoreDocCollector.create(1000, true);
            searcher.Search(query, null, collector);
            ScoreDoc[] docs = collector.TopDocs(0, collector.GetTotalHits()).scoreDocs;
            //TODO 实现分页功能
            list = new List<SearchResult>();
            for (int i = 0; i < docs.Length; i++)
            {                
                int docId = docs[i].doc;
                Document doc = searcher.Doc(docId);
                SearchResult res = new SearchResult
                {
                    Msg = CommonHelper.HighLight(kw, doc.Get("content")),
                    Title = doc.Get("title"),
                    Url = "/book/BookDetail.aspx?bid=" + doc.Get("id")
                };
                list.Add(res);
            }                             
        }

        private void CreateIndex()
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

            var books = BLL.BookManage.GetAll();
            if (books != null && books.Count() > 0)
            {
                foreach (var book in books)
                {
                    Document document = new Document();

                    document.Add(new Field("id", book.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                    document.Add(new Field("title", book.Title, Field.Store.YES, Field.Index.NOT_ANALYZED,
                        Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));
                    document.Add(new Field("content", book.ContentDescription, Field.Store.YES, Field.Index.ANALYZED,
                        Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));
                    writer.AddDocument(document);
                }
            }

            writer.Close();
            directory.Close();
            Response.Write("ok");
        }

    }

    public class SearchResult
    {
        public string Title { get; set; }
        public string Msg { get; set; }
        public string Url { get; set; }
    }
}