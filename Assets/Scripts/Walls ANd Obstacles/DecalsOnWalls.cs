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
    void OnEnable()
    {
        RollForDecal();
    }


    private void RollForDecal()
    {
        if (UnityEngine.Random.value <= DecalSpawnChance && Decals.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, Decals.Count);
            GameObject selectedDecal = Decals[randomIndex];

            PlaceDecal(selectedDecal);
        }
    }

    private void PlaceDecal(GameObject Decal)
    {
        float RandomZ = UnityEngine.Random.Range(MinZ, MaxZ);
        float RandomY = UnityEngine.Random.Range(MinY, MaxY);
        DecalPlacement =new Vector3(2f, RandomY, RandomZ);
        Quaternion prefabRotation = Decal.transform.rotation;
        float DecalRotationAmount = UnityEngine.Random.Range(-15f, 15f);
        Quaternion zAxisRotation = Quaternion.Euler(0f, 0f, DecalRotationAmount);
        Quaternion finalRotation = prefabRotation * zAxisRotation;
        Debug.Log(finalRotation);
        GameObject PlacedDecal = Instantiate(Decal, DecalPlacement, finalRotation, WallTransform);
        PlacedDecal.transform.localPosition = DecalPlacement;
    }
}
