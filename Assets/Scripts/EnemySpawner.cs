using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float interval;
    [SerializeField] private float range;

    private void OnEnable()
    {
        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(interval + range);
            GameObject instance = Instantiate(prefab, transform.position, Quaternion.identity);
            SortingGroup sortingGroup = instance.GetComponentInChildren<SortingGroup>();
            sortingGroup.sortingOrder = (int)transform.position.z + 1;
        }
    }
}
