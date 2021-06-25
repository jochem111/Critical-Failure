using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDirty : MonoBehaviour
{
    public Shoe shoe;

    public GameObject dirtPrefab;
    public GameObject dirtParent;

    public List<GameObject> spawnPoints;
    public GameObject currentPoint;

    public string spawnPointTag;

    public int index;
    public int dirtCount;
    public int listCount;
    public int one;

    private void Start()
    {
        OnStart();
    }

    public void OnStart()
    {
        SpawnObjects(dirtCount);
    }

    public void SpawnObjects(int count)
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            GameObject temp = spawnPoints[i];
            int randomIndex = Random.Range(i, spawnPoints.Count);
            spawnPoints[i] = spawnPoints[randomIndex];
            spawnPoints[randomIndex] = temp;
        }

        for (int i = 0; i < count; i++)
        {
            GameObject dirtClone = Instantiate(dirtPrefab, new Vector3(spawnPoints[i].transform.position.x, spawnPoints[i].transform.position.y, spawnPoints[i].transform.position.z), spawnPoints[i].transform.rotation);
            dirtClone.transform.parent = dirtParent.transform;
            dirtClone.name = "Dirt Clone " + (i + one);

            shoe.dirtCount += one;
        }
    }

    public void LowerDirtCount()
    {
        shoe.dirtCount -= one;
    }
}