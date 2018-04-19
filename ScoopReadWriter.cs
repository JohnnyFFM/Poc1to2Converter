using System;
using System.IO;
namespace poc1poc2Conv
{
    /// <summary>
    /// Summary description for ScoopReadWriter.
    /// </summary>
    public class ScoopReadWriter
	{
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
}
