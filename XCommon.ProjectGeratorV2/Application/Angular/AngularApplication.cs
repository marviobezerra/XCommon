using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommon.ProjectGeratorV2.Application.Commands;
using XCommon.ProjectGeratorV2.Application.CSharp.Writter;

namespace XCommon.ProjectGeratorV2.Application.Angular
{
    public class AngularApplication
    {
        public AngularApplication(string name, string basePath, string publishPath)
        {
            Name = name;
            BasePath = basePath;
            PublishPath = publishPath;

            Commands = new List<ICommand>();
            CommandsPost = new List<ICommand>();

            SetUp();
            SetUpPost();
        }

        private string PublishPath { get; set; }

        private string Name { get; set; }

        private string BasePath { get; set; }

        public List<ICommand> Commands { get; set; }

        public List<ICommand> CommandsPost { get; set; }

        private void SetUp()
        {
            Commands.Add(new CreateFile(new CreateFileParam(BasePath, "App", "app.module.ts") { Template = Resources.Angular2.AppModule }));
            Commands.Add(new CreateFile(new CreateFileParam(BasePath, "App", "main.ts") { Template = Resources.Angular2.MainApp }));
            Commands.Add(new CreateFile(new CreateFileParam(BasePath, "App", "polyfills.ts") { Template = Resources.Angular2.Polyfills }));
            Commands.Add(new CreateFile(new CreateFileParam(BasePath, "App", "vendors.ts") { Template = Resources.Angular2.Vendor }));
            Commands.Add(new CreateFile(new CreateFileParam(BasePath, "App", "index.html") { Template = Resources.Angular2.IndexHtml }));

            Commands.Add(new CreateFile(new CreateFileParam(BasePath, "App\\Components", "app-components.ts") { Template = Resources.Angular2.AppComponents }));
            Commands.Add(new CreateFile(new CreateFileParam(BasePath, "App\\Components", "app-routes.ts") { Template = Resources.Angular2.AppRoutes }));

            Commands.Add(new CreateFile(new CreateFileParam(BasePath, "App\\Components\\Layout", "app-layout.component.ts") { Template = Resources.Angular2.LayoutComponent }));
            Commands.Add(new CreateFile(new CreateFileParam(BasePath, "App\\Components\\Layout", "app-layout.html") { Template = Resources.Angular2.LayoutHtml }));
            Commands.Add(new CreateFile(new CreateFileParam(BasePath, "App\\Components\\Layout", "app-layout.scss") { Template = Resources.Angular2.LayoutSCSS }));


            Commands.Add(new CreateFile(new CreateFileParam(BasePath, "App\\Styles", "app-theme.scss") { Template = Resources.Angular2.AppThemeScss }));
            Commands.Add(new CreateFile(new CreateFileParam(BasePath, "App\\Styles", "app-variables.scss") { Template = Resources.Angular2.AppVariablesScss }));

            Commands.Add(new CreateFile(new CreateFileParam(BasePath, ".", "package.json") { Template = Resources.Angular2.PackageJson }));
            Commands.Add(new CreateFile(new CreateFileParam(BasePath, ".", "tsconfig.json") { Template = Resources.Angular2.TsConfigJson }));

            var webPakcTemplate = Resources.Angular2.WebpackConfig
                .Replace("[{output}]", PublishPath);

            Commands.Add(new CreateFile(new CreateFileParam(BasePath, ".", "webpack.config.js") { Template = webPakcTemplate }));
        }



        private void SetUpPost()
        {
            string initialAboutComponents = "run -- -a -f About -c " + string.Join(" ", InitialComponents());
            string argumentsNPM = "install " + string.Join(" ", AngularPackages()) + " --save";
            string argumentsTyping = "install " + string.Join(" ", TypingsPackages()) + " --global --save";

            CommandsPost.Add(new CommandShell(new CommandShellParam { Name = "Creating basic Angular 'Home' components", Command = "dotnet", Arguments = "run -- -a -f Home -c Home", Directory = Path.Combine(BasePath, "..", $"{Name}.CodeGenerator") }));
            CommandsPost.Add(new CommandShell(new CommandShellParam { Name = "Creating basic Angular 'About' components", Command = "dotnet", Arguments = initialAboutComponents, Directory = Path.Combine(BasePath, "..", $"{Name}.CodeGenerator") }));
            CommandsPost.Add(new CommandShell(new CommandShellParam { Name = "Creating basic Angular 'System' components", Command = "dotnet", Arguments = "run -- -a -f System -c NotFound", Directory = Path.Combine(BasePath, "..", $"{Name}.CodeGenerator") }));

			CommandsPost.Add(new CommandShell(new CommandShellParam { Name = "Installing NPM packages", Command = "npm", Arguments = argumentsNPM, Directory = BasePath }));
			CommandsPost.Add(new CommandShell(new CommandShellParam { Name = "Installing Typings", Command = "typings", Arguments = argumentsTyping, Directory = BasePath }));
		}

        public List<string> AngularPackages()
        {
            List<string> result = new List<string>();

            result.Add("@angular/common");
            result.Add("@angular/compiler");
            result.Add("@angular/core");
            result.Add("@angular/forms");
            result.Add("@angular/http");
            result.Add("@angular/platform-browser");
            result.Add("@angular/platform-browser-dynamic");
            result.Add("@angular/router");
            result.Add("@angular2-material/button");
            result.Add("@angular2-material/button-toggle");
            result.Add("@angular2-material/card");
            result.Add("@angular2-material/checkbox");
            result.Add("@angular2-material/core");
            result.Add("@angular2-material/grid-list");
            result.Add("@angular2-material/icon");
            result.Add("@angular2-material/input");
            result.Add("@angular2-material/list");
            result.Add("@angular2-material/menu");
            result.Add("@angular2-material/progress-bar");
            result.Add("@angular2-material/progress-circle");
            result.Add("@angular2-material/radio");
            result.Add("@angular2-material/sidenav");
            result.Add("@angular2-material/slide-toggle");
            result.Add("@angular2-material/slider");
            result.Add("@angular2-material/tabs");
            result.Add("@angular2-material/toolbar");
            result.Add("@angular2-material/tooltip");
            result.Add("angular2-text-mask");
            result.Add("core-js");
            result.Add("reflect-metadata");
            result.Add("rxjs");
            result.Add("zone.js");
            result.Add("css-loader");
            result.Add("extract-text-webpack-plugin");
            result.Add("gulp");
            result.Add("gulp-htmlmin");
            result.Add("gulp-livereload");
            result.Add("gulp-rimraf");
            result.Add("gulp-shell");
            result.Add("gulp-util");
            result.Add("node-sass");
            result.Add("path");
            result.Add("raw-loader");
            result.Add("sass-loader");
            result.Add("strip-sourcemap-loader");
            result.Add("style-loader");
            result.Add("ts-loader");
            result.Add("typescript");
            result.Add("typings");
            result.Add("webpack");
            result.Add("webpack-merge");
            result.Add("webpack-stream");

            return result;
        }

        public List<string> TypingsPackages()
        {
            List<string> result = new List<string>();

            result.Add("dt~core-js");
            result.Add("dt~hammerjs");
            result.Add("dt~jasmine");
            result.Add("dt~node");

            return result;
        }

        public List<string> InitialComponents()
        {
            List<string> result = new List<string>();

            result.Add("AboutHome?o");
			result.Add("About");
			result.Add("Privacy");
            result.Add("Terms");
            result.Add("Contact");

            return result;
        }
    }
}
