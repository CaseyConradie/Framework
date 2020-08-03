using AventStack.ExtentReports;
using Framework.WayToAutomation.Pages;

namespace Framework.DogAPI
{
    public class DogAPIApp
    {
        DogAPITask _DogAPITask;

        public DogAPIApp()
        {
            _DogAPITask = new DogAPITask();
        }

        public void DogAPIAPITask1(ExtentTest currentTest)
        {
            _DogAPITask.APITask1(currentTest);
        }
        public void DogAPIAPITask2(ExtentTest currentTest)
        {
            _DogAPITask.APITask2(currentTest);
        }
        public void DogAPIAPITask3(ExtentTest currentTest)
        {
            _DogAPITask.APITask3(currentTest);
        }
    }
}