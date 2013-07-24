using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WorkoutPlanner.ViewModels;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;
using System.Diagnostics;
using HtmlAgilityPack;

namespace WorkoutPlanner
{
    public partial class ExerciseExplanation : PhoneApplicationPage
    {
        private ExerciseType active;
        public ExerciseExplanation()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string selectedIndex = "";
            if (NavigationContext.QueryString.TryGetValue("exerciseToShow", out selectedIndex))
            {
                active = ExerciseType.FromName(selectedIndex);
                Label.Text = active.Name;
                SynchronizationContext ctx = SynchronizationContext.Current;

                TheWebView.Navigating += TheWebView_Navigating;
                TheWebView.Navigated += TheWebView_Navigated;
                //TheWebView.Navigate(new Uri(active.Url));
                DownloadHTMLAsync(ctx, active.Url, TheWebView);
                
            }
            else
            {
                return;
            }
        }

        private Boolean loadedPage = false;
        void TheWebView_Navigated(object sender, NavigationEventArgs e)
        {
            loadedPage = true;
        }

        void TheWebView_Navigating(object sender, NavigatingEventArgs e)
        {
            if (!loadedPage)
            {
                return;
            }
           
            e.Cancel = true;
        }


        public static async Task DownloadHTMLAsync(SynchronizationContext sc, string url, WebBrowser toSet)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            string content = await response.Content.ReadAsStringAsync();
            sc.Post(rawState =>
            {

                string strToRemove = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01//EN\" \"http://www.w3.org/TR/html4/strict.dtd\">";
                string parsedState = ((string)rawState).Replace(strToRemove, "").Trim();

                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(parsedState);

                HtmlNode collection = document.GetElementbyId("fw-mainColumn");


                toSet.NavigateToString(collection.InnerHtml);

            }, content);
        }    
    }
}