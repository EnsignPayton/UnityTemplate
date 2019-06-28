var target = Argument("target", "Default");
var config = Argument("config", "Debug");

const string project = "Template/Template.csproj";

Task("Build-Project")
    .Does(() =>
    {
        DotNetCoreBuild(project, new DotNetCoreBuildSettings
        {
            Configuration = config
        });
    });

// TODO: Run Test Project Here

Task("Publish-Project")
    .IsDependentOn("Build-Project")
    .Does(() =>
    {
        DotNetCorePublish(project, new DotNetCorePublishSettings
        {
            Configuration = config,
            NoBuild = true
        });
    });

Task("Copy-Packages")
    .IsDependentOn("Publish-Project")
    .Does(() =>
    {
        CopyFiles($"Template/bin/{config}/netstandard2.0/publish/UnityWrap.dll", "../Assets/Scripts");
    });

Task("Default")
    .IsDependentOn("Copy-Packages");

RunTarget(target);
