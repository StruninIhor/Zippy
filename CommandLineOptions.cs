using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zippy
{
    public class CommandLineOptions
    {
        [Option('p', "operation", Default = 'e', HelpText = "e - extract, l -list")]
        public char Operation { get; set; }

        [Option('i', "input", Required = true)]
        public string InputFile { get; set; }
        [Option('o', "output", Default = ".")]
        public string OutputDirectory { get; set; }
        [Option('v', "verbose", Default = true)]
        public bool Verbose { get; set; }
    }
}
