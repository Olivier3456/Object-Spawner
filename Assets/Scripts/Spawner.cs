using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _nbreDObjetsParSeconde;

    [SerializeField] GameObject _prefabToSpawn;

    private Transform _centreDeLaZoneDeSpawn;
    [SerializeField] private float _rayonDeLaZoneDeSpawn;

    [SerializeField] private List<GameObject> _objectsSpawned;
    [SerializeField] private int _nombreMaxDObjets;
    private GameObject _tempGameObject;
    
    private float _time;

    void Start()
    {
        _nombreMaxDObjets--;

        _centreDeLaZoneDeSpawn = transform;       
    }

    
    void Update()
    {
        _time += Time.deltaTime;

        if (_time >= (1 / _nbreDObjetsParSeconde))
        {
            if (_objectsSpawned.Count > _nombreMaxDObjets)
            {
                RelocateOldestObject();
            }
            else
            {
                InstantiateNewObject();
            }

            _time = 0;
        }
    }

    private void RelocateOldestObject()
    {
        _tempGameObject = _objectsSpawned[_nombreMaxDObjets];
        _tempGameObject.transform.position = NewRandomPosition();

        _objectsSpawned.Remove(_objectsSpawned[_nombreMaxDObjets]);

        _objectsSpawned.Insert(0, _tempGameObject);
    }

    private void InstantiateNewObject()
    {
        _objectsSpawned.Insert(0, Instantiate(_prefabToSpawn, NewRandomPosition(), Quaternion.identity));

        Color newColor = new Color(Random.Range(0, 1.0f), Random.Range(0, 1.0f), Random.Range(0, 1.0f));
        _objectsSpawned[0].GetComponent<Renderer>().material.color = newColor;
        _objectsSpawned[0].AddComponent<ObjectBehavior>();
    }

    private Vector3 NewRandomPosition()
    {
        return Random.insideUnitSphere * _rayonDeLaZoneDeSpawn + _centreDeLaZoneDeSpawn.position;
    }
}