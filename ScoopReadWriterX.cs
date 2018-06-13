using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
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

        public Boolean OpenR(bool directIO)
        {
            try
            {
                if (directIO)
                {
                    _fs = new FileStream(_FileName, FileMode.Open, FileAccess.Read, FileShare.Read, 1048576, FileFlagNoBuffering);
                }
                else
                {
                    _fs = new FileStream(_FileName, FileMode.Open, FileAccess.Read, FileShare.Read, 1048576, FileOptions.SequentialScan);
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message, "Error Opening File", MessageBoxButtons.OK);
                if (_fs != null) _fs.Close();
                return false;
            }
            _lPosition = 0;
            _bOpen = true;
            return true;
        }

        public Boolean OpenW(bool directIO)
        {
            try
            {
                //assert priviliges
                if (!Privileges.HasAdminPrivileges)
                {
                    DialogResult dialogResult = MessageBox.Show("No elevated file creation possible. File creation will be very slow. Continue?", "Freeze Warning", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.No)
                    {
                        Application.Exit();
                    }
                }
                if (directIO)
                {
                    _fs = new FileStream(_FileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None, 1048576, FileFlagNoBuffering);
                }
                else
                {
                    _fs = new FileStream(_FileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None, 1048576, FileOptions.WriteThrough);
                }
            }catch (Exception e)
            {
                //MessageBox.Show(e.Message, "Error Opening File", MessageBoxButtons.OK);
                if (_fs != null) _fs.Close();
                return false;
            }
            _lPosition = 0;
            _lLength = _fs.Length;
            _bOpen = true;
            return true;
        }

        public Boolean isOpen()
        {
            return _bOpen;
        }

        public Boolean ReadScoop(int scoop, long totalNonces, long startNonce, Scoop target, int limit)
        {
            _lPosition = scoop * (64 * totalNonces) + startNonce * 64;
            try {
                _fs.Seek(_lPosition, SeekOrigin.Begin);
            }
            catch (Exception e)
            {
                MessageBox.Show("Scoop: " + scoop.ToString() + " " + e.Message, "I/O Error - Seek to read", MessageBoxButtons.OK);
                return false;
            }
            try {
                //interrupt avoider 1mb read 64*16384
                for (int i = 0; i < limit * 64; i += (64 * 16384))
                    _fs.Read(target.byteArrayField, i, Math.Min(64 * 16384, limit * 64 - i));
            }
            catch (Exception e)
            {
                MessageBox.Show("Scoop: " + scoop.ToString() + " " + e.Message, "I/O Error - Read", MessageBoxButtons.OK);
                return false;
            }
            _lPosition += limit * 64;
            return true;
        }

        public Boolean WriteScoop(int scoop, long totalNonces, long startNonce, Scoop source, int limit)
        {
            _lPosition = scoop * (64 * totalNonces) + startNonce * 64;
            try
            {
                _fs.Seek(_lPosition, SeekOrigin.Begin);
            }
            catch (Exception e)
            {
                MessageBox.Show("Scoop: " + scoop.ToString() + " " + e.Message, "I/O Error - Seek to write", MessageBoxButtons.OK);
                return false;
            }
            try
            {
                //interrupt avoider 1mb read 64*16384
                for (int i = 0; i < limit * 64; i += (64 * 16384))
                    _fs.Write(source.byteArrayField, i, Math.Min(64 * 16384, limit * 64 - i));
            }
            catch (Exception e)
            {
                MessageBox.Show("Scoop: " + scoop.ToString() + " " + e.Message, "I/O Error - Write", MessageBoxButtons.OK);
                return false;
            }
            _lPosition += limit * 64;
            return true;
        }

        public Boolean PreAlloc(long totalNonces)
        {
            try
            {
                _fs.SetLength(totalNonces * (2 << 17));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Preallocating File", MessageBoxButtons.OK);
                return false;
            }
            bool test = SetFileValidData(_fs.SafeFileHandle, totalNonces * (2 << 17));
            if (!test)
            {
                DialogResult dialogResult = MessageBox.Show("Elevated file creation failed. File creation will be very slow. Continue?", "Freeze Warning", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    return false;
                }
            }
            return true;
        }
    }
  


}
