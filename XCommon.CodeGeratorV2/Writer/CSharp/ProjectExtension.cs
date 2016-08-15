namespace XCommon.CodeGeratorV2.Writer.CSharp
{
    public static class ProjectExtension
    {
        public static Project AddClass(this Project project, Class @class)
        {
            project.Classes.Add(@class);
            return project;
        }

        public static Project AddDependency(this Project project, string dependecy)
        {
            project.Dependency.Add(dependecy);
            return project;
        }

        public static Project AddAngular(this Project project, bool material)
        {
            return project;
        }
    }
}
