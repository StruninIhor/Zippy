using CommandLine;
using SharpCompress.Archives;
using SharpCompress.Common;
using SharpCompress.IO;
using System;
using System.IO;
using System.Linq;

namespace Zippy
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsed(options =>
            {
                bool unzip = options.Operation == 'e';
                using var stream = new NonDisposingStream(File.OpenRead(options.InputFile), true);
                using var reader = ArchiveFactory.Open(stream);
                try
                {
                    foreach (var entry in reader.Entries.Where(entry => !entry.IsDirectory))
                    {
                       if (unzip)
                        {
                            if (options.Verbose)
                            {
                                Console.WriteLine($"Processing {entry}");
                            }
                            entry.WriteToDirectory(options.OutputDirectory,
                                              new ExtractionOptions()
                                              {
                                                  ExtractFullPath = true,
                                                  Overwrite = true
                                              });
                        } else
                        {
                            Console.WriteLine(entry.ToString());
                        }
                    }
                }
                catch (IndexOutOfRangeException)
                {
                  //SevenZipArchive_BZip2_Split test needs this
                  stream.ThrowOnDispose = false;
                    throw;
                }
                stream.ThrowOnDispose = false;
            });
        }
    }
}
