using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _nbreDObjetsParSeconde;

    [SerializeField] GameObject _prefabToSpawn;

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
        if (_nbreDObjetsParSeconde <= 0f) return;

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
        SetRandomColor(_objectsSpawned[_nombreMaxDObjets].transform);
    }

    private void InstantiateNewObject()     // Avec couleur aléatoire.
    {
        _objectsSpawned.Add(Instantiate(_prefabToSpawn, NewRandomPosition(), Quaternion.identity));

        SetRandomColor(_objectsSpawned[_objectsSpawned.Count - 1].transform);

        _objectsSpawned[_objectsSpawned.Count - 1].AddComponent<ObjectBehavior>();
    }

    private void SetRandomColor(Transform objectToRecolor)
    {
        //   Color newColor = new Color(Random.Range(0, 1.0f), Random.Range(0, 1.0f), Random.Range(0, 1.0f));  Une fonction existe pour la couleur aléatoire :

        objectToRecolor.GetComponent<Renderer>().material.color = Random.ColorHSV();
    }

    private Vector3 NewRandomPosition()     // Position aléatoire dans une sphère de rayon _rayonDeLaZoneDeSpawn,
    {                                       // et qui a pour position celle de l'objet Spawner.

        //   Vector3 localPosition = new Vector3(0, 0, 0);
        //   Vector3 WorldPosition = transform.TransformPoint(localPosition);     Passe de la position locale à la position globale. Ca revient au même que :

        return Random.insideUnitSphere * _rayonDeLaZoneDeSpawn + transform.position;
    }
}