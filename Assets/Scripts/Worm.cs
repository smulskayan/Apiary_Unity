using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class Worm : MonoBehaviour, IPointerClickHandler 
{
    public float minLifeTime = 0.1f, maxLifeTime = 1f, lifeTime = 0, timer = 0;
    public Sprite newSprite;
    private MiniAppCrop miniAppCrop;
    private Image image;

    void Start () {
        lifeTime = Random.Range(minLifeTime, maxLifeTime);
        image = GetComponent<Image>();
        miniAppCrop = GetComponentInParent<MiniAppCrop>();
    }

    void Update () {
        timer += Time.deltaTime;
        if(timer >= 1.5) 
        {
            Destroy(gameObject);
            print("Пропущен");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(ChangeSpriteAndDestroy());
    }

    IEnumerator ChangeSpriteAndDestroy()
    {
        image.sprite = newSprite;
        print("Попадание");
        if (miniAppCrop != null)
        {
            Player.money++;
            miniAppCrop.UpdateMoneyText();
            miniAppCrop.UpdateCaughtCountText();
        }
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}