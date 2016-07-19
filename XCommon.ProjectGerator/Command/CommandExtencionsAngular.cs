namespace XCommon.ProjectGerator.Command
{
    public static class CommandExtencionsAngular
    {
        public static CreateProject AddAngularApp(this CreateProject project)
        {
            CreateFile appMain = new CreateFile(new CreateFileParam(project.Parameter, "App", "main.ts") { Template = Resources.Angular2.MainApp });
            project.Add(appMain);

            CreateFile appPolyfill = new CreateFile(new CreateFileParam(project.Parameter, "App", "polyfills.ts") { Template = Resources.Angular2.Polyfills });
            project.Add(appPolyfill);

            CreateFile appVendor = new CreateFile(new CreateFileParam(project.Parameter, "App", "vendor.ts") { Template = Resources.Angular2.Vendor });
            project.Add(appVendor);

            CreateFile appIndex = new CreateFile(new CreateFileParam(project.Parameter, "App", "index.html") { Template = Resources.Angular2.IndexHtml });
            project.Add(appIndex);

            CreateFile appComponentStyle = new CreateFile(new CreateFileParam(project.Parameter, "App\\Components\\Home", "app-home.scss") { Template = Resources.Angular2.ComponentStyle });
            project.Add(appComponentStyle);

            CreateFile appComponentHtml = new CreateFile(new CreateFileParam(project.Parameter, "App\\Components\\Home", "app-home.html") { Template = Resources.Angular2.ComponentHTML });
            project.Add(appComponentHtml);

            CreateFile appComponentType = new CreateFile(new CreateFileParam(project.Parameter, "App\\Components\\Home", "app-home.component.ts") { Template = Resources.Angular2.ComponentTypeScript });
            project.Add(appComponentType);

            CreateFile appComponentIndex = new CreateFile(new CreateFileParam(project.Parameter, "App\\Components\\Home", "index.ts") { Template = Resources.Angular2.ComponentIndex });
            project.Add(appComponentIndex);

            return project;
        }

        public static CreateProject AddAngularStyles(this CreateProject project)
        {
            CreateFile appTheme = new CreateFile(new CreateFileParam(project.Parameter, "App\\Styles", "app.theme.scss") { Template = Resources.Angular2.AppThemeScss });
            project.Add(appTheme);

            CreateFile appVariables = new CreateFile(new CreateFileParam(project.Parameter, "App\\Styles", "app.variables.scss") { Template = Resources.Angular2.AppVariablesScss });
            project.Add(appVariables);
            return project;
        }

        public static CreateProject AddAngularGulp(this CreateProject project, bool simple)
        {
            string template = Resources.Angular2.GulpFileSimple;

            if (!simple)
            {
                template = Resources.Angular2.GulpFile
                    .Replace("[{entity}]", $"{project.Parameter.SolutionParam.SolutionName}.Business.Entity")
                    .Replace("[{resource}]", $"{project.Parameter.SolutionParam.SolutionName}.Business.Resource")
                    .Replace("[{codegenerator}]", $"{project.Parameter.SolutionParam.SolutionName}.CodeGenerator");
            }

            CreateFile file = new CreateFile(new CreateFileParam(project.Parameter, ".", "gulpfile.js") { Template = template });
            project.Add(file);
            return project;
        }

        public static CreateProject AddAngularPackage(this CreateProject project)
        {
            CreateFile file = new CreateFile(new CreateFileParam(project.Parameter, ".", "package.json") { Template = Resources.Angular2.PackageJson });
            project.Add(file);
            return project;
        }

        public static CreateProject AddAngularTsConfig(this CreateProject project)
        {
            CreateFile file = new CreateFile(new CreateFileParam(project.Parameter, ".", "tsconfig.json") { Template = Resources.Angular2.TsConfigJson });
            project.Add(file);
            return project;
        }

        public static CreateProject AddAngularTypings(this CreateProject project)
        {
            CreateFile file = new CreateFile(new CreateFileParam(project.Parameter, ".", "typings.json") { Template = Resources.Angular2.TypingsJson });
            project.Add(file);
            return project;
        }

        public static CreateProject AddAngularWebpack(this CreateProject project)
        {
            CreateFile file = new CreateFile(new CreateFileParam(project.Parameter, ".", "webpack.config.js") { Template = Resources.Angular2.WebpackConfig });
            project.Add(file);
            return project;
        }
    }
}
