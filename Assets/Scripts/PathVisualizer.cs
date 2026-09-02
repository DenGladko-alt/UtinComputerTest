using UnityEngine;

namespace Utin
{
    [ExecuteInEditMode]
        [RequireComponent(typeof(LineRenderer))]

    public class PathVisualizer : MonoBehaviour
    {
        [SerializeField] private Transform trackedTransform = null;
        [SerializeField] private Vector3 offset = Vector3.zero;

        private LineRenderer _line;

        private void OnValidate()
        {
            if (!_line) _line = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            UpdateLineRenderer();
        }

        private void UpdateLineRenderer()
        {
            _line.SetPosition(0, trackedTransform.position + offset);
            _line.widthMultiplier = trackedTransform.localScale.x;
        }
    }
}
