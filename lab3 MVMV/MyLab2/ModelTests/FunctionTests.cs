using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelTests
{
    [TestClass]
    public class FunctionTests
    {
        [TestMethod]
        public void TestFuncValue1()
        {
            double p = 2.0;
            Model.ModelData testModelData = new Model.ModelData(4, p);
            Assert.AreEqual(16, testModelData.funcForm.MyFunc(2, p), 0.1);
        }

        [TestMethod]
        public void TestFuncValue2()
        {
            double p = 2.0;
            Model.ModelData testModelData = new Model.ModelData(4, p);
            Assert.AreEqual(25, testModelData.funcForm.MyFunc(3, p), 0.1);
        }

        [TestMethod]
        public void TestFuncValue3()
        {
            double p = 2.0;
            Model.ModelData testModelData = new Model.ModelData(4, p);
            Assert.AreEqual(36, testModelData.funcForm.MyFunc(4, p), 0.1);
        }

        
    }
    

}
