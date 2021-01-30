﻿using System.Web.Optimization;

namespace K9.WebApplication
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/lib").Include(
                "~/Content/fontawesome/all.css",
                "~/Content/fontawesome/font-awesome-legacy.css",
                "~/Content/bootstrap/*.css"));

            bundles.Add(new StyleBundle("~/Content/site").Include(
                "~/Content/site/elements.css",
                "~/Content/site/classes.css",
                "~/Content/site/shared.css",
                "~/Content/site/shared-md.css",
                "~/Content/site/shared-sm.css",
                "~/Content/site/shared-xs.css",
                "~/Content/site/validation.css",
                "~/Content/site/desktop.css",
                "~/Content/site/tablet.css",
                "~/Content/site/mobile.css",
                "~/Content/site/tables.css",
                "~/Content/site/navbar.css",
                "~/Content/site/sections/*.css"));

            bundles.Add(new StyleBundle("~/Content/k9").Include(
                "~/Content/k9/*.css"));

            bundles.Add(new StyleBundle("~/Content/tpl").Include(
                "~/Content/template/lsb.css",
                "~/Content/template/owl.carousel.css",
                "~/Content/template/owl.theme.css",
                "~/Content/template/style.css"));

            bundles.Add(new StyleBundle("~/Content/responsive").Include(
                "~/Content/template/style.1200.css",
                "~/Content/template/style.1080.css",
                "~/Content/template/style.1024.css",
                "~/Content/template/style-lg.css",
                "~/Content/template/style-md.css",
                "~/Content/template/style-sm.css",
                "~/Content/template/style-xs.css",
                "~/Content/template/style.991.css",
                "~/Content/template/style.768.css",
                "~/Content/template/style.760.css",
                "~/Content/template/style.736.css",
                "~/Content/template/style.610.css",
                "~/Content/template/style.525.css",
                "~/Content/template/style.480.css",
                "~/Content/template/style.414.css",
                "~/Content/template/style.384.css",
                "~/Content/template/style.375.css",
                "~/Content/template/style.320.css"));

            bundles.Add(new ScriptBundle("~/Scripts/js").Include(
                "~/Scripts/imageSwitcher/*.js",
                "~/Scripts/template/*.js",
                "~/Scripts/ajax/*.js",
                "~/Scripts/k9/*.js"));

            bundles.Add(new ScriptBundle("~/Scripts/lib").Include(
                "~/Scripts/library/*.js"));

            BundleTable.EnableOptimizations = false;
        }
    }
}