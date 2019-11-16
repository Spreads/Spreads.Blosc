// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

// ReSharper disable All

namespace Spreads.Native.Bootstrap
{
    /// <summary>Contains information about @Yeppp library version.</summary>
    internal struct Version
    {
        internal Version(uint major, uint minor, uint patch, uint build, string releaseName)
        {
            this.major = major;
            this.minor = minor;
            this.patch = patch;
            this.build = build;
            this.releaseName = releaseName;
        }

        private readonly uint major;
        private readonly uint minor;
        private readonly uint patch;
        private readonly uint build;
        private readonly string releaseName;

        /// <summary>The major version number of Yeppp! library.</summary>
        /// <remarks>Library releases with the same major versions are guaranteed to be API- and ABI-compatible.</remarks>
        public uint Major
        {
            get
            {
                return this.major;
            }
        }

        /// <summary>The minor version number of Yeppp! library.</summary>
        /// <remarks>A change in minor versions indicates addition of new features, and major bug-fixes.</remarks>
        public uint Minor
        {
            get
            {
                return this.minor;
            }
        }

        /// <summary>The patch level of Yeppp! library.</summary>
        /// <remarks>A version with a higher patch level indicates minor bug-fixes.</remarks>
        public uint Patch
        {
            get
            {
                return this.patch;
            }
        }

        /// <summary>The build number of Yeppp! library.</summary>
        /// <remarks>The build number is unique for the fixed combination of major, minor, and patch-level versions.</remarks>
        public uint Build
        {
            get
            {
                return this.build;
            }
        }

        /// <summary>Human-readable name of this release of Yeppp! library</summary>
        /// <remarks>The release name may contain non-ASCII characters.</remarks>
        public string ReleaseName
        {
            get
            {
                return this.releaseName;
            }
        }

        /// <summary>Provides a string representation for all parts of the version.</summary>
        /// <returns>The full version string in the format "major.minor.patch.build (release name)".</returns>
        public override string ToString()
        {
            return this.major.ToString() + "." + this.minor.ToString() + "." + this.patch.ToString() + "." + this.build.ToString() + " (" + this.releaseName + ")";
        }
    }
}