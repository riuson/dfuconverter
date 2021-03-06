﻿using System.Collections.Generic;
using System.IO;

namespace DfuLib.Interfaces {
    public interface ITargetPrefixSerializer {
        uint GetSize();

        void Write(
            Stream stream,
            ITargetPrefix targetPrefix,
            IEnumerable<IImageElement> imageElements);
    }
}
