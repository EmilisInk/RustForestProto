using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Terrain terrain;

    public GameObject[] nodes;

    public int maxNodes = 20;
    public float respawnDelay = 10f;
    public float minDistanceBetweenNodes = 5f;

    [Header("Overlap Check")]
    public float overlapRadius = 2f;
    public LayerMask blockSpawnLayers;


    private List<GameObject> spawnedNodes = new List<GameObject>();

    private void Start()
    {
        terrain = Terrain.activeTerrain;
        for(int i = 0; i < maxNodes; i++)
        {
            SpawnNode();
        }
    }

    void SpawnNode()
    {
        Vector3 pos = GetRandomTerrainPosition();

        int tries = 30;
        while(tries-- > 0 && IsTooClose(pos) || IsPositionBlocked(pos))
        {
            pos = GetRandomTerrainPosition();
        }

        GameObject prefab = nodes[Random.Range(0, nodes.Length)];
        GameObject obj = Instantiate(prefab, pos, Quaternion.identity);

        spawnedNodes.Add(obj);

        NodeRespawnWatcher w = obj.AddComponent<NodeRespawnWatcher>();
        w.spawner = this;
    }

    bool IsPositionBlocked(Vector3 pos)
    {
        return Physics.CheckSphere(pos, overlapRadius, blockSpawnLayers);
    }   

    Vector3 GetRandomTerrainPosition()
    {
        Vector3 tPos = terrain.transform.position;
        Vector3 tSize = terrain.terrainData.size;

        float x = Random.Range(tPos.x, tPos.x + tSize.x);
        float z = Random.Range(tPos.z, tPos.z + tSize.z);

        Vector3 origin = new Vector3(x, tPos.y + tSize.y + 50f, z);

        if(Physics.Raycast(origin, Vector3.down, out RaycastHit hit, 10000f))
        {
            return hit.point;
        }

        float y = terrain.SampleHeight(new Vector3(x, 0f, z)) + tPos.y;
        return new Vector3(x, y, z);

    }

    bool IsTooClose(Vector3 pos)
    {
        foreach(GameObject node in spawnedNodes)
        {
            if(Vector3.Distance(pos, node.transform.position) < minDistanceBetweenNodes)
            {
                return true;
            }
        }
        return false;
    }

    public void NotifyDestroyed(GameObject obj)
    {
        spawnedNodes.Remove(obj);
        StartCoroutine(RespawnNodeAfterDelay());
    }

    IEnumerator RespawnNodeAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);
        SpawnNode();
    }


}
