using System;
using System.Diagnostics;
using System.IO;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {

            //string file = @"D:\test\fileout.pdf";
            Process.Start("IExplore.exe", "www.northwindtraders.com");
            //Process.Start("IExplore.exe", @"D:\test\file.pdf");

            //Process p = new Process();
            //p.StartInfo = new ProcessStartInfo()
            //{
            //    CreateNoWindow = true,
            //    FileName = "AcroRd32.exe", //put the path to the pdf reading software e.g. Adobe Acrobat
            //    Arguments = file // put the path of the pdf file you want to print
            //};
            //p.Start();




        }
    }
}
