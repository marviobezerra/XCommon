namespace XCommon.CodeGeratorV2.Writer.CSharp
{
    public static class SolutionExtension
    {
        public static Project AddProject(this Solution solution, string projectName, ProjectType projectType)
        {
            var project = new Project(projectName, projectType, solution);
            solution.Projects.Add(project);
            return project;
        }

        public static void AddProject(this Solution solution, bool full, bool test, bool angular, bool angularMaterial)
        {
            if (full)
            {
                solution
                    .AddProjectData("Data")
                    .AddProjectEntity("Entity")
                    .AddProjectContract("Contract")
                    .AddProjectConcrete("Concrete")
                    .AddProjectFactory("Factory");

                if (test)
                    solution.AddProjectTest("Test");
            }

            var projectWeb = solution.AddProject($"{solution.Name}.View.Web", ProjectType.Web);

            if (angular)
                projectWeb.AddAngular(angularMaterial);
        }

        public static Solution AddProjectData(this Solution solution, string name)
        {
            var project = solution.AddProject($"{solution.Name}.{name}", ProjectType.ClassLibrary);

            return solution;
        }

        public static Solution AddProjectTest(this Solution solution, string name)
        {
            var project = solution.AddProject($"{solution.Name}.{name}", ProjectType.ClassLibrary);

            return solution;
        }

        public static Solution AddProjectEntity(this Solution solution, string name)
        {
            var project = solution.AddProject($"{solution.Name}.{name}", ProjectType.ClassLibrary);

            return solution;
        }

        public static Solution AddProjectContract(this Solution solution, string name)
        {
            var project = solution.AddProject($"{solution.Name}.{name}", ProjectType.ClassLibrary);

            return solution;
        }

        public static Solution AddProjectConcrete(this Solution solution, string name)
        {
            var project = solution.AddProject($"{solution.Name}.{name}", ProjectType.ClassLibrary);

            return solution;
        }

        public static Solution AddProjectFactory(this Solution solution, string name)
        {
            var project = solution.AddProject($"{solution.Name}.{name}", ProjectType.ClassLibrary);

            return solution;
        }

        public static Solution AddProjectResource(this Solution solution, string name)
        {
            var project = solution.AddProject($"{solution.Name}.{name}", ProjectType.ClassLibrary);

            return solution;
        }
    }
}
