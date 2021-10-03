using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultiFunctionScript : MonoBehaviour
{
    public List<Material> MaterialTable = new List<Material>();
    public List<Color> ColorTable = new List<Color>();
    public List<GameObject> prefabs = new List<GameObject>();
    public string sceneName = "";

    private Material standardMaterial;
    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<Renderer>() != null)
        {
            standardMaterial = GetComponent<Renderer>().material;
        }
    }

    public void LoadSceneAfter(float sec)
    {
        Invoke("LoadScene", sec);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SpawnPrefab(int i)
    {
        i = Mathf.Clamp(i, 0, prefabs.Count - 1);
        GameObject gb = Instantiate(prefabs[i], transform.position, prefabs[i].transform.rotation);
    }

    public void ChangeColorTo(int index)
    {
        GetComponent<Renderer>().material = MaterialTable[index];
    }
    public void ChangeImageColorTo(int index)
    {
        GetComponent<Image>().color = ColorTable[index];
    }

    public void ResetColor()
    {
        GetComponent<Renderer>().material = standardMaterial;
    }
}
