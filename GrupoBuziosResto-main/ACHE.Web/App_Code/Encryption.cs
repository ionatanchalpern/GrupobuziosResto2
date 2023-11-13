using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace ACHE.Model {
    public class Encryption {
        private static long key1 = 28757863;
        private static long key2 = 89598347;

        public static string EncriptarCadena(int id) {
            long aux = long.Parse(id.ToString());
            aux += key1;
            aux = aux * key2;
            return aux.ToString();
        }

        public static int DesencriptarCadena(string cadena) {
            int result;
            long aux = long.Parse(cadena);
            aux = aux / key2;
            aux -= key1;
            if (int.TryParse(aux.ToString(), out result))
                return result;
            else
                return 0;
        }
    }
}