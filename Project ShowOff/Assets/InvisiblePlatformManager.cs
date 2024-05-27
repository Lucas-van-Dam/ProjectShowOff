using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static Unity.VisualScripting.Metadata;

public class InvisiblePlatformManager : MonoBehaviour
{

    MeshRenderer[] children;
    [SerializeField]
    bool visible;


    // Start is called before the first frame update
    void Start()
    {
        children = new MeshRenderer[transform.childCount];
        for (int i = 0; i < children.Length; i++)
        {
            children[i] = transform.GetChild(i).GetComponent<MeshRenderer>();
        }
    }

    public void TogglePlatforms()
    {

        visible = !visible;

        for (int i = 0; i < children.Length; i++)
        {
            children[i].enabled = visible;
        }
    }
}
