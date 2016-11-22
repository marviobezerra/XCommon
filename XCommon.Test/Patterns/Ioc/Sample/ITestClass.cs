using XCommon.Test.Entity;

namespace XCommon.Test.Patterns.Ioc.Sample
{
    public interface ITestClass
    {
        int ClassKey { get; set; }

        PersonEntity LastSavedPerson { get; }


        bool Save(PersonEntity person);

        PersonEntity Load();        
    }
}
