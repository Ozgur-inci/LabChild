using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropped : MonoBehaviour
{
    public string substanceType; // Maddenin türü (örneðin, H2O, CO, NH3).
    public int substanceAmount; // Maddenin gerekli oraný (örneðin, 1 veya 2).
    private Vector3 startPosition; // Baþlangýç pozisyonu.
    private Camera mainCamera; // Dokunma pozisyonunu almak için kamera.
    private bool isDragging = false; // Sürükleme iþlemi kontrolü.

    private void Start()
    {
        mainCamera = Camera.main; // Ana kamerayý referans al.
        startPosition = transform.position; // Baþlangýç pozisyonunu kaydet.
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Ýlk dokunmayý al.

            if (touch.phase == TouchPhase.Began)
            {
                // Dokunma baþladýðýnda dokunulan nesneyi kontrol et.
                Vector3 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0;

                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    isDragging = true; // Sürükleme baþlatýlýr.
                }
            }

            if (touch.phase == TouchPhase.Moved && isDragging)
            {
                // Sürükleme sýrasýnda nesneyi dokunma pozisyonuna taþý.
                Vector3 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0;
                transform.position = touchPosition;
            }

            if (touch.phase == TouchPhase.Ended && isDragging)
            {
                // Dokunma sona erdiðinde býrakma iþlemini kontrol et.
                isDragging = false;

                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero);

                if (hit.collider != null && hit.collider.CompareTag("EmptyBottle"))
                {
                    BootleHandle bottleHandler = hit.collider.GetComponent<BootleHandle>();

                    if (bottleHandler != null)
                    {
                        // Þiþe ile etkileþim kur ve madde bilgilerini ilet.
                        bottleHandler.HandleSubstance(substanceType, substanceAmount);
                    }
                }

                // Her durumda madde baþlangýç pozisyonuna geri döner.
                ResetPosition();
            }
        }
    }

    /// <summary>
    /// Nesneyi baþlangýç pozisyonuna sýfýrlar.
    /// </summary>
    private void ResetPosition()
    {
        transform.position = startPosition;
    }
}
