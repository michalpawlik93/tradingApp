using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.ReportGenerator;
using Serilog;
using System.IO;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Common.Tools.ReportGenerator.ReportGeneratorTasks;

[ShutdownDotNetAfterServerBuild]
//[GitHubActions(
//    "continuous",
//    GitHubActionsImage.UbuntuLatest,
//    OnPushBranches = new[] { "main" },
//    InvokedTargets = new[] { nameof(BuildBackend), nameof(BuildFrontend), }
//)]
partial class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.BuildBackend, x => x.Frontend_Tests);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild
        ? Configuration.Debug
        : Configuration.Release;

    [Solution]
    readonly Solution Solution;

    AbsolutePath BackendDirectory => RootDirectory / "src";
    AbsolutePath TestsDirectory => RootDirectory / "tests";
    AbsolutePath TestResultDirectory => RootDirectory / "testresults";
    AbsolutePath TestCoverageReportDirectory => RootDirectory / "coverage";

    public string[] ProjectTests =>
        Directory.GetFiles(RootDirectory, $"TradingApp.*Test*.csproj", SearchOption.AllDirectories);

    Target Backend_Clean =>
        _ =>
            _.Executes(() =>
            {
                Log.Information("Cleaning bin directories...");
                BackendDirectory.GlobDirectories("**/bin", "**/obj").DeleteDirectories();
            });

    Target Backend_Restore =>
        _ =>
            _.DependsOn(Backend_Clean)
                .Executes(() =>
                {
                    DotNetRestore(s => s.SetProjectFile(Solution));
                });

    Target Backend_Compile =>
        _ =>
            _.DependsOn(Backend_Restore)
                .Executes(() =>
                {
                    Log.Information("🚀 Build process started");
                    DotNetBuild(
                        s =>
                            s.SetProjectFile(Solution)
                                .SetConfiguration(Configuration)
                                .EnableNoRestore()
                    );
                });

    Target Backend_UnitTest =>
        _ =>
            _.DependsOn(Backend_Compile)
                .Executes(() =>
                {
                    TestResultDirectory.CreateOrCleanDirectory();

                    DotNetTest(
                        _ =>
                            _.SetConfiguration(Configuration)
                                .SetDataCollector("XPlat Code Coverage")
                                .SetSettingsFile(TestsDirectory / "coverlet-settings.xml")
                                .SetResultsDirectory(TestResultDirectory)
                                .EnableNoBuild()
                                .CombineWith(
                                    ProjectTests,
                                    (_, project) => _.SetProjectFile(project)
                                )
                    );
                });

    Target CollectCoverage =>
        _ =>
            _.DependsOn(Backend_UnitTest)
                .AssuredAfterFailure()
                .Executes(() =>
                {
                    TestCoverageReportDirectory.CreateOrCleanDirectory();
                    ReportGenerator(
                        _ =>
                            _.SetFramework("net8.0")
                                .SetReports($"{TestResultDirectory}/**/coverage.opencover.xml")
                                .SetReportTypes(
                                    ReportTypes.Cobertura,
                                    ReportTypes.HtmlInline_AzurePipelines_Dark
                                )
                                .SetTargetDirectory(TestCoverageReportDirectory)
                    );
                });

    Target BuildBackend => _ => _.DependsOn(CollectCoverage).Executes();
}
