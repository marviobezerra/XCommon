using System;

namespace XCommon.ProjectGeratorV2.Application.Node
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
