namespace Creazen.Wizard.Combat {
    using UnityEngine;
    
    public class Hand : MonoBehaviour {
        [SerializeField] Sprite emptyHand;
        [SerializeField] Sprite holdingWeaponHand;
        [SerializeField] SpriteRenderer weaponHolder;
        [SerializeField] SpriteRenderer hand;

        public void EquipWeapon(Sprite sprite, bool isFlipped, bool isHolding) {
            if(sprite == null) {
                hand.sprite = emptyHand;
            }
            else {
                hand.sprite = holdingWeaponHand;
            }
            if(isHolding) {
                weaponHolder.sprite = sprite;
                weaponHolder.transform.localScale = new Vector3(
                    isFlipped? -1 : 1,
                    weaponHolder.transform.localScale.y,
                    weaponHolder.transform.localScale.z
                );
            }
        }
    }
}