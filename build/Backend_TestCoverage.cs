using Nuke.Common.IO;


partial class Build
{
    AbsolutePath CoverageDirectory => RootDirectory / "coverage";
    AbsolutePath ReportDirectory => RootDirectory / "report";

    //Target Backend_Report =>
    //    _ =>
    //        _.DependsOn(Backend_TestCoverage)
    //            .AssuredAfterFailure()
    //            .Executes(() =>
    //            {
    //                EnsureCleanDirectory(ReportDirectory);
    //                foreach (var directory in UnitTestDirectories)
    //                {
    //                    ReportGenerator(
    //                        s =>
    //                            s.SetTargetDirectory(ReportDirectory)
    //                                .SetFramework("net7.0")
    //                                .SetReportTypes(new ReportTypes[] { ReportTypes.Html })
    //                                .SetReports(CoverageDirectory / $"opencover{directory.Key}.xml")
    //                    );
    //                }
    //            });
}
