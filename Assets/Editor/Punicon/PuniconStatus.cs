using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PuniconStatus", menuName = "Create/OGT Script/PuniconStatus")]
public class PuniconStatus : ScriptableObject
{
    #region Singleton
    private static PuniconStatus instance = null;
    private readonly string FILE_PATH = "PuniconStatus";
    public PuniconStatus GetStatus
    {
        get
        {
            if(instance == null)
            {
                var newInstance = Resources.Load(FILE_PATH);
                if( newInstance == null )
                {
                    newInstance = CreateInstance<PuniconStatus>();
                }

            }
            return instance;
        }
    }
    #endregion


}
