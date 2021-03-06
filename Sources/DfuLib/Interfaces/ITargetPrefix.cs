﻿namespace DfuLib.Interfaces {
    /// <summary>
    /// The target prefix record is used to describe the associated image. The Target Prefix buffer is
    /// represented in Big Endian order.
    /// </summary>
    public interface ITargetPrefix {
        /// <summary>
        /// The Signature field, 6-byte coded, fixed to “Target”.
        /// </summary>
        string Signature { get; set; }

        /// <summary>
        /// The TargetId (AlternateSetting) field gives the device alternate setting for which the associated
        /// image can be used.
        /// </summary>
        int TargetId { get; set; }

        /// <summary>
        /// The IsTargetNamed field is a boolean value which indicates if the target is named or
        /// not.
        /// </summary>
        bool IsTargetNamed { get; set; }

        /// <summary>
        /// The TargetName field gives the target name.
        /// </summary>
        string TargetName { get; set; }

        /// <summary>
        /// The TargetSize field gives the whole length of the associated image excluding the
        /// Target prefix.
        /// </summary>
        uint TargetSize { get; set; }

        /// <summary>
        /// The NbElements field gives the number of elements in the associated image.
        /// </summary>
        uint NbElements { get; set; }
    }
}
