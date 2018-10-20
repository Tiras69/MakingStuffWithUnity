using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class CommandWrapperManager
{

    #region Singleton Stuff
    private static CommandWrapperManager m_instance;
    public static CommandWrapperManager Instance
    {
        get
        {
            if ( m_instance == null )
            {
                m_instance = new CommandWrapperManager();
            }
            return m_instance;
        }
    }
    #endregion

    private Dictionary<string, KeyCode> m_commandToButtonList;

    private CommandWrapperManager()
    {
        string pathName = Path.Combine( Application.persistentDataPath, Constants.FileNames.CommandFileName );
        m_commandToButtonList = (Dictionary<string, KeyCode>) XmlSerializerServices.DeserializeXmlFile<SerializableDictionary<string, KeyCode>>( pathName ).GetDictionary();
    }

    public bool CheckCommand( string _command )
    {
        if ( m_commandToButtonList.ContainsKey( _command ) )
        {
            return Input.GetKey( m_commandToButtonList [ _command ] );
        }
        else
        {
            Debug.Log( string.Format( "The {0} doesn't exists in the command dictionary", _command ) );
            return false;
        }

    }

}
