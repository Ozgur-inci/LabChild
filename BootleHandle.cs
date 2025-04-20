using UnityEngine;

public class BootleHandle : MonoBehaviour
{
    public string acceptedSubstance; // Þiþenin kabul ettiði madde türü (örneðin, "H2O" veya "CO").
    public int requiredDrops = 1; // Þiþe için gerekli toplam býrakma sayýsý (örneðin, H2O için 2).
    private int currentDrops = 0; // Þu ana kadar kaç kez madde býrakýldýðý.

    /// <summary>
    /// Þiþeye madde býrakýldýðýnda çaðrýlýr.
    /// </summary>
    /// <param name="substanceType">Býrakýlan maddenin türü.</param>
    /// <param name="substanceAmount">Býrakýlan maddenin miktarý.</param>
    public void HandleSubstance(string substanceType, int substanceAmount)
    {
        if (currentDrops >= requiredDrops)
        {
            // Þiþe zaten dolu.
            Debug.Log($"{gameObject.name}: Þiþe zaten dolu!");
            return;
        }

        if (substanceType == acceptedSubstance)
        {
            // Doðru madde þiþeye ekleniyor.
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
            // Yanlýþ madde býrakýldý.
            Debug.Log($"{gameObject.name}: {substanceType} bu þiþe tarafýndan kabul edilmiyor!");
        }
    }

    /// <summary>
    /// Þiþenin dolup dolmadýðýný kontrol eder.
    /// </summary>
    /// <returns>Eðer þiþe dolduysa true, aksi halde false.</returns>
    public bool IsFilled()
    {
        return currentDrops >= requiredDrops;
    }
}
