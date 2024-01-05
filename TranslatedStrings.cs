using SunamoXlf._sunamo;

namespace SunamoXlf;

/// <summary>
/// For using in content template etc
/// </summary>
public class TranslatedStrings
{
    public static TranslatedStrings Instance = new TranslatedStrings();
    static Type type = typeof(TranslatedStrings);

    private TranslatedStrings()
    {

    }

    public Func<string, string> get = null;

    public void FillIfIsEmpty(string k)
    {
        var v = RHSunExcXlf.GetValueOfProperty(k, type, Instance, false);

        if (v.ToString() == string.Empty)
        {
            var tr = get(k);
            RHSunExcXlf.SetValueOfProperty(k, type, Instance, false, tr);
            //v = RH.GetValueOfProperty(k, type, Instance, false);
        }
    }

    public string SetAsDefault { get; set; } = string.Empty;

    public string Delete { get; set; } = string.Empty;
}
