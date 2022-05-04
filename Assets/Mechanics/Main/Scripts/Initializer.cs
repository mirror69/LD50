using System;
using System.Collections;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Initializer : Singleton<Initializer>
{
    public static event Action InitializationCompleted;

    private bool _isLanguageSetted;

    public void Init()
    {
        if (_isLanguageSetted)
        {
            OnSetLanguageCompleted();
        }
        else
        {
            StartCoroutine(Preload(null, OnInitializeLanguageCompleted));
        }
    }

    private void OnInitializeLanguageCompleted()
    {
        Locale storedLocale = LocalizationSettings.AvailableLocales.Locales.Find(
            l => l.Identifier.Code == StoredGameDataManager.LanguageOptions.LocaleCode);

        if (storedLocale == null)
        {
            StoredGameDataManager.LanguageOptions.SetLocale(LocalizationSettings.SelectedLocale.Identifier.Code);
        }
        else if (storedLocale != LocalizationSettings.SelectedLocale)
        {
            StartCoroutine(Preload(storedLocale, OnSetLanguageCompleted));
            return;
        }

        OnSetLanguageCompleted();
    }

    private void OnSetLanguageCompleted()
    {
        _isLanguageSetted = true;
        InitializationCompleted?.Invoke();
    }

    private IEnumerator Preload(Locale locale, Action callback)
    {
        if (locale != null)
        {
            LocalizationSettings.SelectedLocale = locale;
        }

        var operation = LocalizationSettings.InitializationOperation;

        do
        {
            // When we first initialize the Selected Locale will not be available however
            // it is the first thing to be initialized and will be available before the InitializationOperation is finished.
            if (locale == null)
                locale = LocalizationSettings.SelectedLocaleAsync.Result;

            yield return null;
        }
        while (!operation.IsDone);

        callback?.Invoke();
    }
}
