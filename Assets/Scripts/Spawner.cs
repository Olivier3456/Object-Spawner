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
        _tempGameObject = _objectsSpawned[0];
        _tempGameObject.transform.position = NewRandomPosition();

        _objectsSpawned.RemoveAt(0);

        _objectsSpawned.Add(_tempGameObject);
    }

    private void InstantiateNewObject()     // Avec couleur aléatoire.
    {
        _objectsSpawned.Add(Instantiate(_prefabToSpawn, NewRandomPosition(), Quaternion.identity));

        Color newColor = new Color(Random.Range(0, 1.0f), Random.Range(0, 1.0f), Random.Range(0, 1.0f));
        _objectsSpawned[_objectsSpawned.Count - 1].GetComponent<Renderer>().material.color = newColor;
        _objectsSpawned[_objectsSpawned.Count - 1].AddComponent<ObjectBehavior>();
    }

    private Vector3 NewRandomPosition()     // Position aléatoire dans une sphère qui a pour position _centreDeLaZoneDeSpawn :
    {
        return Random.insideUnitSphere * _rayonDeLaZoneDeSpawn + _centreDeLaZoneDeSpawn.position;
    }
}