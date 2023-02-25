using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARImageTracker : MonoBehaviour
{
    public Text debugt;
    public Text debugt2;
    public OnScreenObjectManager onScreenObjectManager;
    private ARTrackedImageManager trackedImageManager;
    [SerializeField]
    private GameObject[] _markerPointers;

    private Dictionary<string, GameObject> spawnedObjects;

    Complex complex;
    private void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
        spawnedObjects = new Dictionary<string, GameObject>();

        foreach (GameObject obj in _markerPointers)
        {
            GameObject newObject = Instantiate(obj,Vector3.zero,Quaternion.identity);
            newObject.name = obj.name;
            newObject.SetActive(false);

            spawnedObjects.Add(newObject.name, newObject);
        }
        complex = spawnedObjects["complex"].GetComponent<Complex>();
    }
    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += onTrackedImageChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= onTrackedImageChanged;
    }

    void onTrackedImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateSpawnObject(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateSpawnObject(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            spawnedObjects[trackedImage.referenceImage.name].SetActive(false);
            debugt2.text = $"removed:{trackedImage.name}";
        }
    }

    void UpdateSpawnObject(ARTrackedImage trackedImage)
    {
        string referenceImageName = trackedImage.referenceImage.name;
        if (onScreenObjectManager.ARok)
        {
            spawnedObjects[referenceImageName].transform.position = trackedImage.transform.position;
            spawnedObjects[referenceImageName].transform.rotation = trackedImage.transform.rotation;

            spawnedObjects[referenceImageName].SetActive(true);
        }
        else
        {
            spawnedObjects[referenceImageName].SetActive(false);
        }

    }
    public GameObject GetSpwan(string name)
    {
        return spawnedObjects[name].gameObject;
    }
    public Complex GetComplex()
    {
        return complex;
    }
    void Start()
    {

    }

    void Update()
    {
        debugt.text = $"There are {trackedImageManager.trackables.count} images being tracked";

        foreach (var trakedImage in trackedImageManager.trackables)
        {
            debugt2.text = $"image : {trakedImage.referenceImage.name}is at +" +
                $"{trakedImage.transform.position}";
        }
    }
}
