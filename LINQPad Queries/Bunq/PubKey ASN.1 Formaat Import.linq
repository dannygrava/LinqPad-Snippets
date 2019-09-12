<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
  <Namespace>System.Security.Cryptography.X509Certificates</Namespace>
</Query>

void Main()
{
	RSACryptoServiceProvider bronCsp = new RSACryptoServiceProvider();
	string exportedPubKey = ExportPublicKey (bronCsp);
	exportedPubKey.Dump("Bron");
	RSACryptoServiceProvider importedCsp = DecodeX509PublicKey(Convert.FromBase64String(exportedPubKey.Replace("-----BEGIN PUBLIC KEY-----", "").Replace("-----END PUBLIC KEY-----", "")));
	string importedPubKey = ExportPublicKey (importedCsp);
	importedPubKey.Dump("Geïmporteerd");
	
	(exportedPubKey == importedPubKey).Dump("Are the public keys the same before and after export?");
	
	// Verify the signature with the imported public key (gebaseerd op voorbeeld in C# in a Nutshell)
	
	// Step 1: Create a signature with the private key
	byte[] data = Encoding.UTF8.GetBytes("Message to sign");
	byte[] signature = bronCsp.SignData(data, CryptoConfig.MapNameToOID("SHA256"));
	
	// Step 2: Verify the signature with the imported pub key
	importedCsp.VerifyData(data, CryptoConfig.MapNameToOID("SHA256"), signature).Dump("Result of verification");	
	
	// Step 3: Now tamper with the information
	data[0] = 0;
	importedCsp.VerifyData(data, CryptoConfig.MapNameToOID("SHA256"), signature).Dump("Result of verification after tampering");	
}

public static string ExportPublicKey(RSACryptoServiceProvider csp)
{
    TextWriter outputStream = new StringWriter();
    var parameters = csp.ExportParameters(false);
    using (var stream = new MemoryStream())
    {
        var writer = new BinaryWriter(stream);
        writer.Write((byte)0x30); // SEQUENCE
        using (var innerStream = new MemoryStream())
        {
            var innerWriter = new BinaryWriter(innerStream);
            innerWriter.Write((byte)0x30); // SEQUENCE
            encodeLength(innerWriter, 13);
            innerWriter.Write((byte)0x06); // OBJECT IDENTIFIER
            var rsaEncryptionOid = new byte[] { 0x2a, 0x86, 0x48, 0x86, 0xf7, 0x0d, 0x01, 0x01, 0x01 };
            encodeLength(innerWriter, rsaEncryptionOid.Length);
            innerWriter.Write(rsaEncryptionOid);
            innerWriter.Write((byte)0x05); // NULL
            encodeLength(innerWriter, 0);
            innerWriter.Write((byte)0x03); // BIT STRING
            using (var bitStringStream = new MemoryStream())
            {
                var bitStringWriter = new BinaryWriter(bitStringStream);
                bitStringWriter.Write((byte)0x00); // # of unused bits
                bitStringWriter.Write((byte)0x30); // SEQUENCE
                using (var paramsStream = new MemoryStream())
                {
                    var paramsWriter = new BinaryWriter(paramsStream);
                    encodeIntegerBigEndian(paramsWriter, parameters.Modulus); // Modulus
                    encodeIntegerBigEndian(paramsWriter, parameters.Exponent); // Exponent
                    var paramsLength = (int)paramsStream.Length;
                    encodeLength(bitStringWriter, paramsLength);
                    bitStringWriter.Write(paramsStream.GetBuffer(), 0, paramsLength);
                }
                var bitStringLength = (int)bitStringStream.Length;
                encodeLength(innerWriter, bitStringLength);
                innerWriter.Write(bitStringStream.GetBuffer(), 0, bitStringLength);
            }
            var length = (int)innerStream.Length;
            encodeLength(writer, length);
            writer.Write(innerStream.GetBuffer(), 0, length);
        }
        
        var base64 = Convert.ToBase64String(stream.GetBuffer(), 0, (int)stream.Length).ToCharArray();
        outputStream.Write("-----BEGIN PUBLIC KEY-----\n");
        for (var i = 0; i < base64.Length; i += 64)
        {
            outputStream.Write(base64, i, Math.Min(64, base64.Length - i));
            outputStream.Write("\n");
        }
        outputStream.Write("-----END PUBLIC KEY-----");
        return outputStream.ToString();
    }
}


 static string RSA(string input)
        {
            RSACryptoServiceProvider rsa = DecodeX509PublicKey(Convert.FromBase64String(GetKey()));

            return (Convert.ToBase64String(rsa.Decrypt(Encoding.ASCII.GetBytes(input), false)));
        }

        static string GetKey()
        {
			string serverPublicKey = @"-----BEGIN PUBLIC KEY-----
MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAxspzPyk65FIeoY1hxVnk
L82sF5nWxEJOaPjVoFesfJf9s4slp0nNDE2oIfZnOWzG82ys6XjnFnBHB+djQnbP
Y12iZWH7UQGk9pkzHyp0r/vz6anXAnkaN2uEQoND35jxo0ugNN28T0t/S28ZbG+M
5BylbfsS65D3+MoxHZL5Soh69kus1d2LVezSrLlqRHvYJsvQmdspHd4gqB4wUEus
GPGZ+rs13goHIlkYMM0aFIRJY03rSYovKOVpB19HpQlv1JzXWuHIzJmh+4W5uqdx
XyTGMKG0A4qGW+6nEHTNz9tMK2SL3TkP9K8cfdGTqnFswXX141DgdO9LEaQ3eY+t
TQIDAQAB
-----END PUBLIC KEY-----";
		    return serverPublicKey.Replace("-----BEGIN PUBLIC KEY-----", "").Replace("-----END PUBLIC KEY-----", "");
			//return "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA0OeEcOu4QTY4E58FA9mR\nDlmqoXeDTCmU98/7YQnAz/k3AmAnlZskMDtblJVlaMYO7N79uTNuLdDCYnhZPGYI\n+e+rYD5sP6MZsohm+CffdOLj0DpbLd5gs+H5FmfQq6aK5NE1KdW3dKv5Tpa85Ho4\nAMkdxpZgRT9lmJUe6zxqw0fxwX6KLY4WtMIunLqUmWrWPPz5ncOsFNhD92Mc4TvH\nxaBGTlXoOpNPrxpICBMFDWFQUk6qZbQTOahuj4feI4zXkIaeKjwrQj59Ncx3LILu\nR5pQ0MzeeYqexPvku61dXEMo8ua3Xt/aqYKEJV/IFd4s6sTZkRokP2/kQ7AGzsFh\nGQIDAQAB\n".Dump();
            //return File.ReadAllText("master.pub").Replace("-----BEGIN PUBLIC KEY-----", "").Replace("-----END PUBLIC KEY-----", "");
            //.Replace("\n", "");
        }

        private static bool CompareBytearrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            int i = 0;
            foreach (byte c in a)
            {
                if (c != b[i])
                    return false;
                i++;
            }
            return true;
        }

        public static RSACryptoServiceProvider DecodeX509PublicKey(byte[] x509key)
        {
            // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
            byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
            byte[] seq = new byte[15];
            // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------
            MemoryStream mem = new MemoryStream(x509key);
            BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading
            byte bt = 0;
            ushort twobytes = 0;

            try
            {

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();    //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();   //advance 2 bytes
                else
                    return null;

                seq = binr.ReadBytes(15);       //read the Sequence OID
                if (!CompareBytearrays(seq, SeqOID))    //make sure Sequence for OID is correct
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8103) //data read as little endian order (actual data order for Bit String is 03 81)
                    binr.ReadByte();    //advance 1 byte
                else if (twobytes == 0x8203)
                    binr.ReadInt16();   //advance 2 bytes
                else
                    return null;

                bt = binr.ReadByte();
                if (bt != 0x00)     //expect null byte next
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();    //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();   //advance 2 bytes
                else
                    return null;

                twobytes = binr.ReadUInt16();
                byte lowbyte = 0x00;
                byte highbyte = 0x00;

                if (twobytes == 0x8102) //data read as little endian order (actual data order for Integer is 02 81)
                    lowbyte = binr.ReadByte();  // read next bytes which is bytes in modulus
                else if (twobytes == 0x8202)
                {
                    highbyte = binr.ReadByte(); //advance 2 bytes
                    lowbyte = binr.ReadByte();
                }
                else
                    return null;
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };   //reverse byte order since asn.1 key uses big endian order
                int modsize = BitConverter.ToInt32(modint, 0);

                byte firstbyte = binr.ReadByte();
                binr.BaseStream.Seek(-1, SeekOrigin.Current);

                if (firstbyte == 0x00)
                {   //if first byte (highest order) of modulus is zero, don't include it
                    binr.ReadByte();    //skip this null byte
                    modsize -= 1;   //reduce modulus buffer size by 1
                }

                byte[] modulus = binr.ReadBytes(modsize);   //read the modulus bytes

                if (binr.ReadByte() != 0x02)            //expect an Integer for the exponent data
                    return null;
                int expbytes = (int)binr.ReadByte();        // should only need one byte for actual exponent data (for all useful values)
                byte[] exponent = binr.ReadBytes(expbytes);

                // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                RSAParameters RSAKeyInfo = new RSAParameters();
                RSAKeyInfo.Modulus = modulus;
                RSAKeyInfo.Exponent = exponent;
                RSA.ImportParameters(RSAKeyInfo);
                return RSA;
            }
            catch (Exception)
            {
                return null;
            }

            finally { binr.Close(); }

        }
		
        private static void encodeIntegerBigEndian(BinaryWriter stream, byte[] value, bool forceUnsigned = true)
        {
            stream.Write((byte)0x02); // INTEGER
            var prefixZeros = 0;
            for (var i = 0; i < value.Length; i++)
            {
                if (value[i] != 0) break;
                prefixZeros++;
            }
            if (value.Length - prefixZeros == 0)
            {
                encodeLength(stream, 1);
                stream.Write((byte)0);
            }
            else
            {
                if (forceUnsigned && value[prefixZeros] > 0x7f)
                {
                    // Add a prefix zero to force unsigned if the MSB is 1
                    encodeLength(stream, value.Length - prefixZeros + 1);
                    stream.Write((byte)0);
                }
                else
                {
                    encodeLength(stream, value.Length - prefixZeros);
                }
                for (var i = prefixZeros; i < value.Length; i++)
                {
                    stream.Write(value[i]);
                }
            }
        }

        private static void encodeLength(BinaryWriter stream, int length)
        {
            if (length < 0) throw new ArgumentOutOfRangeException("length", "Length must be non-negative");
            if (length < 0x80)
            {
                // Short form
                stream.Write((byte)length);
            }
            else
            {
                // Long form
                var temp = length;
                var bytesRequired = 0;
                while (temp > 0)
                {
                    temp >>= 8;
                    bytesRequired++;
                }
                stream.Write((byte)(bytesRequired | 0x80));
                for (var i = bytesRequired - 1; i >= 0; i--)
                {
                    stream.Write((byte)(length >> (8 * i) & 0xff));
                }
            }
        }		