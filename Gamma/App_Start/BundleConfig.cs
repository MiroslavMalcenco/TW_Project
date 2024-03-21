using System.Drawing;
using System.Web.Optimization;

namespace Gamma.WEB
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Home style
            bundles.Add(new StyleBundle("~/bundles/main/css").Include("~/Content/css (MINTON)/bootstrap.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/main/css").Include("~/Content/css (MINTON)/materialdesignicons.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/main/css").Include("~/Content/css (MINTON)/pe-icon-7-stroke.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/main/css").Include("~/Content/css (MINTON)/style.css", new CssRewriteUrlTransform()));

        }
    }
}