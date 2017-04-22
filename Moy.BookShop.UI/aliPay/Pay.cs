using Moy.BookShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moy.BookShop.UI.aliPay
{
    public class Pay
    {
        public string partner { get; set; }
        public string retutrn_url { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public string out_trade_no { get; set; }
        public decimal total_fee { get; set; }
        public string seller_email { get; set; }
        public string key { get; set; }
        public string payGateUrl { get; set; }
        public string sign { get; set; }

        public Pay(string subject, string body, string out_trade_no, decimal total_fee)
        {
            partner = CommonHelper.GetValue("partner");
            retutrn_url = CommonHelper.GetValue("retutrn_url");
            seller_email = CommonHelper.GetValue("seller_email");
            key = CommonHelper.GetValue("key");
            payGateUrl = CommonHelper.GetValue("payGateUrl");
            this.subject = subject;
            this.body = body;
            this.out_trade_no = out_trade_no;
            this.total_fee = total_fee;
            //sign：数字签名 按顺序连接
            this.sign = CommonHelper.GetMD5((total_fee + partner + out_trade_no + subject + key)).ToLower();
        }

        public string PayUrl()
        {
            string url = string.Format(@"{0}?partner={1}&retutrn_url={2}&subject={3}&body={4}&out_trade_no={5}
&total_fee={6}&seller_email={7}&sign={8}", payGateUrl, partner, retutrn_url, subject, body, out_trade_no, total_fee, seller_email, sign);
            return url;
        }


    }
}