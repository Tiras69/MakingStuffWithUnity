using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class XmlSerializerServices
{
    public static void SerializeXmlFile<T>( T _object, string _path )
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
        using ( StreamWriter streamWriter = new StreamWriter( _path ) )
        {
            try
            {
                xmlSerializer.Serialize( streamWriter, _object );
            }
            catch ( Exception e )
            {
                Debug.Log( "Serialization of the file : " + _path + " cannot proceed ( " + e.ToString() + " )" );

            }
        }
    }

    public static T DeserializeXmlFile<T>( string _path )
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
        using ( StreamReader streamReader = new StreamReader( _path ) )
        {
            try
            {
                T p = (T)xmlSerializer.Deserialize(streamReader);
                return p;
            }
            catch ( Exception e )
            {
                Debug.Log( "Serialization of the file : " + _path + " cannot proceed ( " + e.ToString() + " )" + Environment.NewLine + "Returning DefaultValue." );
                return default( T );
            }
        }
    }
}