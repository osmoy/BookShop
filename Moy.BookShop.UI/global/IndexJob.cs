using Moy.BookShop.BLL;
using Quartz;

namespace Moy.BookShop.UI.global
{
    public class IndexJob:IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            SearchStatisticManage.Delete();
            SearchStatisticManage.Add();
        }
    }
}