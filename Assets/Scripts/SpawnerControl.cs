using Scripts;
using Unity.Netcode;
using UnityEngine;

public class SpawnerControl : NetworkSingleton<SpawnerControl>
{
    [SerializeField]
    private GameObject objectPrefab;

    [ServerRpc(RequireOwnership = false)]
    public void SpawnObjectsServerRpc(Vector3 position)
    {

            //GameObject go = Instantiate(objectPrefab, 
            //    new Vector3(Random.Range(-10, 10), 10.0f, Random.Range(-10, 10)), Quaternion.identity);
            GameObject go = NetworkObjectPool.Instance.GetNetworkObject(objectPrefab).gameObject;
            go.transform.position = position;

            go.GetComponent<NetworkObject>().SpawnWithOwnership(OwnerClientId);
    }
}