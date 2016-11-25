using System;

namespace XCommon.ProjectGerator.Application.Node
{
    public class NodeApplication : ApplicationBase
    {
        public NodeApplication(string[] args) : base(args)
        {
        }

        protected override void SetUp(string[] args)
        {
            args = RemoveArg("--node", args);
            Console.WriteLine("NODE app");

        }
    }
}
