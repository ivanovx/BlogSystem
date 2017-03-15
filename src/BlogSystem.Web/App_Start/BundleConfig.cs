﻿namespace BlogSystem.Web
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterScripts(bundles);
            RegisterStyles(bundles);
        }

        private static void RegisterStyles(BundleCollection bundles)
        {
            bundles
                .Add(new StyleBundle("~/Content/css")
                .Include(
                    "~/Content/bootstrap.css",
                    "~/Content/font-awesome.css",
                    "~/Content/tether.css",
                    "~/Content/vs.css",
                    "~/Content/blog.css"
                ));
        }

        private static void RegisterScripts(BundleCollection bundles)
        {
            bundles
                .Add(new ScriptBundle("~/bundles/jquery")
                .Include(
                    "~/Scripts/jquery-{version}.js", 
                    "~/Scripts/jquery.unobtrusive-ajax.js",
                    "~/Scripts/jquery.nicescroll.js",
                    "~/Scripts/blog.js"
                ));

            bundles
                .Add(new ScriptBundle("~/bundles/jqueryval")
                .Include("~/Scripts/jquery.validate*"));

            bundles
                .Add(new ScriptBundle("~/bundles/bootstrap")
                .Include("~/Scripts/tether.js", "~/Scripts/bootstrap.js"));

            bundles
                .Add(new ScriptBundle("~/bundles/highlight")
                .Include("~/Scripts/highlight.pack.js"));

            bundles
                .Add(new ScriptBundle("~/bundles/notify")
                .Include("~/Scripts/notify.js"));
        }
    }
}