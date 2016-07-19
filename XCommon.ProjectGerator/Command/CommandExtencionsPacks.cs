namespace XCommon.ProjectGerator.Command
{
    public static class CommandExtencionsPacks
    {
        public static CommandsList WebFull(this CommandsList commandList, string path, string name)
        {
            var solution = commandList
                .AddCSharpSolution(path, name);

            CreateProject businessConcret = solution
                .AddCSharpProject("Business.Concrete")
                .AddCSharpProjectJson(ProjectJson.BusinessConcrecte);

            CreateProject businessContract = solution
                .AddCSharpProject("Business.Contract")
                .AddCSharpProjectJson(ProjectJson.BusinessContract);

            CreateProject businessEntity = solution
                .AddCSharpProject("Business.Entity")
                .AddCSharpProjectJson(ProjectJson.BusinessEntity);

            CreateProject businessData = solution
                .AddCSharpProject("Business.Data")
                .AddCSharpProjectJson(ProjectJson.BusinessData);

            CreateProject businessFactory = solution
                .AddCSharpProject("Business.Factory")
                .AddCSharpProjectJson(ProjectJson.BusinessFactory)
                .AddCSharpFactoryDo();

            CreateProject businessResource = solution
                .AddCSharpProject("Business.Resource")
                .AddCSharpProjectJson(ProjectJson.BusinessResource);

            CreateProject viewWeb = solution
                .AddAspNetProject("View.Web")
                .AddAspNetProjectJson(false)
                .AddAspNetWebConfig()
                .AddAspNetStart(false)
                .AddAspNetSettings()
                .AddAngularGulp(false)
                .AddAngularPackage()
                .AddAngularTsConfig()
                .AddAngularTypings()
                .AddAngularWebpack()
                .AddAngularApp()
                .AddAngularStyles();

            AddPostRun(commandList, solution, viewWeb);

            return commandList;
        }

        public static CommandsList WebSimple(this CommandsList commandList, string path, string name)
        {
            var solution = commandList
                .AddCSharpSolution(path, name);

            CreateProject viewWeb = solution
                .AddAspNetProject("View.Web")
                .AddAspNetProjectJson(true)
                .AddAspNetWebConfig()
                .AddAspNetStart(true)
                .AddAspNetSettings()
                .AddAngularGulp(true)
                .AddAngularPackage()
                .AddAngularTsConfig()
                .AddAngularTypings()
                .AddAngularWebpack()
                .AddAngularApp()
                .AddAngularStyles();

            AddPostRun(commandList, solution, viewWeb);

            return commandList;
        }

        private static void AddPostRun(CommandsList commandList, CreateSolution solution, CreateProject viewWeb)
        {
            commandList.PostRun.Add(new CommandPostRun { Name = "Restoring DotNet packages", Command = "dotnet", Arguments = "restore", Directory = solution.Parameter.Path });
            commandList.PostRun.Add(new CommandPostRun { Name = "Restoring NPM packages", Command = "npm", Arguments = "install", Directory = viewWeb.Parameter.Path });
            commandList.PostRun.Add(new CommandPostRun { Name = "Running Gulp", Command = "gulp", Arguments = "default:dev", Directory = viewWeb.Parameter.Path });
        }
    }
}
