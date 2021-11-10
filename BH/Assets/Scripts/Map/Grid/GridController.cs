using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridController : MonoBehaviour
{
    [SerializeField] private TilemapRenderer groundTilemapRenderer = null;
    [SerializeField] private TilemapRenderer overlayGroundTilemapRenderer = null;
    [SerializeField] private TilemapRenderer LowCollidersTilemapRenderer = null;
    [SerializeField] private TilemapRenderer HighCollidersTilemapRenderer = null;

    private void Awake()
    {
        groundTilemapRenderer.sortingOrder = Constant.GroundOrderLayer;
        overlayGroundTilemapRenderer.sortingOrder = Constant.GroundOverlayOrderLayer;
        LowCollidersTilemapRenderer.sortingOrder = Constant.LowCollidersOrderLayer;
        HighCollidersTilemapRenderer.sortingOrder = Constant.HighCollidersOrderLayer;
    }
}
