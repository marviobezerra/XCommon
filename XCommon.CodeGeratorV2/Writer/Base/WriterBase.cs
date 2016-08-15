namespace XCommon.CodeGeratorV2.Writer.Base
{
    public abstract class WriterBase
    {
        public void CheckDirectory(string name)
        {
            if (!System.IO.Directory.Exists(name))
                System.IO.Directory.CreateDirectory(name);
        }

        public void WriteFile(string name, string content)
        {
            CheckDirectory(System.IO.Path.GetDirectoryName(name));

            System.IO.File.WriteAllText(name, content, System.Text.Encoding.UTF8);
        }
    }
}
