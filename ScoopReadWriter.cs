using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Runtime.InteropServices;
namespace poc1poc2Conv
{
	/// <summary>
	/// Summary description for CBRFieldTestStructReader.
	/// </summary>
	public class ScoopReadWriter
	{
		private BinaryReader _br; 
		protected Scoop _data;
		protected string _sFileName;
        protected string _tFileName;
        private long _lLength = -1;
		protected bool _bOpen = false;
        protected FileStream _fs;
        protected FileStream _fs2;
        bool _inline;
        private long _lPosition = 0;
		private bool _bUseCachedValuesForEOF = false;

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

		public Scoop Data
		{
			get
			{
				return _data;
			}
		}

		public bool EOF
		{
			get
			{	
				if(_bUseCachedValuesForEOF)
				{
					return (!_bOpen || (_lPosition >= _lLength));
				}
				else
				{
					return (!_bOpen || (_br.BaseStream.Position >= _br.BaseStream.Length));
				}
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
                _fs = new FileStream(_sFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                _fs2 = new FileStream(_tFileName, FileMode.Create, FileAccess.Write, FileShare.None);
                _br = new BinaryReader(_fs);
            }
            _lLength = _br.BaseStream.Length;
            _lPosition = 0;
            _bOpen = true;
        }

        public Scoop Read(int noncelimit)
		{
				_data = Scoop.FromBinaryReaderField(_br, noncelimit);
				_lPosition += _data.Size();
            return _data;
		}

        public void Write(Scoop data)
        {
            if (_fs2 == null)
                throw new Exception("Cannot call write on closed ScoopWriter");
            byte[] buff = data.ToByteArray();
            _fs2.Write(buff, 0, buff.Length);
            _lPosition += _data.Size();
        }

        public void setReadPosition(int scoop, long nonces, long start)
        {
            _lPosition = scoop * (64 * nonces) + start * 64;
            _br.BaseStream.Seek(_lPosition, SeekOrigin.Begin);
        }

        public void setWritePosition(int scoop, long nonces, long start)
        {
            _lPosition = scoop * (64 * nonces) + start * 64;
            _fs2.Seek(_lPosition, SeekOrigin.Begin);
        }
        public bool UseCachedValuesForEOF
		{
			get
			{
				return _bUseCachedValuesForEOF;
			}
			set
			{
				_bUseCachedValuesForEOF = value;
			}
		}
	}
}
