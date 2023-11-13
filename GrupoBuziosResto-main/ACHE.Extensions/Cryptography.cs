/*-
*       Copyright (c) 2006-2007 Eugenio Serrano, Daniel Calvin.
*       All rights reserved.
*
*       Redistribution and use in source and binary forms, with or without
*       modification, are permitted provided that the following conditions
*       are met:
*       1. Redistributions of source code must retain the above copyright
*          notice, this list of conditions and the following disclaimer.
*       2. Redistributions in binary form must reproduce the above copyright
*          notice, this list of conditions and the following disclaimer in the
*          documentation and/or other materials provided with the distribution.
*       3. Neither the name of copyright holders nor the names of its
*          contributors may be used to endorse or promote products derived
*          from this software without specific prior written permission.
*
*       THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
*       "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED
*       TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
*       PURPOSE ARE DISCLAIMED.  IN NO EVENT SHALL COPYRIGHT HOLDERS OR CONTRIBUTORS
*       BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
*       CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
*       SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
*       INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
*       CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
*       ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
*       POSSIBILITY OF SUCH DAMAGE.
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace ACHE.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class Cryptography
    {
        /*
		//The key used for generating the encrypted string
		private const string _CryptoKey = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";//@"*1Am|9Kh)+/?sfGq88";

		//The Initialization Vector for the DES encryption routine
		private static readonly byte[] _Iv = { 241, 0, 34, 19, 23, 32, 44, 151 };


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sourceString"></param>
		/// <returns></returns>
		public static string Encrypt(string sourceString)
		{
			return Encrypt(sourceString, _CryptoKey);
		}

        public static string Encrypt(int sourceString)
        {
            return Encrypt(sourceString.ToString(), _CryptoKey);
        }


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sourceString"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string Encrypt(string sourceString, string key)
		{
			Byte[] buffer = Encoding.ASCII.GetBytes(sourceString);
			TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
			MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
			des.Key = MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(key));
			des.IV = _Iv;
			string result = Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length));
			des.Clear();
			MD5.Clear();
			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="encryptedString"></param>
		/// <returns></returns>
		public static string Decrypt(string encryptedString)
		{
			return Decrypt(encryptedString, _CryptoKey);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="encryptedString"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string Decrypt(string encryptedString, string key)
		{
			try
			{
				Byte[] buffer = Convert.FromBase64String(encryptedString);
				TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
				MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
				des.Key = MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(key));
				des.IV = _Iv;
				string result = Encoding.ASCII.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length));
				des.Clear();
				MD5.Clear();
				return result;

			}
			catch (CryptographicException ex)
			{
				throw ex;
			}
		}

        */

        public static string Encrypt(string value)
        {
            return Encrypt(int.Parse(value));
        }

        // expects a integer value and length of the binary string (e.g. 4, 8, 16).
        // returns the padded binary string
        public static string Encrypt(int value)
        {
            string hex = "";
            foreach (char c in value.ToString())
            {
                int tmp = c;
                hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
            }
            return "GBR-" + hex;

        }

        // expects the binary string and returns it's integer equivalent
        public static string Decrypt(string encrypt)
        {
            encrypt = encrypt.Replace("GBR-", "");
            string StrValue = "";
            while (encrypt.Length > 0)
            {
                StrValue += System.Convert.ToChar(System.Convert.ToUInt32(encrypt.Substring(0, 2), 16)).ToString();
                encrypt = encrypt.Substring(2, encrypt.Length - 2);
            }
            return StrValue;

        }
    }
}
