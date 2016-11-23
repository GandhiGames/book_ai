using UnityEngine;
using UnityEngine.UI;

namespace AdventureGame
{

    public class DamageNumbers : MonoBehaviour
    {
        public GameObject damageTextPrefab;

        public void ShowDamageNumber(int dmgAmount, Vector2 worldPos)
        {
            var textObj = (GameObject)Instantiate(damageTextPrefab);
            textObj.transform.SetParent(transform, true);
            textObj.GetComponent<DamageNumber>().Initialise(worldPos, dmgAmount.ToString());

        }

    }
}
