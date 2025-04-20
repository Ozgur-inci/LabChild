using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropped : MonoBehaviour
{
    public string substanceType; // Maddenin t�r� (�rne�in, H2O, CO, NH3).
    public int substanceAmount; // Maddenin gerekli oran� (�rne�in, 1 veya 2).
    private Vector3 startPosition; // Ba�lang�� pozisyonu.
    private Camera mainCamera; // Dokunma pozisyonunu almak i�in kamera.
    private bool isDragging = false; // S�r�kleme i�lemi kontrol�.

    private void Start()
    {
        mainCamera = Camera.main; // Ana kameray� referans al.
        startPosition = transform.position; // Ba�lang�� pozisyonunu kaydet.
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // �lk dokunmay� al.

            if (touch.phase == TouchPhase.Began)
            {
                // Dokunma ba�lad���nda dokunulan nesneyi kontrol et.
                Vector3 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0;

                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    isDragging = true; // S�r�kleme ba�lat�l�r.
                }
            }

            if (touch.phase == TouchPhase.Moved && isDragging)
            {
                // S�r�kleme s�ras�nda nesneyi dokunma pozisyonuna ta��.
                Vector3 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0;
                transform.position = touchPosition;
            }

            if (touch.phase == TouchPhase.Ended && isDragging)
            {
                // Dokunma sona erdi�inde b�rakma i�lemini kontrol et.
                isDragging = false;

                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero);

                if (hit.collider != null && hit.collider.CompareTag("EmptyBottle"))
                {
                    BootleHandle bottleHandler = hit.collider.GetComponent<BootleHandle>();

                    if (bottleHandler != null)
                    {
                        // �i�e ile etkile�im kur ve madde bilgilerini ilet.
                        bottleHandler.HandleSubstance(substanceType, substanceAmount);
                    }
                }

                // Her durumda madde ba�lang�� pozisyonuna geri d�ner.
                ResetPosition();
            }
        }
    }

    /// <summary>
    /// Nesneyi ba�lang�� pozisyonuna s�f�rlar.
    /// </summary>
    private void ResetPosition()
    {
        transform.position = startPosition;
    }
}
