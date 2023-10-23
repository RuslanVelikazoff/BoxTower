using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject boxPrefab;

    public void SpawnBox()
    {
        GameObject box = Instantiate(boxPrefab);

        Vector3 temp = transform.position;
        temp.z = 0f;

        box.transform.position = temp;
    }
}
