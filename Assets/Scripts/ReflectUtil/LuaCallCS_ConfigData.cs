using System.IO;
using System.Collections.Generic;
using System.Text;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;



public static partial class LuaCallCS
{
    public static string GetConfigData(string configName, string index, string name)
    {
        var config = ReadSafeFile<Dictionary<string, Dictionary<string, string>>>(DataUtilityManager.m_binPath + "/Config/Client/" + configName + ".bin");

        if(config.ContainsKey(index) && config[index].ContainsKey(name))
        {
            return config[index][name];
        }

        return "";
    }

    public static byte[] ReadFileByteData(string path)
    {
        byte[] byteData = null;

        using (FileStream encryptFileStream = new FileStream(path, FileMode.Open))
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                encryptFileStream.CopyTo(memoryStream);
                byteData = memoryStream.ToArray();
            }
        }

        return byteData;
    }

    public static void CreateFileByBytes(string path, byte[] inputBytes)
    {
        DataUtilityManager.InitDirectory(path);

        using (FileStream fileStream = new FileStream(path, FileMode.Create))
        {
            using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
            {
                binaryWriter.Write(inputBytes);
            }
        }
    }

    public static byte[] SerializeData(object data)
    {
        byte[] serializeBytes = null;

        using (MemoryStream memoryStream = new MemoryStream())
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            binaryFormatter.Serialize(memoryStream, data);

            serializeBytes = memoryStream.ToArray();
        }

        return serializeBytes;
    }

    public static T Deserialize<T>(byte[] inputBytes)
    {
        T result = default(T);

        using (MemoryStream memoryStream = new MemoryStream(inputBytes))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            result = (T)binaryFormatter.Deserialize(memoryStream);
        }

        return result;
    }

    public static byte[] CompressByteData(byte[] inputBytes)
    {
        byte[] compressBytes = null;

        using (MemoryStream compressMemoryStream = new MemoryStream())
        {
            using (GZipStream compressionStream = new GZipStream(compressMemoryStream, CompressionMode.Compress))
            {
                compressionStream.Write(inputBytes, 0, inputBytes.Length);
            }

            compressBytes = compressMemoryStream.ToArray();
        }

        return compressBytes;
    }

    public static byte[] DecompressByteData(byte[] inputBytes)
    {
        byte[] decompressedBytes = null;

        using (MemoryStream compressedMemoryStream = new MemoryStream(inputBytes))
        {
            using (GZipStream compressionStream = new GZipStream(compressedMemoryStream, CompressionMode.Decompress))
            {
                using (MemoryStream decompressedMemoryStream = new MemoryStream())
                {
                    compressionStream.CopyTo(decompressedMemoryStream);
                    decompressedBytes = decompressedMemoryStream.ToArray();
                }
            }
        }

        return decompressedBytes;
    }

    public static byte[] EncryptByteData(byte[] inputBytes, byte[] key, byte[] iv)
    {
        byte[] encryptBytes = null;

        using (AesManaged aes = new AesManaged())
        {
            aes.Key = key;
            aes.IV = iv;

            using (ICryptoTransform encryptor = aes.CreateEncryptor(key, iv))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(inputBytes, 0, inputBytes.Length);
                        cryptoStream.FlushFinalBlock();//加密会将最后一个数据块填充为满块(需要)，解密会删除填充的数据块(不需要)
                    }

                    encryptBytes = memoryStream.ToArray();
                }
            }
        }

        return encryptBytes;
    }

    public static byte[] DecryptByteData(byte[] inputBytes, byte[] key, byte[] iv)
    {
        byte[] decryptBytes = null;

        using (MemoryStream inputMemoryStream = new MemoryStream(inputBytes))
        {
            using (AesManaged aes = new AesManaged())
            {
                aes.Key = key;
                aes.IV = iv;

                using (ICryptoTransform decryptor = aes.CreateDecryptor(key, iv))
                {
                    using (MemoryStream outputMemoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(inputMemoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            cryptoStream.CopyTo(outputMemoryStream);
                        }

                        decryptBytes = outputMemoryStream.ToArray();
                    }
                }
            }
        }

        return decryptBytes;
    }

    public static void SaveSafeFile(object data, string filePath)
    {
        if (data == null)
        {
            return;
        }

        byte[] inputBytes = SerializeData(data);
        byte[] compressBytes = CompressByteData(inputBytes);
        byte[] encryptBytes = EncryptByteData(compressBytes, Encoding.UTF8.GetBytes("95gbt368426hyb13"), Encoding.UTF8.GetBytes("i8g3451h5cxmj6rf"));

        CreateFileByBytes(filePath, encryptBytes);
    }

    public static T ReadSafeFile<T>(string path)
    {
        byte[] inputBytes = ReadFileByteData(path);
        byte[] decryptBytes = DecryptByteData(inputBytes, Encoding.UTF8.GetBytes("95gbt368426hyb13"), Encoding.UTF8.GetBytes("i8g3451h5cxmj6rf"));
        byte[] decompressedBytes = DecompressByteData(decryptBytes);

        T result = Deserialize<T>(decompressedBytes);

        return result;
    }

    public static T ReadSafeFile<T>(byte[] inputBytes)
    {
        byte[] decryptBytes = DecryptByteData(inputBytes, Encoding.UTF8.GetBytes("95gbt368426hyb13"), Encoding.UTF8.GetBytes("i8g3451h5cxmj6rf"));
        byte[] decompressedBytes = DecompressByteData(decryptBytes);

        T result = Deserialize<T>(decompressedBytes);

        return result;
    }

    public static string FormatFileByteSize(long bytes)
    {
        string[] units = { "B", "KB", "MB", "GB", "TB" };
        int unitIndex = 0;
        double size = bytes;

        while (size >= 1024 && unitIndex < units.Length - 1)
        {
            size /= 1024;
            unitIndex++;
        }

        return $"{size:0.##} {units[unitIndex]}";
    }

    public static bool GetTextureRectByAtlasName(string atlasName, string textureName, out float[] rect)
    {
        rect = new float[4];

        string path = DataUtilityManager.m_binPath + "/Atlas/" + atlasName + ".bin";

        if (!File.Exists(path))
        {
            return false;
        }

        Dictionary<string, float[]> textureRect = ReadSafeFile<Dictionary<string, float[]>>(path);

        if (textureRect.ContainsKey(textureName))
        {
            rect = textureRect[textureName];
            return true;
        }

        return false;
    }
}