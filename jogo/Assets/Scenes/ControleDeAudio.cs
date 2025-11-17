using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ControleDeAudio : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Text texto;
    public Slider slider;

    float masterVolume;

    void Start()
    {
        audioMixer.GetFloat("Master", out masterVolume);

        slider.value = masterVolume;
        AtualizarVolume();
    }

    void Update()
    {
        // Ajuste pelo teclado
        if (Input.GetKeyDown(KeyCode.A))
            slider.value += 1f;

        if (Input.GetKeyDown(KeyCode.S))
            slider.value -= 1f;

        masterVolume = slider.value;

        AtualizarVolume();
        AtualizarTexto();
    }

    void AtualizarVolume()
    {
        if (masterVolume <= -20f)
            audioMixer.SetFloat("Master", -80f);
        else
            audioMixer.SetFloat("Master", masterVolume);
    }

    void AtualizarTexto()
    {
        // Converte o valor dB para porcentagem visual (0% a 100%)
        float porcentagem = Mathf.InverseLerp(-20f, 0f, masterVolume) * 100f;
        porcentagem = Mathf.Clamp(porcentagem, 0, 100);

        texto.text = $"Música: {porcentagem:F0}%";
    }
}