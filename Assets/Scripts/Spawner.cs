using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _nbreDObjetsParSeconde;

    [SerializeField] GameObject _prefabToSpawn;

    [SerializeField] private Transform _centreDeLaZoneDeSpawn;
    [SerializeField] private float _rayonDeLaZoneDeSpawn;

    [SerializeField] private List<GameObject> _objectsSpawned;
    [SerializeField] private int _nombreMaxDObjets;
    private GameObject _tempGameObject;


    private float _time;

    void Start()
    {
        _nombreMaxDObjets--;
    }


    void Update()
    {
        _time += Time.deltaTime;

        if (_time >= (1 / _nbreDObjetsParSeconde))
        {
            float xRandom = OffsetAleatoire();
            float yRandom = OffsetAleatoire();
            float zRandom = OffsetAleatoire();


            Vector3 positionToSpawn = new Vector3(_centreDeLaZoneDeSpawn.position.x + xRandom,
                                                  _centreDeLaZoneDeSpawn.position.y + yRandom,
                                                  _centreDeLaZoneDeSpawn.position.z + zRandom);

            if (_objectsSpawned.Count > _nombreMaxDObjets)
            {
                _tempGameObject = _objectsSpawned[_nombreMaxDObjets];
                _tempGameObject.transform.position = positionToSpawn;

                _objectsSpawned.Remove(_objectsSpawned[_nombreMaxDObjets]);

                _objectsSpawned.Insert(0, _tempGameObject);
            }
            else
            {
                _objectsSpawned.Insert(0, Instantiate(_prefabToSpawn, positionToSpawn, Quaternion.identity));

                Color newColor = new Color(Random.Range(0, 1.0f), Random.Range(0, 1.0f), Random.Range(0, 1.0f));
                _objectsSpawned[0].GetComponent<Renderer>().material.color = newColor;
            }

            _time = 0;
        }
    }



    private float OffsetAleatoire()
    {
        return Random.Range(_rayonDeLaZoneDeSpawn, -_rayonDeLaZoneDeSpawn);
    }
}
