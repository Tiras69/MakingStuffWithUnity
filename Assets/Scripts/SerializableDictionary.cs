using System.Collections.Generic;
using System.Xml.Serialization;

namespace Kiwiii.ZombieEscape.CommonServices
{
    [XmlRoot( "Dictionary" )]
    public class SerializableDictionary<K, V>
    {

        [XmlArray( "Pairs" )]
        [XmlArrayItem( "Pair" )]
        public List<SerializablePair<K, V>> Pairs { get; set; }

        public SerializableDictionary()
        {
            Pairs = new List<SerializablePair<K, V>>();
        }

        public IDictionary<K, V> GetDictionary()
        {
            Dictionary<K, V> dictionary = new Dictionary<K, V>();
            foreach ( var pair in Pairs )
            {
                dictionary.Add( pair.Key, pair.Value );
            }
            return dictionary;
        }

        public static SerializableDictionary<K, V> CreateSerializableDictionary( IDictionary<K, V> _dictionary )
        {
            SerializableDictionary<K, V> serializableDictionary = new SerializableDictionary<K, V>();
            foreach ( var key in _dictionary.Keys )
            {
                serializableDictionary.Pairs.Add( new SerializablePair<K, V>() { Key = key, Value = _dictionary [ key ] } );
            }
            return serializableDictionary;
        }
    }
    
    public class SerializablePair<K, V>
    {
        [XmlAttribute( "Key" )]
        public K Key { get; set; }
        [XmlAttribute( "Value" )]
        public V Value { get; set; }

        public SerializablePair()
        {
            Key = default( K );
            Value = default( V );
        }
    }
    
}