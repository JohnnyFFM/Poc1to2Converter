using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace poc1poc2Conv
{
    /// <summary>
    /// Summary description for ScoopReadWriter.
    /// </summary>
    public class ScoopReadWriterX
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetFileValidData(SafeFileHandle hFile, long ValidDataLength);
        protected string _FileName;
        private long _lLength = -1;
        protected bool _bOpen = false;
        protected FileStream _fs;
        private long _lPosition = 0;
        FileOptions FileFlagNoBuffering = (FileOptions)0x20000000;

        //inline constructor
        public ScoopReadWriterX(string stFileName)
        {
            _FileName = stFileName;
        }

        public void Close()
        {
            _bOpen = false;
            _fs.Close();
        }

        public void OpenR()
        {
            _fs = new FileStream(_FileName, FileMode.Open, FileAccess.Read, FileShare.Read, 1048576, FileFlagNoBuffering);
            _lPosition = 0;
            _bOpen = true;
        }

        public void OpenW()
        {
            //assert priviliges
            if (!Privileges.HasAdminPrivileges) Console.WriteLine("INFO: Missing Priviledge, File creation will take a while...");
            _fs = new FileStream(_FileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None, 1048576, FileFlagNoBuffering);
            _lPosition = 0;
            _lLength = _fs.Length;
            _bOpen = true;
        }

        public void ReadScoop(int scoop, long totalNonces, long startNonce, Scoop target, int limit)
        {
            _lPosition = scoop * (64 * totalNonces) + startNonce * 64;
            _fs.Seek(_lPosition, SeekOrigin.Begin);
            _fs.Read(target.byteArrayField, 0, limit * 64);
            _lPosition += limit * 64;
        }

        public void WriteScoop(int scoop, long totalNonces, long startNonce, Scoop source, int limit)
        {
            _lPosition = scoop * (64 * totalNonces) + startNonce * 64;
            _fs.Seek(_lPosition, SeekOrigin.Begin);
            _fs.Write(source.byteArrayField, 0, limit * 64);
            _lPosition += limit * 64;
        }

        public void PreAlloc(long totalNonces)
        {
            _fs.SetLength(totalNonces * (2 << 17));
            bool test = SetFileValidData(_fs.SafeFileHandle, totalNonces * (2 << 17));
            if (!test) Console.WriteLine("INFO: Quick File creation failed. File creation will take a while...");
        }
    }
  


}
