using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCube : MonoBehaviour
{

    [SerializeField] private GameObject _prefabToSpawn;

    [SerializeField] private Camera _cam;

    [SerializeField] private int _nombreMaxDObjets;
    private int _nombreDObjets;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButtonDown(0) && _nombreDObjets < _nombreMaxDObjets)
        {
            Vector3 positionObjectToSpawn = _cam.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, 25));

            Instantiate(_prefabToSpawn, positionObjectToSpawn, Quaternion.identity);
            _nombreDObjets++;
        }
        else if (_nombreDObjets >= _nombreMaxDObjets) Debug.Log("Nombre maximum d'objets atteint :" + _nombreDObjets);
    }
}
