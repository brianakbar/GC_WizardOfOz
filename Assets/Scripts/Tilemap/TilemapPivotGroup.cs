namespace Creazen.Wizard.Tilemap {
    using UnityEngine;
    
    public class TilemapPivotGroup : MonoBehaviour {
        [SerializeField] VerticalPivot verticalPivot;
        [SerializeField] HorizontalPivot horizontalPivot;
        [SerializeField] Vector2 offset;

        void OnValidate() {
            UpdateChildPivotParameter();
        }

        void UpdateChildPivotParameter() {
            foreach(Transform child in transform) {
                if(child.TryGetComponent<TilemapPivot>(out TilemapPivot tilemapPivot)) {
                    tilemapPivot.UpdatePivotParameter(verticalPivot, horizontalPivot, offset);
#if UNITY_EDITOR
                    UnityEditor.EditorUtility.SetDirty(tilemapPivot);
#endif
                }
                if(child.TryGetComponent<TilemapPivotGroup>(out TilemapPivotGroup tilemapPivotGroup)) {
                    tilemapPivotGroup.UpdatePivotParameter(verticalPivot, horizontalPivot, offset);
#if UNITY_EDITOR
                    UnityEditor.EditorUtility.SetDirty(tilemapPivotGroup);
#endif
                }
            }
        }

        public void UpdatePivotParameter(VerticalPivot vPivot, HorizontalPivot hPivot, Vector2 offset) {
            this.verticalPivot = vPivot;
            this.horizontalPivot = hPivot;
            this.offset = offset;
            UpdateChildPivotParameter();
        }
    }
}