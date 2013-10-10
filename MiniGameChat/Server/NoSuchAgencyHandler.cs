using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using PacketLibrary;

namespace Server
{
    class NoSuchAgencyHandler
    {
        private const String Filepath = "NSA_log.bin";

        public static void writeHandler(String sender, Packet packet)
        {
            Dictionary<String, Dictionary<DateTime, Packet>> informationDictionary = ReadFile(Filepath);
            Dictionary<DateTime, Packet> timedPack = new Dictionary<DateTime, Packet>();
            timedPack.Add(DateTime.Now, packet);
            informationDictionary.Add(sender, timedPack);
            using (FileStream stream = File.Open(Filepath, FileMode.Create))
            {
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(stream, informationDictionary);
            }
        }

        private static Dictionary<String, Dictionary<DateTime, Packet>> ReadFile(String filepath)
        {
            if (!File.Exists(filepath)) return new Dictionary<string, Dictionary<DateTime, Packet>>();

            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = File.OpenRead(filepath))
            {
                Dictionary<String, Dictionary<DateTime, Packet>> info = (Dictionary<String, Dictionary<DateTime, Packet>>)formatter.Deserialize(stream);
                return info;
            }
        }
    }
}
