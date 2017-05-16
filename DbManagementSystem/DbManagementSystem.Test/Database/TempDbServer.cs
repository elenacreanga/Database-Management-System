using System;

namespace DbManagementSystem.Test.Database
{
    internal class TempDbServer
    {
        public void ExecuteOperation(Action testCode, Action revert)
        {
            try
            {
                testCode();
            }
            finally
            {
                revert();
            }
        }
    }
}