﻿using EverythingToolbar;
using NHotkey;
using NLog;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace CSDeskBand
{
	[ComVisible(true)]
    [Guid("9d39b79c-e03c-4757-b1b6-ecce843748f3")]
    [CSDeskBandRegistration(Name = "Everything Toolbar")]
    public class Deskband : CSDeskBandWpf
    {
        protected override UIElement UIElement => new ToolbarControl();

        public Deskband()
        {
            Options.MinHorizontalSize = new Size(18, 30);
            Options.MinVerticalSize = new Size(30, 40);
            TaskbarInfo.TaskbarEdgeChanged += OnTaskbarEdgeChanged;

            ToolbarLogger.Initialize();
			ILogger logger = ToolbarLogger.GetLogger("EverythingToolbar");
            logger.Info("EverythingToolbar started. Version: {version}, OS: {os}", Assembly.GetExecutingAssembly().GetName().Version, Environment.OSVersion);

            AppDomain.CurrentDomain.UnhandledException += (sender, args) => ToolbarLogger.GetLogger("EverythingToolbar").Error((Exception)args.ExceptionObject, "Unhandled Exception");

            SearchResultsPopup.taskbarEdge = TaskbarInfo.Edge;
        }

        private void OnTaskbarEdgeChanged(object sender, TaskbarEdgeChangedEventArgs e)
        {
            SearchResultsPopup.taskbarEdge = e.Edge;
        }
    }
}
