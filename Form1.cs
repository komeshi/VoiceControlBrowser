using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Speech.Recognition;
using System.Globalization;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;

namespace VoiceControlBrowser
{
    public partial class Form1 : Form
    {
        static CultureInfo ci = new CultureInfo("ja-jp");
        static SpeechRecognitionEngine sre = new SpeechRecognitionEngine(ci);
        public Form1()
        {
            InitializeComponent();
        }

        private void IE_Open(String searchEngine)
        {
            SHDocVw.InternetExplorer objIE = new SHDocVw.InternetExplorer(); //オブジェクトを作成
            objIE.Navigate("about:blank");          //空ページの表示
            objIE.Visible = true;                   //IEを表示


            String encodedKeyword = System.Web.HttpUtility.UrlEncode(this.textBox1.Text, System.Text.Encoding.UTF8);

            String targetUrl = searchEngine + encodedKeyword;

            objIE.Navigate(targetUrl);

            //読み込み完了まで待つ
            while (objIE.Busy || objIE.ReadyState != SHDocVw.tagREADYSTATE.READYSTATE_COMPLETE)
            {
                //無処理
                System.Windows.Forms.Application.DoEvents();
                System.Threading.Thread.Sleep(100);
            }

            var ObjHtml = (mshtml.HTMLDocument)objIE.Document;

            var bodys = ObjHtml.getElementsByTagName("body");
            foreach (mshtml.IHTMLElement element in bodys)
            {
                if (element != null)
                {
                    Debug.WriteLine(element.outerHTML);
                }
            }

            System.Threading.Thread.Sleep(5000);

            if (objIE == null)
            {
                objIE.Quit();
            }
        }

        private void Edge_Open(String searchEngine)
        {
            var edgeDriver = new EdgeDriver(EdgeDriverService.CreateDefaultService(@"C:\Users\Razor0329\Downloads\edgedriver_win64", "msedgedriver.exe"));

            String encodedKeyword = System.Web.HttpUtility.UrlEncode(this.textBox1.Text, System.Text.Encoding.UTF8);

            String targetUrl = searchEngine + encodedKeyword;

            // URL遷移
            edgeDriver.Navigate().GoToUrl(targetUrl);


            System.Threading.Thread.Sleep(5000);

            if (edgeDriver == null)
            {
                edgeDriver.Quit();
            }
        }

        private void buttonIE_Yahoo_Click(object sender, EventArgs e)
        {
            IE_Open("https://search.yahoo.co.jp/search?b=0&p=");
        }
        private void buttonIE_Google_Click(object sender, EventArgs e)
        {
            IE_Open("https://www.google.com/search?hl=ja&q=");
        }
        private void buttonEdge_Yahoo_Click(object sender, EventArgs e)
        {
            Edge_Open("https://search.yahoo.co.jp/search?b=0&p=");
        }
        private void buttonEdge_Google_Click(object sender, EventArgs e)
        {
            Edge_Open("https://www.google.com/search?hl=ja&q=");
        }

    }
}
