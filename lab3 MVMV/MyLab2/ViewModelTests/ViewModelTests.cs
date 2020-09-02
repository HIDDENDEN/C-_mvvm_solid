using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel;

namespace ViewModelTests
{
    [TestClass]
    public class ViewModelTests
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
        [TestMethod]
        public void NoSaveChangesBeforeNewFileTest()
        {
            ModelDataView modelDataView = new ModelDataView(new NegativeUIServices());
            modelDataView.obs_md.AddDefaults();
            modelDataView.WereChangesSaved = false;
            modelDataView.NewCommand.Execute(null);
            Assert.AreEqual(false, modelDataView.WereChangesSaved);
        }
        [TestMethod]
        public void EmptyCollectionAfterNewCommandTest()
        {
            ModelDataView modelDataView = new ModelDataView(new NegativeUIServices());
            modelDataView.obs_md.AddDefaults();
            modelDataView.WereChangesSaved = false;
            modelDataView.NewCommand.Execute(null);
            Assert.AreEqual(modelDataView.obs_md.Count, 0);
        }
        [TestMethod]
        public void NoSaveChangedDataBeforeClosingTest()
        {
            ModelDataView modelDataView = new ModelDataView(new NegativeUIServices());
            modelDataView.obs_md.AddDefaults();
            modelDataView.WereChangesSaved = false;
            modelDataView.MainWindowClose(null, null);
            Assert.AreEqual(false, modelDataView.WereChangesSaved);
        }
        [TestMethod]
        public void PValueRangeCheckTest()
        {
            ModelDataView modelDataView = new ModelDataView(new NegativeUIServices());
            modelDataView.MyMD.p = -10;
            Assert.AreEqual(false, ModelDataView.pValueCheck(modelDataView.MyMD));
        }
        [TestMethod]
        public void NumOfGridNodesValueRangeCheckTest()
        {
            ModelDataView modelDataView = new ModelDataView(new NegativeUIServices());
            modelDataView.MyMD.num_of_grid_nodes = -5;
            Assert.AreEqual(false, ModelDataView.NumOfGridNodesCheck(modelDataView.MyMD));
        }
    }

    public class NegativeUIServices : ViewModel.IUISurvices
    {
        public bool ConfirmChanges()
        {
            return false;
        }
    }
}
