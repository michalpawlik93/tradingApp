using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tools.Coverlet;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Tools.NerdbankGitVersioning;
using Nuke.Common.Tools.ReportGenerator;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.Coverlet.CoverletTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Common.Tools.ReportGenerator.ReportGeneratorTasks;

partial class Build
{
    AbsolutePath CoverageDirectory => RootDirectory / "coverage";
    AbsolutePath ReportDirectory => RootDirectory / "report";
    Target Backend_TestCoverage =>
        _ =>
            _.DependsOn(Backend_UnitTest)
                .Executes(() =>
                {
                    EnsureCleanDirectory(CoverageDirectory);
                    foreach (var directory in UnitTestDirectories)
                    {
                        DotNetTest(
                            s =>
                                s.SetProjectFile(directory.Value)
                                    .SetConfiguration(Configuration)
                                    .EnableNoRestore()
                                    .EnableNoBuild()
                        );

                        Coverlet(
                            s =>
                                s.SetAssembly(directory.Value)
                                    .SetThreshold(75)
                                    .SetOutput(CoverageDirectory / $"opencover{directory.Key}.xml")
                                    .SetFormat(CoverletOutputFormat.opencover)
                        );
                    }
                });

    Target Backend_Report =>
        _ =>
            _.DependsOn(Backend_TestCoverage)
                .AssuredAfterFailure()
                .Executes(() =>
                {
                    EnsureCleanDirectory(ReportDirectory);
                    foreach (var directory in UnitTestDirectories)
                    {
                        ReportGenerator(
                            s =>
                                s.SetTargetDirectory(ReportDirectory)
                                    .SetFramework("net7.0")
                                    .SetReportTypes(new ReportTypes[] { ReportTypes.Html })
                                    .SetReports(CoverageDirectory / $"opencover{directory.Key}.xml")
                        );
                    }
                });
}
