using AventStack.ExtentReports;
using Framework.WayToAutomation.Pages;

namespace Framework.DogAPI
{
    public class DogAPIApp
    {
        public DogAPITask _DogAPITask {get ; set;}

        public DogAPIApp()
        {
            _DogAPITask = new DogAPITask();
        }
    }
}