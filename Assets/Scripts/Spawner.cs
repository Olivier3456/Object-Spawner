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
                RelocateOldestObject(PositionToSpawn());
            }
            else
            {
                InstantiateNewObject(PositionToSpawn());
            }

            _time = 0;
        }
    }

    private Vector3 PositionToSpawn()
    {
        float xRandom = OffsetAleatoire();
        float yRandom = OffsetAleatoire();
        float zRandom = OffsetAleatoire();

        Vector3 positionToSpawn = new Vector3(_centreDeLaZoneDeSpawn.position.x + xRandom,
                                              _centreDeLaZoneDeSpawn.position.y + yRandom,
                                              _centreDeLaZoneDeSpawn.position.z + zRandom);
        return positionToSpawn;
    }

    private void RelocateOldestObject(Vector3 positionToSpawn)
    {
        _tempGameObject = _objectsSpawned[_nombreMaxDObjets];
        _tempGameObject.transform.position = positionToSpawn;

        _objectsSpawned.Remove(_objectsSpawned[_nombreMaxDObjets]);

        _objectsSpawned.Insert(0, _tempGameObject);
    }

    private void InstantiateNewObject(Vector3 positionToSpawn)
    {
        _objectsSpawned.Insert(0, Instantiate(_prefabToSpawn, positionToSpawn, Quaternion.identity));

        Color newColor = new Color(Random.Range(0, 1.0f), Random.Range(0, 1.0f), Random.Range(0, 1.0f));
        _objectsSpawned[0].GetComponent<Renderer>().material.color = newColor;
    }

    private float OffsetAleatoire()
    {
        return Random.Range(_rayonDeLaZoneDeSpawn, -_rayonDeLaZoneDeSpawn);
    }
}
