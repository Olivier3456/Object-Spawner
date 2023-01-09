using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCube : MonoBehaviour
{
    [SerializeField] private Camera _cam;

    [SerializeField] private GameObject _prefabToSpawn;
    [SerializeField] private List<GameObject> _objectsSpawned;
    [SerializeField] private int _nombreMaxDObjets;
    
    private GameObject _tempGameObject;

    private void Start()
    {
        _nombreMaxDObjets--;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 positionObjectToSpawn = _cam.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, 25));

            if (_objectsSpawned.Count > _nombreMaxDObjets)
            {
                RelocateOldestObject(positionObjectToSpawn);
            }
            else
            {
                InstantiateNewObject(positionObjectToSpawn);
            }
        }
    }

    private void InstantiateNewObject(Vector3 positionObjectToSpawn)
    {
        _objectsSpawned.Add(Instantiate(_prefabToSpawn, positionObjectToSpawn, Quaternion.identity));
    }

    private void RelocateOldestObject(Vector3 positionObjectToSpawn)
    {
        _tempGameObject = _objectsSpawned[0];
        _tempGameObject.transform.position = positionObjectToSpawn;

        _objectsSpawned.RemoveAt(0);

        _objectsSpawned.Add(_tempGameObject);
    }
}
