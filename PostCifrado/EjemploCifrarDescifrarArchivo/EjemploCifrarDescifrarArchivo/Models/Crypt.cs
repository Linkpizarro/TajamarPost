using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace EjemploCifrarDescifrarArchivo.Models
{
    public class Crypt
    {
        public static byte[] EncodingPrivateKey(String privada)
        {
            byte[] key = UTF8Encoding.UTF8.GetBytes(privada);
            int keySize = 32;
            Array.Resize(ref key, keySize);
            return key;
        }

        public static byte[] EncodingPublicKey(String publica)
        {
            byte[] iv = UTF8Encoding.UTF8.GetBytes(publica);
            int ivSize = 16;
            Array.Resize(ref iv, ivSize);
            return iv;
        }

        public static void encryptToFile(String plainMessage, String filename, byte[] Key, byte[] IV)
        {
            // Crear un flujo para el archivo a generarse
            FileStream fileStream = File.Open(filename, FileMode.OpenOrCreate);

            // Crear una instancia del algoritmo Rijndael
            Rijndael RijndaelAlg = Rijndael.Create();

            // Crear un flujo de cifrado basado en el flujo de los datos
            CryptoStream cryptoStream = new CryptoStream(fileStream,
                                                         RijndaelAlg.CreateEncryptor(Key, IV),
                                                         CryptoStreamMode.Write);

            // Crear un flujo de escritura basado en el flujo de cifrado
            StreamWriter streamWriter = new StreamWriter(cryptoStream);

            // Cifrar el mensaje a través del flujo de escritura
            streamWriter.WriteLine(plainMessage);

            // Cerrar los flujos utilizados
            streamWriter.Close();
            cryptoStream.Close();
            fileStream.Close();
        }

        public static string decryptFromFile(String filename, byte[] Key, byte[] IV)
        {
            // Crear un flujo para el archivo a generarse
            FileStream fileStream = File.Open(filename, FileMode.OpenOrCreate);

            // Crear una instancia del algoritmo Rijndael
            Rijndael RijndaelAlg = Rijndael.Create();

            // Crear un flujo de cifrado basado en el flujo de los datos
            CryptoStream cryptoStream = new CryptoStream(fileStream,
                                                         RijndaelAlg.CreateDecryptor(Key, IV),
                                                         CryptoStreamMode.Read);

            // Crear un flujo de lectura basado en el flujo de cifrado
            StreamReader streamReader = new StreamReader(cryptoStream);

            // Descifrar el mensaje a través del flujo de lectura
            string plainMessage = streamReader.ReadLine();

            // Cerrar los flujos utilizados
            streamReader.Close();
            cryptoStream.Close();
            fileStream.Close();

            return plainMessage;
        }
    }
}