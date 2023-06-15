using System.Linq;
using UnityEngine;
using DG.Tweening;

[ExecuteAlways]
public class CollectibleGrid : MonoBehaviour
{
    [Header("Placement Values")]
    [SerializeField] private Vector2Int size = Vector2Int.one;
    [SerializeField] private float gemHeight;
    [SerializeField] private float gemScale = 0.1f;

    [Header("Values")] 
    [SerializeField] private float gemGrowDuration = 5f;
    
    [Header("Other")]
    [SerializeField] private Material gridMat;
    [SerializeField] private GameObject[] availableGems;

    private Renderer _renderer;
    private Vector3[] _gemPositions;
    private Vector3 _gemEndScale;
    
    private void OnValidate()
    {
        SetMaterialTiling();    //Texture tiling
        InitGemPositions();     //Calculate Gem Positions
    }
    
    private void Start()
    {
        if (Application.isPlaying)
        {
            //Instantiate Gems
            _gemPositions.ToList().ForEach(x => CreateGem(x));
        } 
    }

    private void CreateGem(Vector3 pos)
    {
        Gem gem = Instantiate(
            availableGems.RandomItem(),
            pos,
            Quaternion.identity
        ).GetComponent<Gem>();
                
        gem.transform.localScale = Vector3.zero;
        gem.transform.SetParent(transform);
        StartGemGrow(gem);
    }

    private void SetMaterialTiling()
    {
        if (GetComponent<Renderer>() == null)
            _renderer = GetComponent<Renderer>();
        if (gridMat != null)
            GetComponent<Renderer>().material.mainTextureScale = (Vector2)size / 2f;
    }
    
    private void InitGemPositions()
    {
        _gemPositions = new Vector3[size.x * size.y];
        Bounds planeBounds = GetComponent<MeshRenderer>().bounds;

        //Length of a single grid
        Vector2 gridLength = new Vector3(
            planeBounds.size.x / size.x, 
            planeBounds.size.z / size.y
        );
        
        //Position of first gem
        Vector3 startPos = new Vector3(
            planeBounds.min.x + (planeBounds.size.x / (2 * size.x)), 
            gemHeight, 
            planeBounds.min.z + (planeBounds.size.z / (2 * size.y))
        );
        
        //Calculate each gem position
        for (int y = 0; y < size.y; y++) 
        {
            for (int x = 0; x < size.x; x++)
            {
                int index = (y * size.x) + x;
                
                Vector3 pos = startPos + new Vector3(
                    x * gridLength.x, 
                    0, 
                    y * gridLength.y
                );

                _gemPositions[index] = pos;
            }
        }
    }

    private void StartGemGrow(Gem gem)
    {
        Vector3 endScale = new Vector3(
            gemScale / transform.localScale.x,
            gemScale / transform.localScale.y,
            gemScale / transform.localScale.z);
        gem.transform.DOScale(endScale, 5).OnUpdate(() =>
        {
            float currentGemScale = gem.transform.localScale.x / transform.localScale.x;
            if (currentGemScale >= .25f)
                gem.GetComponent<Collider>().enabled = true;
        });
    }

    public void CollectGem(GameObject gem)
    {
        Vector3 pos = gem.transform.position;
        CreateGem(pos);
    }
}