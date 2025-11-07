using UnityEngine;

public class LoopMap : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    
    public float offsetSpeed = 1f;
    
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        Vector2 offset = Vector2.right * offsetSpeed * Time.deltaTime;

        meshRenderer.material.SetTextureOffset("_BaseMap", meshRenderer.material.mainTextureOffset + offset);
    }
}