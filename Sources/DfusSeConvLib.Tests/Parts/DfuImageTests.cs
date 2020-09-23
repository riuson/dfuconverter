﻿using Castle.Components.DictionaryAdapter;
using DfuSeConvLib.Interfaces;
using DfuSeConvLib.Serialization;
using Moq;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace DfuSeConvLib.Tests.Parts {
    [TestFixture]
    public class DfuImageTests {
        [Test]
        public void CanWrite() {
            var sampleData1 = new byte[] { 1, 2, 3, 4 };
            var sampleData2 = new byte[] { 5, 6, 7, 8 };

            var targetPrefixMock = new Mock<ITargetPrefix>();
            //targetPrefixMock.Setup(x => x.Write(It.IsAny<Stream>(), It.IsAny<IDfuImage>()))
            //    .Callback<Stream, IDfuImage>((s, i) => i.Write(s));
            targetPrefixMock.SetupGet(x => x.Signature).Returns("Target");
            targetPrefixMock.SetupGet(x => x.AlternateSetting).Returns(7);
            targetPrefixMock.SetupGet(x => x.TargetNamed).Returns(true);
            targetPrefixMock.SetupGet(x => x.TargetName).Returns(
                string.Join("", Enumerable.Range(0, 50).Select(_ => "SAMPLE")));

            var imageElement1Mock = new Mock<IImageElement>();
            imageElement1Mock.SetupGet(x => x.ElementAddress).Returns(0x08000000);
            imageElement1Mock.SetupGet(x => x.Data).Returns(sampleData1);
            imageElement1Mock.SetupGet(x => x.ElementSize).Returns(Convert.ToUInt32(sampleData1.Length));

            var imageElement2Mock = new Mock<IImageElement>();
            imageElement2Mock.SetupGet(x => x.ElementAddress).Returns(0x08000100);
            imageElement2Mock.SetupGet(x => x.Data).Returns(sampleData2);
            imageElement2Mock.SetupGet(x => x.ElementSize).Returns(Convert.ToUInt32(sampleData2.Length));

            var dfuimageMock = new Mock<IDfuImage>();
            dfuimageMock.SetupGet(x => x.Prefix).Returns(targetPrefixMock.Object);
            dfuimageMock.SetupGet(x => x.ImageElements)
                .Returns(new EditableList<IImageElement> { imageElement1Mock.Object, imageElement2Mock.Object });


            // Array generated by DFU File Manager v3.0.6
            var expected = new byte[] {
                0x54, 0x61, 0x72, 0x67, 0x65, 0x74,
                0x07,
                0x01, 0x00, 0x00, 0x00,
                0x53, 0x41, 0x4D, 0x50, 0x4C, 0x45, 0x53, 0x41, 0x4D, 0x50, 0x4C, 0x45, 0x53, 0x41, 0x4D, 0x50, 0x4C,
                0x45, 0x53, 0x41, 0x4D, 0x50, 0x4C, 0x45, 0x53, 0x41, 0x4D, 0x50, 0x4C, 0x45, 0x53, 0x41, 0x4D, 0x50,
                0x4C, 0x45, 0x53, 0x41, 0x4D, 0x50, 0x4C, 0x45, 0x53, 0x41, 0x4D, 0x50, 0x4C, 0x45, 0x53, 0x41, 0x4D,
                0x50, 0x4C, 0x45, 0x53, 0x41, 0x4D, 0x50, 0x4C, 0x45, 0x53, 0x41, 0x4D, 0x50, 0x4C, 0x45, 0x53, 0x41,
                0x4D, 0x50, 0x4C, 0x45, 0x53, 0x41, 0x4D, 0x50, 0x4C, 0x45, 0x53, 0x41, 0x4D, 0x50, 0x4C, 0x45, 0x53,
                0x41, 0x4D, 0x50, 0x4C, 0x45, 0x53, 0x41, 0x4D, 0x50, 0x4C, 0x45, 0x53, 0x41, 0x4D, 0x50, 0x4C, 0x45,
                0x53, 0x41, 0x4D, 0x50, 0x4C, 0x45, 0x53, 0x41, 0x4D, 0x50, 0x4C, 0x45, 0x53, 0x41, 0x4D, 0x50, 0x4C,
                0x45, 0x53, 0x41, 0x4D, 0x50, 0x4C, 0x45, 0x53, 0x41, 0x4D, 0x50, 0x4C, 0x45, 0x53, 0x41, 0x4D, 0x50,
                0x4C, 0x45, 0x53, 0x41, 0x4D, 0x50, 0x4C, 0x45, 0x53, 0x41, 0x4D, 0x50, 0x4C, 0x45, 0x53, 0x41, 0x4D,
                0x50, 0x4C, 0x45, 0x53, 0x41, 0x4D, 0x50, 0x4C, 0x45, 0x53, 0x41, 0x4D, 0x50, 0x4C, 0x45, 0x53, 0x41,
                0x4D, 0x50, 0x4C, 0x45, 0x53, 0x41, 0x4D, 0x50, 0x4C, 0x45, 0x53, 0x41, 0x4D, 0x50, 0x4C, 0x45, 0x53,
                0x41, 0x4D, 0x50, 0x4C, 0x45, 0x53, 0x41, 0x4D, 0x50, 0x4C, 0x45, 0x53, 0x41, 0x4D, 0x50, 0x4C, 0x45,
                0x53, 0x41, 0x4D, 0x50, 0x4C, 0x45, 0x53, 0x41, 0x4D, 0x50, 0x4C, 0x45, 0x53, 0x41, 0x4D, 0x50, 0x4C,
                0x45, 0x53, 0x41, 0x4D, 0x50, 0x4C, 0x45, 0x53, 0x41, 0x4D, 0x50, 0x4C, 0x45, 0x53, 0x41, 0x4D, 0x50,
                0x4C, 0x45, 0x53, 0x41, 0x4D, 0x50, 0x4C, 0x45, 0x53, 0x41, 0x4D, 0x50, 0x4C, 0x45, 0x53, 0x41, 0x4D,
                0x18, 0x00, 0x00, 0x00,
                0x02, 0x00, 0x00, 0x00,

                0x00, 0x00, 0x00, 0x08,
                0x04, 0x00, 0x00, 0x00,
                0x01, 0x02, 0x03, 0x04,

                0x00, 0x01, 0x00, 0x08,
                0x04, 0x00, 0x00, 0x00,
                0x05, 0x06, 0x07, 0x08
            };

            var tempStream = new MemoryStream();

            var sut = new DfuImageSerializer(
                dfuimageMock.Object,
                (x, y) => new TargetPrefixSerializer(x, y),
                x => new ImageElementSerializer(x));
            sut.Write(tempStream);

            var actual = tempStream.ToArray();

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
