using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCube : MonoBehaviour
{
    [SerializeField] private Camera _cam;
       
    [SerializeField] private GameObject _prefabToSpawn;
    [SerializeField] private List<GameObject> _objectsSpawned;
    [SerializeField] private int _nombreMaxDObjets;
    private int _indexObjects = 0;


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
                Destroy(_objectsSpawned[_nombreMaxDObjets]);           
                _indexObjects = _nombreMaxDObjets;
            }

            _objectsSpawned.Insert(0, Instantiate(_prefabToSpawn, positionObjectToSpawn, Quaternion.identity));
            _indexObjects++;
        }    
    }
}
