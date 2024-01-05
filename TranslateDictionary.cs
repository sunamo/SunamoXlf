using SunamoExceptions.OnlyInSE;

namespace SunamoXlf;

public class TranslateDictionary : IDictionary<string, string>
{
    private static Type type = typeof(TranslateDictionary);
    public static string basePathSolution = null;
    private Dictionary<string, string> _d = new Dictionary<string, string>();
    private Langs _l = Langs.en;

    public TranslateDictionary(Langs l)
    {
        _l = l;
    }

    public static Func<string, LocalizationLanguages, string> ReloadIfKeyWontBeFound;
    public static Action<string> ShowMb
    {
        get
        {
            throw new Exception("V tomhle je absolutní bordel. Nevím zda to PD potřebuji, ");
            //if (PD.delShowMb == null)
            //{
            //    System.Windows.Forms.MessageBox.Show("PD.delShowMb is null, return dummy method");
            //    return (s) => { };
            //}
            //return PD.delShowMb;
        }
        set
        {
            throw new Exception("V tomhle je absolutní bordel. Nevím zda to PD potřebuji, ");
            //PD.delShowMb = value;
        }
    }

    public static bool returnXlfKey = false;
    public static LocalizationLanguages localizationLanguages = null;

    public string this[string key]
    {
        get
        {
            if (returnXlfKey)
            {
                return key;
            }

            //ShowMb(_l + ": " + Count +" . Key was copied to clipboard");
            ////ClipboardHelper.SetText(Exc.GetStackTrace());
            //ClipboardHelper.SetText(SH.NullToStringOrDefault( key));

            if (!_d.ContainsKey(key))
            {
                //if (ReloadIfKeyWontBeFound == null)
                //{
                //    ShowMb("ReloadIfKeyWontBeFound is null");
                //}
                if (ReloadIfKeyWontBeFound == null)
                {
                    return ThrowNotFoundError(key, "ReloadIfKeyWontBeFound is null.");
                }
                var k = ReloadIfKeyWontBeFound(key, localizationLanguages);

                if (!_d.ContainsKey(key))
                {
                    //ShowMb(key + " is not in " + _l);

                    //XlfResourcesH.initialized = false;
                    //XlfResourcesH.SaveResouresToRL(basePathSolution);
                    return ThrowNotFoundError(key, string.Empty);
                    //return string.Empty;
                }
            }

            var value = _d[key];

            return value;
        }
        set => _d[key] = value;
    }

    private string ThrowNotFoundError(string key, string customErr)
    {
        if (string.IsNullOrEmpty(customErr))
        {
            ThrowEx.Custom(customErr + ". " + key + " is not in " + _l + " dictionary");
            return null;
        }
        else
        {
            ThrowEx.Custom(customErr + ". " + key + " is not in " + _l + " dictionary");
            return null;
        }

    }

    public ICollection<string> Keys => _d.Keys;

    public ICollection<string> Values => _d.Values;

    public int Count => _d.Count;

    public bool IsReadOnly => false;

    public void Add(string key, string value)
    {
        _d.Add(key, value);
    }

    public void Add(KeyValuePair<string, string> item)
    {
        _d.Add(item.Key, item.Value);
    }

    public void Clear()
    {
        _d.Clear();
    }

    public bool Contains(KeyValuePair<string, string> item)
    {
        return _d.ContainsKey(item.Key);
    }

    public bool ContainsKey(string key)
    {
        return _d.ContainsKey(key);
    }

    /// <summary>
    /// Copy elements to A1 from A2
    /// </summary>
    /// <param name="array"></param>
    /// <param name="arrayIndex"></param>
    public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
    {
        ThrowEx.NotImplementedMethod();
        // Doufám že tuto metodu nebudu potřebovat, byla ve SunamoExceptions než prošlo nutným zeštíhlením
        //DictionaryHelper.CopyTo<string, string>(_d, array, arrayIndex);
    }

    public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
    {
        return _d.GetEnumerator();
    }

    public bool Remove(string key)
    {
        return _d.Remove(key);
    }

    public bool Remove(KeyValuePair<string, string> item)
    {
        return _d.Remove(item.Key);
    }

    public bool TryGetValue(string key, out string value)
    {
        return _d.TryGetValue(key, out value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _d.GetEnumerator();
    }
}
