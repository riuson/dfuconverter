﻿using DfuLib.Helpers;
using DfuToolCli.Tests.Helpers;
using DfuToolCli.Tools.Targets.Clear;
using NUnit.Framework;
using System;
using System.IO;

namespace DfuToolCli.Tests.Tools.Targets.Clear {
    [TestFixture]
    public class ProcessorTests : BaseTestFixture {
        [Test]
        public void CanTargetClear() {
            // Sample.
            var sample = new byte[] {
                0x44, 0x66, 0x75, 0x53, 0x65,
                0x01,
                0x00, 0x00, 0x00, 0x00,
                0x02,

                0x54, 0x61, 0x72, 0x67, 0x65, 0x74,
                0x01,
                0x01, 0x00, 0x00, 0x00,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x70, 0x00, 0x00, 0x00,
                0x02, 0x00, 0x00, 0x00,

                0x00, 0x00, 0x00, 0x08,
                0x30, 0x00, 0x00, 0x00,
                0x12, 0x13, 0x13, 0x21, 0x34, 0x56, 0x46, 0x46,
                0x46, 0x46, 0x04, 0x86, 0x46, 0x40, 0x64, 0x06,
                0x46, 0x30, 0x48, 0x47, 0x09, 0x87, 0x98, 0x79,
                0x86, 0x78, 0x69, 0x78, 0x69, 0x78, 0x69, 0x78,
                0x97, 0x89, 0x07, 0x98, 0x60, 0x76, 0x97, 0x69,
                0x87, 0x98, 0x79, 0x78, 0x97, 0x98, 0x79, 0x80,

                0x00, 0x00, 0x00, 0x18,
                0x30, 0x00, 0x00, 0x00,
                0x12, 0x13, 0x13, 0x21, 0x34, 0x56, 0x46, 0x46,
                0x46, 0x46, 0x04, 0x86, 0x46, 0x40, 0x64, 0x06,
                0x46, 0x30, 0x48, 0x47, 0x09, 0x87, 0x98, 0x79,
                0x86, 0x78, 0x69, 0x78, 0x69, 0x78, 0x69, 0x78,
                0x97, 0x89, 0x07, 0x98, 0x60, 0x76, 0x97, 0x69,
                0x87, 0x98, 0x79, 0x78, 0x97, 0x98, 0x79, 0x80,

                0x54, 0x61, 0x72, 0x67, 0x65, 0x74,
                0x00,
                0x01, 0x00, 0x00, 0x00,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x70, 0x00, 0x00, 0x00,
                0x02, 0x00, 0x00, 0x00,

                0x00, 0x00, 0x00, 0x08,
                0x30, 0x00, 0x00, 0x00,
                0x12, 0x13, 0x13, 0x21, 0x34, 0x56, 0x46, 0x46,
                0x46, 0x46, 0x04, 0x86, 0x46, 0x40, 0x64, 0x06,
                0x46, 0x30, 0x48, 0x47, 0x09, 0x87, 0x98, 0x79,
                0x86, 0x78, 0x69, 0x78, 0x69, 0x78, 0x69, 0x78,
                0x97, 0x89, 0x07, 0x98, 0x60, 0x76, 0x97, 0x69,
                0x87, 0x98, 0x79, 0x78, 0x97, 0x98, 0x79, 0x80,

                0x00, 0x00, 0x00, 0x18,
                0x30, 0x00, 0x00, 0x00,
                0x12, 0x13, 0x13, 0x21, 0x34, 0x56, 0x46, 0x46,
                0x46, 0x46, 0x04, 0x86, 0x46, 0x40, 0x64, 0x06,
                0x46, 0x30, 0x48, 0x47, 0x09, 0x87, 0x98, 0x79,
                0x86, 0x78, 0x69, 0x78, 0x69, 0x78, 0x69, 0x78,
                0x97, 0x89, 0x07, 0x98, 0x60, 0x76, 0x97, 0x69,
                0x87, 0x98, 0x79, 0x78, 0x97, 0x98, 0x79, 0x80,

                0x78, 0x56,
                0x34, 0x12,
                0x83, 0x04,
                0x1A, 0x01,
                0x55, 0x46, 0x44,
                0x10,
                0x00, 0x00, 0x00, 0x00
            };

            var length = sample.Length - 16;
            Array.Copy(
                BitConverter.GetBytes(length),
                0,
                sample,
                6,
                4);

            var crc32 = Crc32.ComputeAll(sample, 0, Convert.ToUInt32(sample.Length - 4));
            Array.Copy(
                BitConverter.GetBytes(crc32 ^ 0xffffffffu),
                0,
                sample,
                sample.Length - 4,
                4);

            // Expected.
            var expected = new byte[] {
                0x44, 0x66, 0x75, 0x53, 0x65,
                0x01,
                0x00, 0x00, 0x00, 0x00,
                0x02,

                0x54, 0x61, 0x72, 0x67, 0x65, 0x74,
                0x01,
                0x01, 0x00, 0x00, 0x00,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x70, 0x00, 0x00, 0x00,
                0x02, 0x00, 0x00, 0x00,

                0x00, 0x00, 0x00, 0x08,
                0x30, 0x00, 0x00, 0x00,
                0x12, 0x13, 0x13, 0x21, 0x34, 0x56, 0x46, 0x46,
                0x46, 0x46, 0x04, 0x86, 0x46, 0x40, 0x64, 0x06,
                0x46, 0x30, 0x48, 0x47, 0x09, 0x87, 0x98, 0x79,
                0x86, 0x78, 0x69, 0x78, 0x69, 0x78, 0x69, 0x78,
                0x97, 0x89, 0x07, 0x98, 0x60, 0x76, 0x97, 0x69,
                0x87, 0x98, 0x79, 0x78, 0x97, 0x98, 0x79, 0x80,

                0x00, 0x00, 0x00, 0x18,
                0x30, 0x00, 0x00, 0x00,
                0x12, 0x13, 0x13, 0x21, 0x34, 0x56, 0x46, 0x46,
                0x46, 0x46, 0x04, 0x86, 0x46, 0x40, 0x64, 0x06,
                0x46, 0x30, 0x48, 0x47, 0x09, 0x87, 0x98, 0x79,
                0x86, 0x78, 0x69, 0x78, 0x69, 0x78, 0x69, 0x78,
                0x97, 0x89, 0x07, 0x98, 0x60, 0x76, 0x97, 0x69,
                0x87, 0x98, 0x79, 0x78, 0x97, 0x98, 0x79, 0x80,

                0x54, 0x61, 0x72, 0x67, 0x65, 0x74,
                0x00,
                0x01, 0x00, 0x00, 0x00,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x61, 0x61, 0x61, 0x61, 0x61, 0x61, 0x61,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,

                0x78, 0x56,
                0x34, 0x12,
                0x83, 0x04,
                0x1A, 0x01,
                0x55, 0x46, 0x44,
                0x10,
                0x00, 0x00, 0x00, 0x00
            };

            length = expected.Length - 16;
            expected.WriteInteger(6, Convert.ToUInt32(length));

            crc32 = Crc32.ComputeAll(expected, 0, Convert.ToUInt32(expected.Length - 4));
            expected.WriteInteger(expected.Length - 4, crc32 ^ 0xffffffffu);

            // Test.
            var sut = this.Resolve<Processor>();

            using (var stream = new MemoryStream()) {
                stream.Write(sample, 0, sample.Length);
                stream.Seek(0, SeekOrigin.Begin);

                sut.ProcessInternal(stream, 0);

                var actual = stream.ToArray();

                Assert.That(actual, Is.EqualTo(expected));
            }
        }
    }
}