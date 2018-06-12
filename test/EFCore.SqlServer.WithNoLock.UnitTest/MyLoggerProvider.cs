using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace EFCore.SqlServer.WithNoLock.UnitTest
{
    public class MyLoggerProvider : ILoggerProvider
    {

        private readonly ITestOutputHelper _testOutputHelper;

        public MyLoggerProvider(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        public ILogger CreateLogger(string categoryName)
        {


            return new MyLogger(_testOutputHelper);

        }

        public void Dispose()
        { }

        private class MyLogger : ILogger
        {
            private readonly ITestOutputHelper _testOutputHelper;

            public MyLogger(ITestOutputHelper testOutputHelper)
            {
                _testOutputHelper = testOutputHelper;
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                _testOutputHelper.WriteLine(formatter(state, exception));
            }

            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }
        }


    }
}
