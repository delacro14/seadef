using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objectPrefabs;

    public GameObject GetObject(string type)
    {
        for(int i = 0; i < objectPrefabs.Length; i++)
        {
            if(objectPrefabs[i].name == type)
            {
                GameObject ship = MonoBehaviour.Instantiate(objectPrefabs[i]);
                ship.name = type;
                return ship;
            }
        }
        return null;
    }
}
