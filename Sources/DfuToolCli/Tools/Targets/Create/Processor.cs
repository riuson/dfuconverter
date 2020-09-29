﻿using DfuConvLib.Interfaces;
using DfuToolCli.Helpers;
using DfuToolCli.Interfaces;
using System;
using System.IO;
using System.Linq;

namespace DfuToolCli.Tools.Targets.Create {
    internal class Processor : IVerbProcessor {
        private readonly Func<IDfuDeserializer> _createDfuDeserializer;
        private readonly Func<IDfuImage> _createDfuImage;
        private readonly Func<IDfuSerializer> _createDfuSerializer;
        private readonly Func<ITargetPrefix> _createTargetPrefix;

        public Processor(
            Func<IDfuSerializer> createDfuSerializer,
            Func<IDfuDeserializer> createDfuDeserializer,
            Func<IDfuImage> createDfuImage,
            Func<ITargetPrefix> createTargetPrefix) {
            this._createDfuSerializer = createDfuSerializer;
            this._createDfuDeserializer = createDfuDeserializer;
            this._createDfuImage = createDfuImage;
            this._createTargetPrefix = createTargetPrefix;
        }

        public void Process(IVerbOptions obj) {
            var options = obj as Options;

            var targetId = string.IsNullOrEmpty(options.Id) ? -1 : options.Id.ToInt32(0, 255);

            var dfuSerializer = this._createDfuSerializer();
            var dfuDeserializer = this._createDfuDeserializer();

            using (var stream = new FileStream(options.File, FileMode.Open, FileAccess.ReadWrite)) {
                var dfu = dfuDeserializer.Read(stream);

                var targetPrefix = this._createTargetPrefix();

                if (string.IsNullOrEmpty(options.Name)) {
                    targetPrefix.TargetName = string.Empty;
                    targetPrefix.TargetNamed = false;
                } else {
                    targetPrefix.TargetName = options.Name;
                    targetPrefix.TargetNamed = true;
                }

                var usedIds = dfu.Images.Images
                    .Select(x => x.Prefix.AlternateSetting)
                    .OrderBy(x => x)
                    .ToArray();

                var allIds = Enumerable.Range(0, 256)
                    .ToArray();

                var availableIds = allIds
                    .Except(usedIds)
                    .OrderBy(x => x)
                    .ToArray();

                if (availableIds.Length == 0) {
                    throw new ArgumentException("There is no available Target IDs in this DFU.");
                }

                if (targetId == -1) {
                    targetId = availableIds.First();
                } else {
                    if (!availableIds.Contains(targetId)) {
                        throw new ArgumentException("Specified Target ID is exists already in this DFU.");
                    }
                }

                targetPrefix.AlternateSetting = targetId;

                var dfuImage = this._createDfuImage();
                dfuImage.Prefix = targetPrefix;

                dfu.Images.Images.Add(dfuImage);


                stream.Seek(0, SeekOrigin.Begin);
                stream.SetLength(0);

                dfuSerializer.Write(stream, dfu);
            }
        }
    }
}