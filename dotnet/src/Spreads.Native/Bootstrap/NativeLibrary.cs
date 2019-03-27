// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Spreads.Native.Bootstrap
{
    public class NativeLibrary : IDisposable
    {
        internal string _path;
        internal IntPtr _handle;
        internal readonly INativeLibraryLoader _loader;

        internal NativeLibrary(string path, INativeLibraryLoader loader, IntPtr handle = default)
        {
            _path = path;
            _loader = loader;
            if (handle != default)
            {
                _handle = loader.LoadLibrary(path);
            }
            else
            {
                _handle = handle;
            }

            if (_handle == IntPtr.Zero)
            {
                var error = loader.LastError();
                Trace.TraceError("NativeLibrary: handle == IntPtr.Zero, lastError: " + (long)error);
                throw new DllNotFoundException(path);
            }
        }

        public string Path => _path;
        public IntPtr Handle => _handle;

        [Obsolete("Use generic overload instead")]
        public Delegate GetFunction(string name, Type type)
        {
            var function = _loader.FindFunction(_handle, name);
            if (function == IntPtr.Zero)
                if (function == IntPtr.Zero)
                {
                    throw new EntryPointNotFoundException(name);
                }
            return Marshal.GetDelegateForFunctionPointer(function, type);
        }

        public TDelegate GetFunction<TDelegate>(string name)
        {
            IntPtr function = _loader.FindFunction(_handle, name);
            if (function == IntPtr.Zero)
            {
                throw new EntryPointNotFoundException(name);
            }
            return Marshal.GetDelegateForFunctionPointer<TDelegate>(function);
        }

        public IntPtr GetFunctionPtr(string name)
        {
            IntPtr function = _loader.FindFunction(_handle, name);
            if (function == IntPtr.Zero)
            {
                throw new EntryPointNotFoundException(name);
            }

            return function;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_handle != IntPtr.Zero)
            {
                _loader.UnloadLibrary(_handle);
                _handle = IntPtr.Zero;
            }
            if (_path != null)
            {
                try
                {
                    System.IO.File.Delete(_path);
                }
                catch
                {
                }
                _path = null;
            }
        }

        ~NativeLibrary()
        {
            Dispose(false);
        }
    }
}
