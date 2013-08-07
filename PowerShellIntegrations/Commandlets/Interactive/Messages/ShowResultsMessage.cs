﻿using System;
using System.Text;
using System.Web;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Jobs.AsyncUI;
using Sitecore.Resources.Media;
using Sitecore.Shell.Framework;
using Sitecore.Text;
using Sitecore.Web.UI.Sheer;

namespace Cognifide.PowerShell.PowerShellIntegrations.Commandlets.Interactive.Messages
{
    [Serializable]
    public class ShowResultsMessage : BasePipelineMessage, IMessage
    {

        public string Html { get; private set; }
        public string Width { get; private set; }
        public string Height { get; private set; }
        
        public ShowResultsMessage(string html, string width, string height)
        {
            Html = html;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Shows a confirmation dialog.
        /// 
        /// </summary>
        protected override void ShowUI()
        {
            string resultSig = Guid.NewGuid().ToString();
            HttpContext.Current.Session[resultSig] = Html;
            UrlString urlString = new UrlString(UIUtil.GetUri("control:PowerShellResultViewerText"));
            urlString.Add("sid", resultSig);
            var response = SheerResponse.ShowModalDialog(urlString.ToString(), Width, Height);
        }
    }
}