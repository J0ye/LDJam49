using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public bool singelton = true;

    protected static List<GameObject> singles = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        if(singelton && ContainsName(name))
        {
            Destroy(gameObject);
        } else if(singelton)
        {
            singles.Add(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    protected bool ContainsName(string name)
    {
        foreach(GameObject gbj in singles)
        {
            if (gbj.name == name)
                return true;
        }

        return false;
    }
}
