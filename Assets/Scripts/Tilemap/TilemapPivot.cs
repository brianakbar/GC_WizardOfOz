namespace Creazen.Wizard.Tilemap {
    using UnityEngine;
    
    public class TilemapPivot : MonoBehaviour {
        [SerializeField] VerticalPivot verticalPivot;
        [SerializeField] HorizontalPivot horizontalPivot;
        [SerializeField] Vector2 offset;

        CompositeCollider2D coll2D;

        void Awake() {
            UpdatePivot();
        }

        void OnValidate() {
            UpdatePivot();
        }

        void OnDrawGizmosSelected() {
            UpdatePivot();
#if UNITY_EDITOR
            UnityEditor.Handles.DrawSolidDisc(transform.position, transform.forward, 0.1f);
#endif
        }

        public void UpdatePivotParameter(VerticalPivot vPivot, HorizontalPivot hPivot, Vector2 offset) {
            this.verticalPivot = vPivot;
            this.horizontalPivot = hPivot;
            this.offset = offset;
        }

        void UpdatePivot() {
            if(coll2D == null) coll2D = GetComponentInChildren<CompositeCollider2D>();
            coll2D.transform.position = Vector3.zero;
            transform.position = Vector3.zero;

            Vector3 pivot = GetPivot();
            coll2D.transform.position = -pivot;
            transform.position = pivot;

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        Vector3 GetPivot() {
            if(coll2D == null) coll2D = GetComponentInChildren<CompositeCollider2D>();

            if(!coll2D) return default;
            Vector3 pivotVectorFromCenter = new Vector3((float)horizontalPivot * coll2D.bounds.extents.x,
                                              (float)verticalPivot * coll2D.bounds.extents.y,
                                              coll2D.bounds.extents.z);
            Vector3 pivot = coll2D.bounds.center + pivotVectorFromCenter + (Vector3)offset;
            return pivot;
        }
    }
}
