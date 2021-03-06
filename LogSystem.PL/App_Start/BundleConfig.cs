﻿using System.Web.Optimization;

namespace LogSystem.PL
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            // Angular bundles
            bundles.Add(new ScriptBundle("~/bundles/Angular")
              .Include(
                "~/Angular/dist/Angular/inline.*",
                "~/Angular/dist/Angular/polyfills.*",
                "~/Angular/dist/Angular/scripts.*",
                "~/Angular/dist/Angular/vendor.*",
                "~/Angular/dist/Angular/runtime.*",
                "~/Angular/dist/Angular/main.*"));

            bundles.Add(new StyleBundle("~/Content/Angular")
              .Include("~/Angular/dist/Angular/styles.*"));
        }
    }
}
