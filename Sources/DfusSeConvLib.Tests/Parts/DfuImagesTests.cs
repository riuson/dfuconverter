﻿using Castle.Components.DictionaryAdapter;
using DfuSeConvLib.Interfaces;
using DfuSeConvLib.Serialization;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace DfuSeConvLib.Tests.Parts {
    [TestFixture]
    public class DfuImagesTests {
        [Test]
        public void CanWrite() {
            var image1Mock = new Mock<IDfuImage>();
            var image2Mock = new Mock<IDfuImage>();

            var dfuImagesMock = new Mock<IDfuImages>();
            dfuImagesMock.SetupGet(x => x.Images)
                .Returns(new EditableList<IDfuImage> { image1Mock.Object, image2Mock.Object });

            byte b = 0;

            var dfuSerializerMock = new Mock<ISerializer>();
            dfuSerializerMock.Setup(x => x.Write(It.IsAny<Stream>())).Callback<Stream>(x => {
                var array = new[] { b++, b++, b++, b++ };
                x.Write(array, 0, 4);
            });

            var sut = new DfuImagesSerializer(
                dfuImagesMock.Object,
                x => dfuSerializerMock.Object);

            var tempStream = new MemoryStream();
            sut.Write(tempStream);

            var actual = tempStream.ToArray();
            var expected = Enumerable.Range(0, 8).Select(x => (byte) x).ToArray();

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
