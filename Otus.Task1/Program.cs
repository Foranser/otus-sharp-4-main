using System;
using Otus.Task1.Models;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Otus.Task1

{
    class Program
    {

        static void EnsureValid(SaveFile sf)
        {
            if (sf?.Items == null)
                return;

            foreach (var item in sf.Items)
            {
                if (item != null && item.Quantity < 0)
                    throw new InvalidOperationException("Items.Quantity не может быть меньше 0");
            }
        }


        static SaveFile Generate1()
        {
            var res = new SaveFile();
            res.Coords = (1241.44, 124145.4);
            res.CurrentLocation = "Dungeon";
            res.User = new User { Level = 10, Name = "Пушкин", Gender = Gender.Male };
            res.Items = new[] { new Item() { Name = "Топор", Quantity = 2 } };

            return res;
        }

        static void SerializeBinary(SaveFile sf)
        {
            EnsureValid(sf);

            var formatter = new BinaryFormatter();

            using var ms = new MemoryStream();
            formatter.Serialize(ms, sf);

            Console.WriteLine($"Binary size: {ms.Length} bytes");
        }

        
        static void SerializeJson(SaveFile sf)
        {
            EnsureValid(sf);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
            };

            var json = JsonSerializer.Serialize(sf, options);
            Console.WriteLine(json);
        }


        static void SerializeXml(SaveFile sf)
        {
            EnsureValid(sf);

            var serializer = new XmlSerializer(typeof(SaveFile));

            using var sw = new StringWriter();
            serializer.Serialize(sw, sf);

            Console.WriteLine(sw.ToString());
        }

        static SaveFile Generate2()
        {
            var res = new SaveFile();
            res.Coords = (121.44, 124.4);
            res.CurrentLocation = "Subway";
            res.User = new User { Level = 10, Name = "Feodorov" };
            res.Items = new[] { new Item() { Name = "Stick", Quantity = -2 } };

            return res;
        }

        static void Main(string[] args)
        {
            var g1 = Generate1();
            var g2 = Generate2();

            SerializeBinary(g1);
            SerializeJson(g1);
            SerializeXml(g1);


            try
            {
                SerializeBinary(g2);
                SerializeJson(g2);
                SerializeXml(g2);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
