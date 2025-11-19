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
        // Começa sempre em 100% (0 dB)
        masterVolume = 0f;
        slider.value = masterVolume;
        audioMixer.SetFloat("Master", masterVolume);

        AtualizarTexto();
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
        float porcentagem = Mathf.InverseLerp(-20f, 0f, masterVolume) * 100f;
        porcentagem = Mathf.Clamp(porcentagem, 0, 100);

        texto.text = $"Música: {porcentagem:F0}%";
    }
} 