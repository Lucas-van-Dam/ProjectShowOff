using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using static Unity.VisualScripting.Metadata;

public class InvisiblePlatformManager : MonoBehaviour
{

    public static InvisiblePlatformManager Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

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
            children[i].enabled = false;
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

    public void DisablePlatforms()
    {

        visible = false;

        for (int i = 0; i < children.Length; i++)
        {
            children[i].enabled = false;
        }
    }
}
