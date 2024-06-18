using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using static Unity.VisualScripting.Metadata;

public class InvisiblePlatformManager : MonoBehaviour
{

    //public static InvisiblePlatformManager Instance { get; private set; }

    //private void Awake()
    //{
    //    // If there is an instance, and it's not me, delete myself.

    //    if (Instance != null && Instance != this)
    //    {
    //        Destroy(this);
    //    }
    //    else
    //    {
    //        Instance = this;
    //    }
    //}

    //MeshRenderer[] children;
    List<MeshRenderer> children = new List<MeshRenderer>();
    [SerializeField]
    bool visible;


    // Start is called before the first frame update
    void Start()
    {
        //children = new MeshRenderer[transform.childCount];
        //for (int i = 0; i < children.Length; i++)
        //{
        //    children[i] = transform.GetChild(i).GetComponent<MeshRenderer>();
        //    children[i].enabled = false;
        //}

        Transform[] AllChildren = GetComponentsInChildren<Transform>();

        for (int i = 0; i < AllChildren.Length; i++)
        {
            children.Add(GetComponentInChildren<MeshRenderer>(AllChildren[i]));
        }

        foreach(MeshRenderer child in children)
        {
            child.enabled = false;
        }
        

        //List<Transform> OnesThatIWant = new List<Transform>();

        //foreach (var child in AllChildren)
        //{
        //    // here is where you decide if you want this (replace it with whatever you like)
        //    if (child.gameObject.tag == "Active")
        //    {
        //        OnesThatIWant.Add(child);
        //    }
        //}


    }

    public int TogglePlatforms()
    {

        visible = !visible;

        for (int i = 0; i < children.Count; i++)
        {
            children[i].enabled = visible;
        }

        if (visible)
        {
            return 1;
        }

        return 0;
    }

    public void DisablePlatforms()
    {

        visible = false;

        for (int i = 0; i < children.Count; i++)
        {
            children[i].enabled = false;
        }
    }
}
