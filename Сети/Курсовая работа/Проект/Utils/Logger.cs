using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Course.Utils
{
    public class Logger
    {
        private static readonly Logger instance = new Logger();
        private string _logs;

        private Logger()
        {
            _logs = string.Empty;
        }

        public static Logger Instance
        {
            get
            {
                return instance;
            }
        }

        public void Add(string log)
        {
            _logs += log + Environment.NewLine;
        }

        public void Add(List<string> logs)
        {
            foreach (var log in logs)
            {
                _logs += log + Environment.NewLine;
            }
        }

        public void Add(Dictionary<int, string> logs)
        {
            foreach (var log in logs)
            {
                _logs += log.ToString() + Environment.NewLine;
            }
        }

        public void AddError(string error)
        {
            _logs += "=== ERROR === " + error + Environment.NewLine;
        }

        public string Get()
        {
            return _logs;
        }
    }
}
