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
    public class ScoopReadWriter
	{
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetFileValidData(SafeFileHandle hFile, long ValidDataLength);
        private BinaryReader _br; 
		protected string _sFileName;
        protected string _tFileName;
        private long _lLength = -1;
		protected bool _bOpen = false;
        protected FileStream _fs;
        protected FileStream _fs2;
        bool _inline;
        private long _lPosition = 0;

        //inline constructor
        public ScoopReadWriter(string stFileName)
		{
			_sFileName = stFileName;
            _tFileName = stFileName;
            _inline = true;
        }

        //separate file constructor
        public ScoopReadWriter(string sFileName, string tFileName)
        {
            _sFileName = sFileName;
            _tFileName = tFileName;
            _inline = false;
        }

        public void PreAlloc(long totalNonces)
        {
            _fs2.SetLength(totalNonces * (2 << 17));
            bool test = SetFileValidData(_fs2.SafeFileHandle, totalNonces * (2 << 17));
            if (!test)
            {
                DialogResult dialogResult = MessageBox.Show("Elevated file creation failed. File creation will be very slow. Continue?", "Freeze Warning", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }
        }

        public void Close()
		{
			_bOpen = false;
			if (!_inline) _br.Close();
            _fs2.Close();
			_br = null;
		}

		public bool EOF
		{
			get
			{	
					return (!_bOpen || (_lPosition >= _lLength));
			}
		}

        public void Open()
        {
            if (_inline)
            {
                _fs2 = new FileStream(_sFileName, FileMode.Open, FileAccess.ReadWrite, FileShare.None,1048576,FileOptions.SequentialScan|FileOptions.WriteThrough);
                _br = new BinaryReader(_fs2);
            }
            else
            {
                if (!Privileges.HasAdminPrivileges)
                {
                    DialogResult dialogResult = MessageBox.Show("No elevated file creation possible. File creation will be very slow. Continue?", "Freeze Warning", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.No)
                    {
                        Application.Exit();
                    }
                }
               _fs = new FileStream(_sFileName, FileMode.Open, FileAccess.Read, FileShare.Read, 1048576, FileOptions.SequentialScan);
                _fs2 = new FileStream(_tFileName, FileMode.Create, FileAccess.Write, FileShare.None, 1048576, FileOptions.WriteThrough);
                _br = new BinaryReader(_fs);
            }
            _lLength = _br.BaseStream.Length;
            _lPosition = 0;
            _bOpen = true;
        }

        public void ReadScoop(int scoop, long totalNonces, long startNonce, Scoop target, int limit)
		{
            _lPosition = scoop * (64 * totalNonces) + startNonce * 64;
            _br.BaseStream.Seek(_lPosition, SeekOrigin.Begin);
            _br.BaseStream.Read(target.byteArrayField, 0, limit * 64);
            //target.byteArrayField = _br.ReadBytes(limit * 64);
            _lPosition += limit * 64;
        }

        public void WriteScoop(int scoop, long totalNonces, long startNonce, Scoop source, int limit)
        {
            _lPosition = scoop * (64 * totalNonces) + startNonce * 64;
            _fs2.Seek(_lPosition, SeekOrigin.Begin);
            _fs2.Write(source.byteArrayField, 0, limit * 64);
            _lPosition += limit * 64;
        }
	}
    public static class Privileges
    {
        private static int asserted = 0;
        private static bool hasBackupPrivileges = false;

        public static bool HasAdminPrivileges
        {
            get { return AssertPriveleges(); }
        }

        private static bool AssertPriveleges()
        {
            bool success = false;
            var wasAsserted = Interlocked.CompareExchange(ref asserted, 1, 0);
            if (wasAsserted == 0)
            {

                success = AssertPrivelege(NativeMethods.SE_MANAGE_VOLUME_NAME);

                hasBackupPrivileges = success;

            }
            return hasBackupPrivileges;
        }

        private static bool AssertPrivelege(string privelege)
        {
            IntPtr token;
            var tokenPrivileges = new NativeMethods.TOKEN_PRIVILEGES();
            tokenPrivileges.Privileges = new NativeMethods.LUID_AND_ATTRIBUTES[1];

            var success =
              NativeMethods.OpenProcessToken(NativeMethods.GetCurrentProcess(), NativeMethods.TOKEN_ADJUST_PRIVILEGES, out token)
              &&
              NativeMethods.LookupPrivilegeValue(null, privelege, out tokenPrivileges.Privileges[0].Luid);

            try
            {
                if (success)
                {
                    tokenPrivileges.PrivilegeCount = 1;
                    tokenPrivileges.Privileges[0].Attributes = NativeMethods.SE_PRIVILEGE_ENABLED;
                    success =
                      NativeMethods.AdjustTokenPrivileges(token, false, ref tokenPrivileges, Marshal.SizeOf(tokenPrivileges), IntPtr.Zero, IntPtr.Zero)
                      &&
                      (Marshal.GetLastWin32Error() == 0);
                }

                if (!success)
                {
                    Console.WriteLine("Could not assert privilege: " + privelege);
                }
            }
            finally
            {
                NativeMethods.CloseHandle(token);
            }

            return success;
        }
    }

    internal class NativeMethods
    {
        internal const int ERROR_HANDLE_EOF = 38;

        //--> Privilege constants....
        internal const UInt32 SE_PRIVILEGE_ENABLED = 0x00000002;
        internal const string SE_MANAGE_VOLUME_NAME = "SeManageVolumePrivilege";

        //--> For starting a process in session 1 from session 0...
        internal const uint TOKEN_ADJUST_PRIVILEGES = 0x0020;

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool OpenProcessToken(IntPtr ProcessHandle, UInt32 DesiredAccess, out IntPtr TokenHandle);
        [DllImport("kernel32.dll")]
        internal static extern IntPtr GetCurrentProcess();
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool LookupPrivilegeValue(string lpSystemName, string lpName, out LUID lpLuid);
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool AdjustTokenPrivileges(IntPtr TokenHandle, [MarshalAs(UnmanagedType.Bool)]bool DisableAllPrivileges, ref TOKEN_PRIVILEGES NewState, Int32 BufferLength, IntPtr PreviousState, IntPtr ReturnLength);
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CloseHandle(IntPtr hObject);

        [StructLayout(LayoutKind.Sequential)]
        internal struct LUID
        {
            public UInt32 LowPart;
            public Int32 HighPart;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct LUID_AND_ATTRIBUTES
        {
            public LUID Luid;
            public UInt32 Attributes;
        }

        internal struct TOKEN_PRIVILEGES
        {
            public UInt32 PrivilegeCount;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public LUID_AND_ATTRIBUTES[] Privileges;
        }
    }
}
