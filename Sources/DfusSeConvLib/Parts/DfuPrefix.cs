﻿using DfuSeConvLib.Interfaces;

namespace DfuSeConvLib.Parts {
    public class DfuPrefix : IDfuPrefix {
        private static readonly string StandardSignature = "DfuSe";
        private static readonly int StandardVersion = 1;

        public string Signature { get; set; } = StandardSignature;
        public int Version { get; set; } = StandardVersion;
        public uint DfuImageSize { get; set; } = 0;
        public int Targets { get; set; } = 0;
    }
}