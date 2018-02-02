using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer))]
public class Selectable : MonoBehaviour, IPointerUpHandler
{
    [SerializeField]
    GameObject prefab;

    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        SpriteRenderer prefabRenderer = prefab.GetComponent<SpriteRenderer>();

        //ToDo: change collider shape based on sprite?
        gameObject.AddComponent<CircleCollider2D>();

        if (prefabRenderer != null)
            spriteRenderer.sprite = prefabRenderer.sprite;
    }

    public void OnPointerUp(PointerEventData data)
    {
        if (data.pointerId == -1) //Left Click
        {
            LevelEditorController.Instance.SetCurrentPrefab(prefab);
        }
        else if (data.pointerId == -2)
        {
            //??
        }
        else if (data.pointerId == -3)
        {
            Debug.Log("can I get a triple?"); //YES I CAN
        }
    }
}
