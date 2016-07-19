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
                .AddAspNetProjectJson()
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

            return commandList;
        }

        public static CommandsList WebSimple(this CommandsList commandList, string path, string name)
        {
            var solution = commandList
                .AddCSharpSolution(path, name);
            
            CreateProject viewWeb = solution
                .AddAspNetProject("View.Web")
                .AddAspNetProjectJson()
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

            return commandList;
        }
    }
}
