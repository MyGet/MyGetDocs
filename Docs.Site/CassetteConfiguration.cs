using Cassette;
using Cassette.Scripts;
using Cassette.Stylesheets;

namespace Docs.Site
{
    /// <summary>
    /// Configures the Cassette asset bundles for the web application.
    /// </summary>
    public class CassetteBundleConfiguration : IConfiguration<BundleCollection>
    {
        public void Configure(BundleCollection bundles)
        {
            bundles.Add<StylesheetBundle>("Content", new[]
            {
                "~/Content/less/bootstrap.less",
                "~/Content/less/responsive.less",
                "~/Content/less/slider.less",
                "~/Content/less/myget.less",
                "~/Content/prettify.css"
            });

            bundles.Add<ScriptBundle>("Scripts", new[]
            {
                "~/Scripts/jquery-1.9.1.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/bootstrap-slider.js",
                "~/Scripts/autocolumn.js",
                "~/Scripts/prettify/prettify.js",
                "~/Scripts/Helpers.js"
            });
        }
    }
}