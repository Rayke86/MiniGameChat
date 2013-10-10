using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using PacketLibrary;

namespace Server
{
    class NSAhandler
    {
        public static void writeHandler(String sender, Packet packet)
        {
            String filepath = "NSA_log.bin";

            Dictionary<String, Dictionary<DateTime, Packet>> informationDictionary = readFile(filepath);
            Dictionary<DateTime, Packet> timedPack = new Dictionary<DateTime, Packet>();
            timedPack.Add(DateTime.Now, packet);
            informationDictionary.Add(sender, timedPack);
            using (FileStream stream = File.Open(filepath, FileMode.Create))
            {
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(stream, informationDictionary);
            }
        }

        private static Dictionary<String, Dictionary<DateTime, Packet>> readFile(String filepath)
        {
            if (File.Exists(filepath))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream stream = File.OpenRead(filepath))
                {
                    Dictionary<String, Dictionary<DateTime, Packet>> info = (Dictionary<String, Dictionary<DateTime, Packet>>)formatter.Deserialize(stream);
                    return info;
                }
            }
            return new Dictionary<string, Dictionary<DateTime, Packet>>();
        }
    }
}
