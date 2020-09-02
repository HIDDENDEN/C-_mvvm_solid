using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelTests
{
    [TestClass]
    public class CollectionTests
    {
        [TestMethod]
        public void TestCollectionChange1()
        {
            Model.ObservableModelData obs_md = new Model.ObservableModelData();
            obs_md.AddDefaults();
            Assert.IsTrue(obs_md.IsChanged);
        }
        [TestMethod]
        public void TestCollectionChange2()
        {
            Model.ObservableModelData obs_md = new Model.ObservableModelData();
            obs_md.Add_ModelData(new Model.ModelData(5, 5));
            Assert.IsTrue(obs_md.IsChanged);
        }
        [TestMethod]
        public void TestCollectionChange3()
        {
            Model.ObservableModelData obs_md = new Model.ObservableModelData();
            obs_md.AddDefaults();
            obs_md.Remove_All();
            Assert.IsTrue(obs_md.IsChanged);
        }
    }
}
