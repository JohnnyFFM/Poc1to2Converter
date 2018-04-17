using System;
using System.IO;
using System.Runtime.InteropServices;

namespace poc1poc2Conv
{
    public struct Scoop
    {
        //[MarshalAs(UnmanagedType.ByValArray)]//, SizeConst=64
        public byte[] byteArrayField;

        public static Scoop FromBinaryReaderField(BinaryReader br, int noncelimit)
        {
            Scoop s = new Scoop();
            s.byteArrayField = br.ReadBytes(64 * noncelimit);
            return s;
        }

        public byte[] ToByteArray()
        {
            return byteArrayField;
        }

        public int Size()
        {
            return byteArrayField.Length * 64;
        }

    }

}
