using UnityEngine;

public class BootleHandle : MonoBehaviour
{
    public string acceptedSubstance; // �i�enin kabul etti�i madde t�r� (�rne�in, "H2O" veya "CO").
    public int requiredDrops = 1; // �i�e i�in gerekli toplam b�rakma say�s� (�rne�in, H2O i�in 2).
    private int currentDrops = 0; // �u ana kadar ka� kez madde b�rak�ld���.

    /// <summary>
    /// �i�eye madde b�rak�ld���nda �a�r�l�r.
    /// </summary>
    /// <param name="substanceType">B�rak�lan maddenin t�r�.</param>
    /// <param name="substanceAmount">B�rak�lan maddenin miktar�.</param>
    public void HandleSubstance(string substanceType, int substanceAmount)
    {
        if (currentDrops >= requiredDrops)
        {
            // �i�e zaten dolu.
            Debug.Log($"{gameObject.name}: �i�e zaten dolu!");
            return;
        }

        if (substanceType == acceptedSubstance)
        {
            // Do�ru madde �i�eye ekleniyor.
            currentDrops += substanceAmount;

            if (currentDrops > requiredDrops)
            {
                currentDrops = requiredDrops; // Fazla dolma durumunu engelle.
            }

            Debug.Log($"{gameObject.name}: {substanceType} kabul edildi! Toplam: {currentDrops}/{requiredDrops}");

            if (currentDrops >= requiredDrops)
            {
                Debug.Log($"{gameObject.name}: {substanceType} ile doldu!");
            }
        }
        else
        {
            // Yanl�� madde b�rak�ld�.
            Debug.Log($"{gameObject.name}: {substanceType} bu �i�e taraf�ndan kabul edilmiyor!");
        }
    }

    /// <summary>
    /// �i�enin dolup dolmad���n� kontrol eder.
    /// </summary>
    /// <returns>E�er �i�e dolduysa true, aksi halde false.</returns>
    public bool IsFilled()
    {
        return currentDrops >= requiredDrops;
    }
}
