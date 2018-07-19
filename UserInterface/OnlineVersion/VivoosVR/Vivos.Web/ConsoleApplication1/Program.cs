using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceClientCredentials cred = new CustomServiceClientCredentials(); 
            VivoosVRWeb web = new VivoosVRWeb(cred);
            web.CustomerApi.GetNotMyAssetsWithImages();
        }
    }
}
