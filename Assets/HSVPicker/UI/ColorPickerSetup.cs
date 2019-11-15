
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.HSVPicker
{
    [System.Serializable]
    public class ColorPickerSetup
    {
        public enum ColorHeaderShowing
        {
            Hide,
            ShowColor,
            ShowColorCode,
            ShowAll,
        }

        [System.Serializable]
        public class UiElements
        {
            public RectTransform[] Elements;


            public void Toggle(bool active)
            {
                for (int cnt = 0; cnt < Elements.Length; cnt++)
                {
                    Elements[cnt].gameObject.SetActive(active);
                }
            }

        }

        public bool ShowRgb = true;
        public bool ShowHsv;
        public bool ShowAlpha = true;
        public bool ShowColorBox = true;
        public bool ShowColorSliderToggle = true;

        public ColorHeaderShowing ShowHeader = ColorHeaderShowing.ShowAll;

        public UiElements RgbSliders;
        public UiElements HsvSliders;
        public UiElements ColorToggleElement;
        public UiElements ColorBox;
        public TextMeshProUGUI SliderToggleButtonText;

        public string PresetColorsId = "default";
        public Color[] DefaultPresetColors;
    }
}
