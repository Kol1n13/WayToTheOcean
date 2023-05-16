using UnityEngine;

public class WaterController : MonoBehaviour
{
    private bool isPlayerInside; // ���������� ��� ������������ ���������� ������ ������ ����
    private Material waterMaterial; // ������ �� �������� ����

    private void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        waterMaterial = renderer.material;
    }

    private void Update()
    {
        // ������ �������� �����-������ ��������� � ����������� �� ����, ��������� �� ����� ������ ����
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