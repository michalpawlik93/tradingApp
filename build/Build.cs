using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.Npm;
using System.Collections.Generic;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[ShutdownDotNetAfterServerBuild]
partial class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.Backend_UnitTest, x => x.Frontend_Tests);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;

    AbsolutePath FrontendDirectory = RootDirectory / "ui" / "trading-app";

    private static Dictionary<string, AbsolutePath> UnitTestDirectories = new Dictionary<string, AbsolutePath>()
    {
            {"Core", RootDirectory / "tests" / "TradingApp.Core.Test"},
            {"Modules", RootDirectory / "tests" / "TradingApp.Module.Quotes.Test"},
            {"StooqProvider", RootDirectory / "tests" / "TradingApp.StooqProvider.Test"},
            {"TradingAdapter", RootDirectory / "tests" / "TradingApp.Ports.Test"},
            {"TradingViewProvider", RootDirectory / "tests" / "TradingApp.TradingViewProvider.Test"},
    };

    Target Backend_Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Backend_Compile => _ => _
        .DependsOn(Backend_Restore)
        .Executes(() =>
        {
            Logger.Info("🚀 Build process started");
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore());
        });

    Target Backend_UnitTest => _ => _
    .DependsOn(Backend_Compile)
    .Executes(() =>
    {
        EnsureCleanDirectory(CoverageDirectory);
        foreach (var directory in UnitTestDirectories)
        {
            DotNetTest(s => s
                .SetProjectFile(directory.Value)
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .EnableNoBuild());
        }
    });

    Target Frontend_Clean => _ => _
       .Before(Frontend_Restore)
       .Executes(() =>
       {
           (FrontendDirectory / "node_modules").DeleteDirectory();
       });

    Target Frontend_Restore => _ => _
        .DependsOn(Frontend_Clean)
        .Executes(() =>
        {
            NpmTasks.NpmInstall(settings =>
               settings
                   .EnableProcessLogOutput()
                   .SetProcessWorkingDirectory(FrontendDirectory)
                   .SetProcessArgumentConfigurator(e => e.Add("--legacy-peer-deps"))
                   );
        });

    Target Frontend_Tests => _ => _
        .DependsOn(Frontend_Restore)
        .Executes(() =>
        {
            NpmTasks.NpmRun(s => s
              .SetCommand("test")
              .SetProcessWorkingDirectory(FrontendDirectory)
          );
            NpmTasks.NpmRun(s => s
              .SetCommand("coverage")
              .SetProcessWorkingDirectory(FrontendDirectory)
            );
        });
}