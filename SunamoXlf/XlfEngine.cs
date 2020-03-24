using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using sunamo.Constants;
using sunamo.Essential;
using SunamoCode;
using XliffParser;

/// <summary>
/// Manage multilanguage strings in *.xlf files 
/// Specific methods for working with Xlf from InsertIntoXlfAndConstantCsUC
/// </summary>
public class XlfEngine
{
    static Type type = typeof(XlfEngine);

    #region Variables
    Dictionary<Langs, string> filesWithTranslation = new Dictionary<Langs, string>();
    public Langs l = Langs.cs;
    public bool requireUserDecision = false;
    /// <summary>
    /// Path to sunamo project (not solution)
    /// </summary>
    readonly string basePathXlf = null;
    /// <summary>
    /// XlfKeys.cs
    /// </summary>
    public readonly string pathXlfKeys = null;
    public static XlfEngine Instance = new XlfEngine();
    public const string CopyWhileMassAddingNameFolder = "CopyWhileMassAdding";
    public bool waitingForUserDecision = false;
    /// <summary>
    /// Must be global because HotKey has delegate for handling method
    /// </summary>
    public string pascal = null;
    #endregion

    #region Init
    private XlfEngine()
    {
        pathXlfKeys = FS.Combine(DefaultPaths.sunamo, @"sunamo\Constants\XlfKeys.cs");
        basePathXlf = FS.Combine(DefaultPaths.sunamo, "sunamo");
    }

    /// <summary>
    /// Externally called from many places
    /// </summary>
    public Dictionary<Langs, string> InitializeMultilingualResources()
    {
        Dictionary<Langs, string> filesWithTranslation = new Dictionary<Langs, string>();
        #region Load strings from MultilingualResources file
        var path = FS.Combine(basePathXlf, "MultilingualResources\\");
        var files = FS.GetFiles(path, "*.xlf", System.IO.SearchOption.TopDirectoryOnly);
        foreach (var item in files)
        {
            Langs l2 = XmlLocalisationInterchangeFileFormatSunamo.GetLangFromFilename(item);
            if (!filesWithTranslation.ContainsKey(l2))
            {
                filesWithTranslation.Add(l2, item);
            }
        }
        #endregion
        this.filesWithTranslation = filesWithTranslation;
        return filesWithTranslation;
    }
    #endregion

    public string englishTranslate = null;
    public CollectionWithoutDuplicates<string> en = null;
    public CollectionWithoutDuplicates<string> cs = null;

    #region Add
    public void AddCzech(string englishText, string key)
    {
        XmlLocalisationInterchangeFileFormat.Append(string.Empty, englishText, key, GetFile(Langs.cs));
    }

    public string GetFile(Langs cs)
    {
        if (filesWithTranslation.Count == 0)
        {
            filesWithTranslation = InitializeMultilingualResources();
        }
        return filesWithTranslation[cs];
    }

    public void AddEnglish( string englishText, string key)
    {
        XmlLocalisationInterchangeFileFormat.Append(string.Empty, englishText, key, GetFile(Langs.en));
    }
    #endregion

    #region Work with consts in XlfKeys
    /// <summary>
    /// Add to XlfKeys.cs from xlf
    /// Must manually call XlfResourcesH.SaveResouresToRL(DefaultPaths.sunamoProject) before
    /// called externally from MiAddTranslationWhichIsntInKeys_Click
    /// </summary>
    /// <param name="keysAll"></param>
    public void AddConsts(List<string> keysAll)
    {
        int first = -1;

        List<string> lines = null;
        var keys = GetConsts(out first, out lines);

        var both = CA.CompareList(keys, keysAll);
        AddKeysConsts(keysAll, first, lines);
    }


    /// <summary>
    /// Add c# const code
    /// </summary>
    /// <param name="csg"></param>
    /// <param name="item"></param>
    private static void AddConst(CSharpGenerator csg, string item)
    {
        csg.Field(1, AccessModifiers.Public, true, VariableModifiers.Mapped, "string", item, true, item);
    }

    /// <summary>
    /// Get consts which exists in XlfKeys.cs
    /// </summary>
    /// <param name="first"></param>
    public List<string> GetConsts(out int first)
    {
        List<string> lines = null;
        return GetConsts(out first, out lines);
    }

    /// <summary>
    /// Get consts which exists in XlfKeys.cs
    /// </summary>
    /// <param name="first"></param>
    /// <param name="lines"></param>
    public List<string> GetConsts(out int first, out List<string> lines)
    {
        first = -1;

        lines = TF.ReadAllLines(pathXlfKeys);

        var keys = CSharpParser.ParseConsts(lines, out first);
        return keys;
    }
    #endregion

    public void AddKeysConsts(List<string> keysAll, int first, List<string> lines)
    {
        CSharpGenerator csg = new CSharpGenerator();

        string append = string.Empty;

        foreach (var item in keysAll)
        {
            
            if (XmlLocalisationInterchangeFileFormat.IsToBeInXlfKeys(item))
            {
                append = string.Empty;
                if (char.IsDigit(item[0]))
                {
                    append = "_";
                }

                AddConst(csg, append + item);
            }
        }

        lines.Insert(first, csg.ToString());

        TF.SaveLines(lines, pathXlfKeys);
    }


    public  void RemoveFromXlfKeysWhichIsNotInXlfFile()
    {
        
        var path = XlfResourcesH.PathToXlfSunamo(Langs.en);



        var allids = XmlLocalisationInterchangeFileFormat.GetIds(path);

           CA.ChangeContent(allids, d2 => "public const string "+d2+" = \"" + d2 + "\";");

        var dxs2 = CA.ReturnWhichContainsIndexes(allids, CA.ToList<string>("\"Page\""));
        int s2 = 0;

        List<string> b;
        int a;
        XlfEngine.Instance.GetConsts(out a, out b);

        var b2 = b.ToList();
        CA.RemoveStringsEmpty2(b2);
        CA.Trim(b2);

        var dxs = CA.ReturnWhichContainsIndexes(b2, CA.ToList<string>("\"Page\""));
        int s = 0;

        var both = CA.CompareList(b2, allids);

        var mc = CA.ToList<string>("\"Page\"");

        var b1 = CA.ReturnWhichContainsIndexes(both, mc);
        var b4 = CA.ReturnWhichContainsIndexes(b2, mc);
        var b3 = CA.ReturnWhichContainsIndexes(allids, mc);

        CA.ChangeContent(allids, d4 => SH.GetTextBetween(d4, "const string ", " = \"", false));

        CSharpParser.RemoveConsts(XlfEngine.Instance.pathXlfKeys, b);

        AddKeysConsts(allids, a, b);
    }

    #region Methods


    /// <summary>
    /// Must be here coz use GetConsts - works with XlfKeys ant XlfEngine is only one class which can
    /// Return whether A1 is in XlfKeys
    /// if A2, save A1 to clipboard
    /// Externally called from InsertIntoXlfAndConstantCsUC.ClipboardMonitor_OnClipboardContentChanged
    /// </summary>
    /// <param name="pascal"></param>
    /// <param name="insertToClipboard"></param>
    public bool IsAlreadyContainedInXlfKeys(string pascal, bool insertToClipboard)
    {
        int first = -1;
        var keys = GetConsts(out first);

        if (keys.Contains(pascal))
        {
            if (insertToClipboard)
            {
                ClipboardHelper.SetText(pascal);
            }

            ThisApp.SetStatus(TypeOfMessage.Information, "Already " + pascal + " contained");
            return true;
        }
        return false;
    }

    /// <summary>
    /// Must be in XlfEngine coz use XlfDocument which is not imported in SunamoCode
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="l"></param>
    public void MergeWithAnotherXlf(string from, string to, Langs l)
    {
        var fileIdAlreadyExistsInXlf = AppData.ci.GetFile(AppFolders.Data, "AlreadyExistsInSunamoXlf_" + l + ".txt");
        PpkOnDrive ppk = new PpkOnDrive(fileIdAlreadyExistsInXlf);

        var dFrom = new XlfDocument(from);
        var dTo = new XlfDocument(to);

        var sFrom = XlfResourcesH.GetTransUnits(dFrom);
        var sTo = XlfResourcesH.GetTransUnits(dTo);

        foreach (var item in sFrom)
        {
            if (!sTo.ContainsKey(item.Key))
            {
                XmlLocalisationInterchangeFileFormat.Append(string.Empty, item.Value, item.Key, to);
            }
            else
            {
                var vTo = sTo[item.Key];
                ppk.Add(item.Key + "|" + item.Value);
            }
        }
    }
    #endregion
}