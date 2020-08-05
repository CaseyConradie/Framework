using Framework.Src.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using Framework.DogAPI;

[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.MethodLevel)]

namespace Framework.TestsSuites
{
    [TestClass]
    public class APITaskTestSuite
    {
        public TestContext TestContext { get; set; }
        private DogAPIApp _dogAPI;

        [TestMethod, TestCategory("APITests"), TestProperty("Description", "API request to produce a list of all dog breeds.")]
        public void GetListOfDogBreeds()
        {
            var curTest = Base.Report.CreateTest(TestContext.TestName);
            _dogAPI = new DogAPIApp();
            _dogAPI._DogAPITask.APITask1(curTest);
        }

        [TestMethod, TestCategory("APITests"), TestProperty("Description", "API request to produce a list of sub-breeds for “retriever”.")]
        public void GetListOfSubBreeds()
        {
            var curTest = Base.Report.CreateTest(TestContext.TestName);
            _dogAPI = new DogAPIApp();
            _dogAPI._DogAPITask.APITask2(curTest);
        }

        [TestMethod, TestCategory("APITests"), TestProperty("Description", "API request to produce a random dog Image.")]
        public void GetRandomImage()
        {
            var curTest = Base.Report.CreateTest(TestContext.TestName);
            _dogAPI = new DogAPIApp();
            _dogAPI._DogAPITask.APITask3(curTest);
        }

    }

}