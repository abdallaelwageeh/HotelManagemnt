using Helper.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public static class SystemHelper
    {
        public static HttpClient Client = new HttpClient() { BaseAddress = new Uri(@"https://localhost:44370/api/") };
        public static string  GetAction(string address,bool flag=false)
        {
            using (HttpResponseMessage response = Client.GetAsync(address).GetAwaiter().GetResult())
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    return DecryptContent(responseContent,flag);
                }
            }
            return "";
        }
        public static string PostAction(string address,string content,bool flag=false)
        {

            StringContent Content = new StringContent(JsonConvert.SerializeObject(new Message {value=content}),Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = Client.PostAsync(new Uri(Client.BaseAddress.ToString()+address),Content).GetAwaiter().GetResult())
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    return DecryptContent(responseContent,flag);
                }
            }
            return "";
        }
        public static string PutAction(string address, string content)
        {
            StringContent Content = new StringContent(content);
            using (HttpResponseMessage response = Client.PutAsync(address, Content).GetAwaiter().GetResult())
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    return DecryptContent(responseContent);
                }
            }
            return "";
        }
        public static string DeleteAction(string address)
        {
            using (HttpResponseMessage response = Client.DeleteAsync(address).GetAwaiter().GetResult())
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    return DecryptContent(responseContent);
                }
            }
            return "";
        }
        public static string EncryptContent(string content)
        {
            byte[]encryptedContent=EncryptStringToBytes_Aes(content);
            return Convert.ToBase64String(encryptedContent);
        }
        public static string DecryptContent(string content, bool flag = false)
        {
            if (flag)
            {
                content = content.Remove(content.IndexOf('"'), 1);
                content = content.Remove(content.LastIndexOf('"'));
            }
            var encryptedContent = Convert.FromBase64String(content);
            return DecryptStringFromBytes_Aes(encryptedContent);
        }
        private static string DecryptStringFromBytes_Aes(byte[] cipherText)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
                aesAlg.IV = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }
        private static byte[] EncryptStringToBytes_Aes(string plainText)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
                aesAlg.IV = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        public static List<Room> GenerateRoomsList(string text)
        {
            switch (text)
            {
                case "":
                    return new List<Room>();
                default:
                    return JsonConvert.DeserializeObject<List<Room>>(text);
            }
        }
        public static List<Models.Type> GenerateTypeList(string text)
        {
            switch (text)
            {
                case "":
                    return new List<Models.Type>();
                default:
                    return JsonConvert.DeserializeObject<List<Models.Type>>(text);
            }
        }

    }
}
