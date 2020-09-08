using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMDEvers.CQRS.Sample.Domain.Services
{
    public class ExampleService : IExampleService
    {
        public string GetValue()
        {
            return "Sample Value";
        }
    }

    public interface IExampleService
    {
        string GetValue();
    }
}
