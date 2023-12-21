using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.Npm;

partial class Build
{
    AbsolutePath FrontendDirectory = RootDirectory / "ui" / "trading-app";
    Target Frontend_Clean =>
        _ =>
            _.Before(Frontend_Restore)
                .Executes(() =>
                {
                    (FrontendDirectory / "node_modules").DeleteDirectory();
                });

    Target Frontend_Restore =>
        _ =>
            _.DependsOn(Frontend_Clean)
                .Executes(() =>
                {
                    NpmTasks.NpmInstall(
                        settings =>
                            settings
                                .EnableProcessLogOutput()
                                .SetProcessWorkingDirectory(FrontendDirectory)
                                .SetProcessArgumentConfigurator(e => e.Add("--legacy-peer-deps"))
                    );
                });

    Target Frontend_Tests =>
        _ =>
            _.DependsOn(Frontend_Restore)
                .Executes(() =>
                {
                    NpmTasks.NpmRun(
                        s => s.SetCommand("test").SetProcessWorkingDirectory(FrontendDirectory)
                    );
                    NpmTasks.NpmRun(
                        s => s.SetCommand("coverage").SetProcessWorkingDirectory(FrontendDirectory)
                    );
                });
}
