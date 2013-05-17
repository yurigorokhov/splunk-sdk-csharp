﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Splunk;

namespace UnitTests
{
    [TestClass]
    public class NamespaceTest : TestHelper
    {
        /// <summary>
        /// Tests the syntax of the path returned by Service.FullPath with different namespaces
        /// </summary>
        [TestMethod]
        public void TestStaticNameSpace()
        {
            Service service = Connect();

            Args splunkNameSpace = new Args();

            splunkNameSpace.Add("app", "search");
            Assert.AreEqual("/servicesNS/-/search/", service.Fullpath("", splunkNameSpace),
                            "Expected the path URL to be /servicesNS/-/search/");

            splunkNameSpace.Clear();
            splunkNameSpace.Add("owner", "Bob");
            Assert.AreEqual("/servicesNS/Bob/-/", service.Fullpath("", splunkNameSpace),
                            "Expected path URL to be /servicesNS/Bob/-/");

            splunkNameSpace.Clear();
            splunkNameSpace.Add("sharing", "app");
            Assert.AreEqual("/servicesNS/nobody/-/", service.Fullpath("", splunkNameSpace),
                            "Expected path URL to be /servicesNS/nobody/-/");

            splunkNameSpace.Clear();
            splunkNameSpace.Add("sharing", "system");
            Assert.AreEqual("/servicesNS/nobody/system/", service.Fullpath("", splunkNameSpace),
                            "Expected path URL to be /servicesNS/nobody/system/");
        }

        /// <summary>
        /// Establishes and returns a namespace
        /// </summary>
        public Args CreatesNamespace(String username, String appname)
        {
            Args splunkNamespace = new Args();
            
            splunkNamespace.Add("owner", username);
            splunkNamespace.Add("app", appname);

            return splunkNamespace;
        }

        /// <summary>
        /// Tests removing applications that already exist in the collection
        /// </summary>
        [TestMethod]
        public void TestRemoveAppsInNamespace()
        {
            Service service = Connect();
            EntityCollection<Application> apps = service.GetApplications();

            String appname1 = "sdk-app1";
            String appname2 = "sdk-app2";

            if (apps.ContainsKey(appname1))
            {
                apps.Remove(appname1);
            }
            if (apps.ContainsKey(appname2))
            {
                apps.Remove(appname2);
            }
            Assert.IsFalse(apps.ContainsKey(appname1), "Expected the app " + appname1 + " to be removed");
            Assert.IsFalse(apps.ContainsKey(appname2), "Expected the app " + appname2 + " to be removed");
        }

        /// <summary>
        /// Tests removing users that already exists in the collection of splunk users
        /// </summary>
        [TestMethod]
        public void TestRemoveUsersInNamespace()
        {
            Service service = Connect();
            UserCollection users = service.GetUsers();

            String username1 = "sdk-user1";
            String username2 = "sdk-user2";

            if (users.ContainsKey(username1))
            {
                users.Remove(username1);
            }
            if (users.ContainsKey(username2))
            {
                users.Remove(username2);
            }
            Assert.IsFalse(users.ContainsKey(username1), "Expected the username " + username1 + " to be removed");
            Assert.IsFalse(users.ContainsKey(username2), "Expected the username " + username2 + " to be removed");
        }

        /// <summary>
        /// Tests creating users
        /// </summary>
        [TestMethod]
        public void TestCreateUsersInNamespace()
        {
            Service service = Connect();
            UserCollection users = service.GetUsers();

            String username1 = "sdk-user1";
            String username2 = "sdk-user2";

            users.Create(username1, "abc", "user");
            users.Create(username2, "abc", "user");

            Assert.IsTrue(users.ContainsKey(username1), "Expected users to contain " + username1);
            Assert.IsTrue(users.ContainsKey(username2), "Expected users to contain " + username2);
        }

        /// <summary>
        /// Tests creating apps
        /// </summary>
        [TestMethod]
        public void TestCreateAppsInNamespace()
        {
            Service service = Connect();
            EntityCollection<Application> apps = service.GetApplications();

            String appname1 = "sdk-app1";
            String appname2 = "sdk-app2";

            apps.Create(appname1);
            apps.Create(appname2);

            Assert.IsTrue(apps.ContainsKey(appname1), "Expected app to contain " + appname1);
            Assert.IsTrue(apps.ContainsKey(appname2), "Expected app to contain " + appname2);
        }

        /// <summary>
        /// Tests creating namespace specific saved searches and removing those searches
        /// </summary>
        [TestMethod]
        public void TestCreateAndRemoveSavedSearchInNamespace()
        {
            Service service = Connect();

            String username1 = "sdk-user1";
            String username2 = "sdk-user2";
            String appname1 = "sdk-app1";
            String appname2 = "sdk-app2";
            
            // Namespaces
            Args splunkNamespace11 = CreatesNamespace(username1, appname1);
            Args splunkNamespace22 = CreatesNamespace(username2, appname2);
            Args splunkNamespacex1 = CreatesNamespace("-", appname1);

            // Create namespace specific searches
            SavedSearchCollection savedSearches11 = service.GetSavedSearches(splunkNamespace11);
            SavedSearchCollection savedSearches22 = service.GetSavedSearches(splunkNamespace22);
            SavedSearchCollection savedSearchesx1 = service.GetSavedSearches(splunkNamespacex1);

            // Removes test search "sdk-test-search" if it already exists
            if (savedSearches11.ContainsKey("sdk-test-search"))
            {
                savedSearches11.Remove("sdk-test-search");
            }
            if (savedSearches22.ContainsKey("sdk-test-search"))
            {
                savedSearches22.Remove("sdk-test-search");
            }
            Assert.IsFalse(savedSearches11.ContainsKey("sdk-test-search"),
                           "SavedSearches11 already contains sdk-test-search, remove");
            Assert.IsFalse(savedSearches22.ContainsKey("sdk-test-search"),
                           "SavedSearches22 already contains sdk-test-search, remove");

            // Create test search
            savedSearches11.Create("sdk-test-search", "search * | head 1");
            savedSearches22.Create("sdk-test-search", "search * | head 2");
            Assert.IsTrue(savedSearches11.ContainsKey("sdk-test-search"),
                          "Expected savedSearches11 to contain the key sdk-test-search");
            Assert.IsTrue(savedSearches22.ContainsKey("sdk-test-search"),
                          "Expected savedSearches22 to contain the key sdk-test-search");
            Assert.IsTrue(savedSearchesx1.ContainsKey("sdk-test-search", splunkNamespace11),
                          "Expected savedSearchesx1 to contain sdk-test-search in the namespace splunkNameSpace11");
            Assert.IsTrue(savedSearchesx1.Get("sdk-test-search", splunkNamespace11) != null,
                          "Expected savedSearchesx1 to have the test sdk-test-search for splunkNameSpace11");

            // Remove a saved search through a specific namespace path
            savedSearchesx1.Remove("sdk-test-search", splunkNamespace11);
            Assert.IsFalse(savedSearchesx1.ContainsKey("sdk-test-search"),
                           "Expected the saved search sdk-test-search to be removed");

        }

        /// <summary>
        /// Tests cleaning up/removing the created apps and users
        /// </summary>
        [TestMethod]
        public void TestAppAndUserCleanUp()
        {
            Service service = Connect();
            EntityCollection<Application> apps = service.GetApplications();
            UserCollection users = service.GetUsers();

            String appname1 = "sdk-app1";
            String appname2 = "sdk-app2";
            String username1 = "sdk-user1";
            String username2 = "sdk-user2";

            apps.Refresh();
            if (apps.ContainsKey(appname1))
            {
                apps.Remove(appname1);
            }
            if (apps.ContainsKey(appname2))
            {
                apps.Remove(appname2);
            }
            Assert.IsFalse(apps.ContainsKey(appname1), "Expected app " + appname1 + " to be removed");
            Assert.IsFalse(apps.ContainsKey(appname2), "Expected app " + appname2 + " to be removed");

            if (users.ContainsKey(username1))
            {
                users.Remove(username1);
            }
            if (users.ContainsKey(username2))
            {
                users.Remove(username2);
            }
            Assert.IsFalse(users.ContainsKey(username1), "Expected user " + username1 + " to be removed");
            Assert.IsFalse(users.ContainsKey(username2), "Expected user " + username2 + " to be removed");
        }

    }
}