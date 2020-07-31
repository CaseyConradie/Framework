using System;
using Framework.Src.Tools;
using Framework.Src.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.MethodLevel)]

namespace Framework.TestsSuites
{
    [TestClass]
    public class APITaskTestSuite
    {
        DogAPITask _DogAPITask;


        [TestMethod, TestCategory("APITests"), TestProperty("Description", "API request to produce a list of all dog breeds.")]
        public void GetListOfDogBreeds()
        {
            var curTest = Base.Report.CreateTest(MethodBase.GetCurrentMethod().ToString().Replace("Void", "").Replace("()","").Trim());
            _DogAPITask = new DogAPITask();
            _DogAPITask.APITask1(curTest);
        }

        [TestMethod, TestCategory("APITests"), TestProperty("Description", "API request to produce a list of sub-breeds for “retriever”.")]
        public void GetListOfSubBreeds()
        {
            var curTest = Base.Report.CreateTest(MethodBase.GetCurrentMethod().ToString().Replace("Void", "").Replace("()","").Trim());
            _DogAPITask = new DogAPITask();
            _DogAPITask.APITask2(curTest);
        }

        [TestMethod, TestCategory("APITests"), TestProperty("Description", "API request to produce a random dog Image.")]
        public void GetRandomImage()
        {
           var curTest = Base.Report.CreateTest(MethodBase.GetCurrentMethod().ToString().Replace("Void", "").Replace("()","").Trim());
           _DogAPITask = new DogAPITask();
            _DogAPITask.APITask3(curTest);
        }

    }

}