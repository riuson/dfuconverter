﻿using DfuSeConvLib.Interfaces;

namespace DfuSeConvLib.Parts {
    public class Dfu : IDfu {
        public IDfuPrefix Prefix { get; set; }
        public IDfuImages Images { get; set; }
        public IDfuSuffix Suffix { get; set; }
    }
}