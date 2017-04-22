using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Moy.BookShop.BLL;
using Moy.BookShop.Model;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Index;
using Lucene.Net.Store;
using System.IO;
using Lucene.Net.Search;
using Moy.BookShop.Common;
using Lucene.Net.Documents;
using System.Configuration;

namespace Moy.BookShop.UI.master
{
    public partial class Common : System.Web.UI.MasterPage
    {
        protected IEnumerable<Category> categories = null;
        protected string indexPath = ConfigurationManager.AppSettings["indexPath"];
        protected List<SearchResult> list = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                categories = CategoryManage.GetAll();
            }

            if (!string.IsNullOrEmpty(Request.QueryString["btnSearch"]))
            {
                SearchInfo();
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
            SearchDetailManage.Add(kw);
        }

    }

    public class SearchResult
    {
        public string Title { get; set; }
        public string Msg { get; set; }
        public string Url { get; set; }
    }
}