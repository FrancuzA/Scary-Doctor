using System.Collections.Generic;
using UnityEngine;

public class DecalsOnWalls : MonoBehaviour
{
    public List<GameObject> Decals;
    public Vector3 DecalPlacement;
    public Transform WallTransform;
    public float DecalSpawnChance;
    public float DecalRotationAmount;
    public float MinZ;
    public float MinY;
    public float MaxZ;
    public float MaxY;
    void Start()
    {
        RollForDecal();
    }


    public void RollForDecal()
    {
        if (RNG_Custom.random.NextDouble() <= DecalSpawnChance && Decals.Count > 0)
        {
            int randomIndex = RNG_Custom.random.Next(0, Decals.Count);
            GameObject selectedDecal = Decals[randomIndex];

            PlaceDecal(selectedDecal);
        }
    }

    public void PlaceDecal(GameObject Decal)
    {
        float RandomZ = RNG_Custom.NextFloat(MinZ, MaxZ);
        float RandomY = RNG_Custom.NextFloat(MinY, MaxY);
        DecalPlacement =new Vector3(2f, RandomY, RandomZ);
        Quaternion prefabRotation = Decal.transform.rotation;
        float DecalRotationAmount = RNG_Custom.NextFloat(-15f, 15f);
        Quaternion zAxisRotation = Quaternion.Euler(0f, 0f, DecalRotationAmount);
        Quaternion finalRotation = prefabRotation * zAxisRotation;
        Debug.Log(finalRotation);
        GameObject PlacedDecal = Instantiate(Decal, DecalPlacement, finalRotation, WallTransform);
        PlacedDecal.transform.localPosition = DecalPlacement;
    }
}
