using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommon.Patterns.Specification.Entity.Implementation;
using XCommon.Test.Patterns.Specification.Helper;

namespace XCommon.Test.Patterns.Specification.Entity
{
    [TestClass]
    public class AndIsNotEmptyTest
    {
        [TestMethod]
        public void Patterns_Specification_Entity_AndIsNotEmpty_Int_NotNull()
        {
            SpecificationEntity<HelperSpecificationEntity> spec = new SpecificationEntity<HelperSpecificationEntity>()
                .AndIsNotEmpty(c => c.Int);

            var result = spec.IsSatisfiedBy(new HelperSpecificationEntity(false));

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndIsNotEmpty_Date_NotNull()
        {
            SpecificationEntity<HelperSpecificationEntity> spec = new SpecificationEntity<HelperSpecificationEntity>()
                .AndIsNotEmpty(c => c.DateTime);

            var result = spec.IsSatisfiedBy(new HelperSpecificationEntity(false));

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndIsNotEmpty_String_NotNull()
        {
            SpecificationEntity<HelperSpecificationEntity> spec = new SpecificationEntity<HelperSpecificationEntity>()
                .AndIsNotEmpty(c => c.String);

            var result = spec.IsSatisfiedBy(new HelperSpecificationEntity(false));

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndIsNotEmpty_Object_NotNull()
        {
            SpecificationEntity<HelperSpecificationEntity> spec = new SpecificationEntity<HelperSpecificationEntity>()
                .AndIsNotEmpty(c => c.Item);

            var result = spec.IsSatisfiedBy(new HelperSpecificationEntity(false));

            Assert.AreEqual(true, result);
        }
        
        [TestMethod]
        public void Patterns_Specification_Entity_AndIsNotEmpty_Int_Null()
        {

            SpecificationEntity<HelperSpecificationEntity> spec = new SpecificationEntity<HelperSpecificationEntity>()
                .AndIsNotEmpty(c => c.Int);

            var result = spec.IsSatisfiedBy(new HelperSpecificationEntity(true));

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndIsNotEmpty_Date_Null()
        {
            SpecificationEntity<HelperSpecificationEntity> spec = new SpecificationEntity<HelperSpecificationEntity>()
               .AndIsNotEmpty(c => c.DateTime);

            var result = spec.IsSatisfiedBy(new HelperSpecificationEntity(true));

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndIsNotEmpty_String_Null()
        {
            SpecificationEntity<HelperSpecificationEntity> spec = new SpecificationEntity<HelperSpecificationEntity>()
               .AndIsNotEmpty(c => c.String);

            var result = spec.IsSatisfiedBy(new HelperSpecificationEntity(true));

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Patterns_Specification_Entity_AndIsNotEmpty_Object_Null()
        {
            SpecificationEntity<HelperSpecificationEntity> spec = new SpecificationEntity<HelperSpecificationEntity>()
               .AndIsNotEmpty(c => c.Item);

            var result = spec.IsSatisfiedBy(new HelperSpecificationEntity(true));

            Assert.AreEqual(false, result);
        }
    }
}
