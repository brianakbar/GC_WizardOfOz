namespace Creazen.Wizard.Combat {
    using UnityEngine;
    
    public class Hand : MonoBehaviour {
        [SerializeField] Sprite emptyHand;
        [SerializeField] Sprite holdingWeaponHand;
        [SerializeField] SpriteRenderer weaponHolder;
        [SerializeField] SpriteRenderer hand;

        public void EquipWeapon(Sprite sprite) {
            if(sprite == null) {
                hand.sprite = emptyHand;
            }
            else {
                hand.sprite = holdingWeaponHand;
            }
            weaponHolder.sprite = sprite;
        }
    }
}