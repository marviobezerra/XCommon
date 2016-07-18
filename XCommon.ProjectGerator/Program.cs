using XCommon.ProjectGerator.Enumeration;
using XCommon.ProjectGerator.Steps;
using System;

namespace XCommon.ProjectGerator
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandsList appCommand = new CommandsList();

            var solution = appCommand
                .AddSolution(@"D:\Temp", "Prospect.Agnes");

            CreateProject businessConcret = solution
                .AddProjectClass("Business.Concret")
                .AddProjectJson(ProjectJson.BusinessContract);

            CreateProject businessContract = solution
                .AddProjectClass("Business.Contract")
                .AddProjectJson(ProjectJson.BusinessContract);

            CreateProject businessEntity = solution
                .AddProjectClass("Business.Entity")
                .AddProjectJson(ProjectJson.BusinessEntity);

            CreateProject businessData = solution
                .AddProjectClass("Business.Data")
                .AddProjectJson(ProjectJson.BusinessData);

            CreateProject businessFactory = solution
                .AddProjectClass("Business.Factory")
                .AddProjectJson(ProjectJson.BusinessFactory)
                .AddFactoryDo();

            CreateProject businessResource = solution
                .AddProjectClass("Business.Resource")
                .AddProjectJson(ProjectJson.BusinessResource);
            
            CreateProject viewWeb = solution
                .AddProjectWeb("View.Web")
                .AddProjectJson(ProjectJson.ViewWeb)
                .AddAppStart(false)
                .AddAppSettings()
                .AddGulp(false)
                .AddPackage()
                .AddTsConfig()
                .AddTypings()
                .AddWebConfig()
                .AddWebpackConfig();

            appCommand.Run();

            Console.ReadKey();
        }
    }
}
