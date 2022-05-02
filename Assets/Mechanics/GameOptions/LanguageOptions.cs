using UnityEngine;

public class LanguageOptions
{
    public const string LanguageParamName = "Language";

    public string LocaleCode { get; private set; }

    public void SetLocale(string localeCode)
    {
        LocaleCode = localeCode;
        SaveToStorage();
    }

    public bool LoadFromStorage()
    {
        if (PlayerPrefs.HasKey(LanguageParamName))
        {
            LocaleCode = PlayerPrefs.GetString(LanguageParamName);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Сохранить параметры в хранилище
    /// </summary>
    private void SaveToStorage()
    {
        PlayerPrefs.SetString(LanguageParamName, LocaleCode);
    }
}
