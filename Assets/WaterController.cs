using UnityEngine;

public class WaterController : MonoBehaviour
{
    private bool isPlayerInside; // Переменная для отслеживания нахождения игрока внутри воды
    private Material waterMaterial; // Ссылка на материал воды

    private void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        waterMaterial = renderer.material;
    }

    private void Update()
    {
        // Меняем значение альфа-канала материала в зависимости от того, находится ли игрок внутри воды
        float alpha = isPlayerInside ? 0.5f : 1f;
        waterMaterial.SetFloat("_Alpha", alpha);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }
}