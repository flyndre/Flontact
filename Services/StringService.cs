using flontact.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flontact.Services
{
    public class StringService : IStringService
    {
        public string GetString()
        {
            return "Hello World!";
        }
    }
}
