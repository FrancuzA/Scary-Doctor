using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DecalsOnFloor : MonoBehaviour
{
    public List<GameObject> Decals;
    public Vector3 DecalPlacement;
    public Transform WallTransform;
    public float DecalSpawnChance;
    public float MinZ;
    public float MinX;
    public float MaxZ;
    public float MaxX;
    void Start()
    {
        RollForDecal();
    }


    public void RollForDecal()
    {
        if (UnityEngine.Random.value <= DecalSpawnChance && Decals.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, Decals.Count);
            GameObject selectedDecal = Decals[randomIndex];

            PlaceDecal(selectedDecal);
        }
    }

    public void PlaceDecal(GameObject Decal)
    {
        float RandomZ = UnityEngine.Random.Range(MinZ, MaxZ);
        float RandomX = UnityEngine.Random.Range(MinX, MaxX);
        DecalPlacement = new Vector3(RandomX,0.25f , RandomZ);
        Quaternion prefabRotation = Decal.transform.rotation;
        float DecalRotationAmount = UnityEngine.Random.Range(-15f, 15f);
        Quaternion yAxisRotation = Quaternion.Euler(0f,DecalRotationAmount, 0f);
        Quaternion finalRotation = prefabRotation * yAxisRotation;
        GameObject PlacedDecal = Instantiate(Decal, DecalPlacement, finalRotation, WallTransform);
        PlacedDecal.transform.localPosition = DecalPlacement;
    }
}
