﻿using CommandLine;
using CommandLine.Text;
using DfuConvCli.Interfaces;
using System.Collections.Generic;

namespace DfuConvCli.Tools.Create {
    [Verb("create", HelpText = "Create empty DFU.")]
    internal class Options : IVerbOptions {
        [Value(0)] public string File { get; set; }

        [Option(
            'd',
            "device",
            Required = true,
            Default = "0xffff",
            HelpText = "Firmware version contained in the file, or 0xffff if ignored.")]
        public string Device { get; set; }

        [Option(
            'p',
            "product",
            Required = true,
            Default = "0xffff",
            HelpText = "Device's Product ID or 0xffff if ignored.")]
        public string Product { get; set; }

        [Option(
            'v',
            "vendor",
            Required = true,
            Default = "0xffff",
            HelpText = "Device's VEndor ID or 0xffff if ignored.")]
        public string Vendor { get; set; }

        [Usage(ApplicationAlias = "dfuconvcli")]
        public static IEnumerable<Example> Examples =>
            new List<Example> {
                new Example(
                    "Create empty DFU file.",
                    new Options {
                        File = "sample.dfu"
                    }),
                new Example(
                    "Create empty DFU file with specified IDs.",
                    new Options {
                        File = "sample.dfu",
                        Device = "0x5678",
                        Product = "0x1234",
                        Vendor = "0x0483"
                    })
            };
    }
}
