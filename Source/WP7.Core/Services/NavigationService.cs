﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Win8.Core.Helpers;

namespace Win8.Core.Services
{
    /// <summary>
    /// Default implementation of the navigation service for Windows Phone 7.
    /// </summary>
    public class NavigationService : INavigationService
    {
        private PhoneApplicationFrame mainFrame;

        public event NavigatingCancelEventHandler Navigating;

        public void NavigateTo(Uri pageUri)
        {
            if (EnsureMainFrame())
            {
                TaskHelper.SafeShow(() => mainFrame.Navigate(pageUri));
            }
        }

        public void GoBack()
        {
            if (EnsureMainFrame() && mainFrame.CanGoBack)
            {
                TaskHelper.SafeShow(() => mainFrame.GoBack());
            }
        }

        public bool RemoveBackEntry()
        {
            if (EnsureMainFrame() && mainFrame.BackStack.Any())
            {
                mainFrame.RemoveBackEntry();
                return true;
            }
            return false;
        }

        private bool EnsureMainFrame()
        {
            if (mainFrame != null)
            {
                return true;
            }

            mainFrame = Application.Current.RootVisual as PhoneApplicationFrame;

            if (mainFrame != null)
            {
                // Could be null if the app runs inside a design tool
                mainFrame.Navigating += (s, e) =>
                {
                    if (Navigating != null)
                    {
                        Navigating(s, e);
                    }
                };

                return true;
            }

            return false;
        }
    }
}