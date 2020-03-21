using System.Linq;
using UnityEngine;

namespace Core.Tools.Bindings
{
  [ExecuteInEditMode]
  [RequireComponent(typeof(Renderer))]
  public class RendererSortingLayerBinder : MonoBehaviour
  {
    public bool _setParentCanvas;

    [ConditionalField("_setParentCanvas")]
    [SerializeField]
    private Canvas _parentCanvas;

    private Renderer _mesh;

    private void SetCanvasLayer()
    {
      if (_parentCanvas != null)
      {
        _mesh.sortingLayerName = _parentCanvas.sortingLayerName;
        _mesh.sortingOrder = _parentCanvas.sortingOrder;
      }
    }

    private void OnEnable()
    {
      SetCanvasLayer();
    }

    private void Awake()
    {
      if (_mesh == null)
        _mesh = GetComponent<Renderer>();

      if (_parentCanvas == null)
        _parentCanvas = GetComponentsInParent(typeof(Canvas), true).Cast<Canvas>().FirstOrDefault();

      SetCanvasLayer();
    }
  }
}