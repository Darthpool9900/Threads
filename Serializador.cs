using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml;
using System.Threading;
using System.IO;

namespace Projeto_8
{
    internal static class Serializador
    {
        static private DataContractSerializer Serializer = new DataContractSerializer(typeof(DataBase));

        public static void Serializar(string pPath,DataBase Data)
        {
            XmlWriterSettings settings = new XmlWriterSettings { Indent  = true};
            StringBuilder  constructor = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(constructor, settings);
            Serializer.WriteObject(writer, Data);
            writer.Flush();
            string SerialObject = constructor.ToString();

            FileStream fs = File.Create(pPath);
            fs.Close();
            File.WriteAllText(pPath, SerialObject);
            Thread.Sleep(10000);
            writer.Close();
        }

        public static DataBase unSerialize(string pPath)
        {
            try
            {
                if(File.Exists(pPath))
                {
                    string content =File.ReadAllText(pPath);
                    Thread.Sleep(10000);
                    StringReader reader = new StringReader(content);
                    XmlReader xmlReader = XmlReader.Create(reader);

                    DataBase Data = (DataBase)Serializer.ReadObject(xmlReader);
                    return Data;
                }
                else
                {
                    return null;
                }
            }catch (Exception e)
            {
                Console.WriteLine("Ocorreu a exceção:"+e.ToString());
                return null;
            }
        }
    }
}
