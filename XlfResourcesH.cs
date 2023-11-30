/// <summary>
/// Must be in shared
/// In sunamo is not XliffParser and fmdev.ResX - these projects requires .net fw due to CodeDom
/// </summary>
public partial class XlfResourcesH
{
    public static bool initialized = false;
    static Type type = typeof(XlfResourcesH);

    public static string PathToXlfSunamo(Langs l)
    {
        var p = BasePathsHelper.vs + @"sunamo\sunamo\MultilingualResources\sunamo.";
        switch (l)
        {
            case Langs.cs:
                p += "cs-CZ";
                break;
            case Langs.en:
                p += "en-US";
                break;
            default:
                ThrowEx.NotImplementedCase(l);
                break;
        }

        return p + ".xlf";
    }

    static string previousKey = null;

    #region Main worker
    #region Less sophisficated - Loading always from file
    ///// <summary>
    ///// 2. loading from xlf files
    ///// </summary>
    ///// <typeparam name="StorageFolder"></typeparam>
    ///// <typeparam name="StorageFile"></typeparam>
    ///// <param name="basePath"></param>
    ///// <param name="existsDirectory"></param>
    ///// <param name="appData"></param>
    //public static string SaveResouresToRL<StorageFolder, StorageFile>(string key, string basePath, ExistsDirectory existsDirectory)
    //{
    //    if (previousKey == key && previousKey != null)
    //    {
    //        return null;
    //    }

    //    previousKey = key;

    //    // cant be inicialized - after cs is set initialized to true and skip english
    //    //initialized = true;

    //    var path = Path.Combine(basePath, "MultilingualResources");

    //    var files = FS.GetFiles(path, "*.xlf", SearchOption.TopDirectoryOnly);
    //    foreach (var file3 in files)
    //    {
    //        var lang = XmlLocalisationInterchangeFileFormatXlf.GetLangFromFilename(file3);
    //        ProcessXlfFile(path, lang.ToString(), file3);
    //    }



    //    return key;
    //}
    #endregion

    #region More sophisficated - If is not my computer, reading from resources
    /// <summary>
    /// A2 can be string.Empty
    /// </summary>
    /// <typeparam name="StorageFolder"></typeparam>
    /// <typeparam name="StorageFile"></typeparam>
    /// <param name="key"></param>
    /// <param name="basePath"></param>
    /// <param name="existsDirectory"></param>
    /// <param name="appData"></param>
    /// <returns></returns>
    public static string SaveResouresToRL<StorageFolder, StorageFile>(string key, string basePath, LocalizationLanguages ll)
    {
        if (previousKey == key && previousKey != null)
        {
            return null;
        }

        previousKey = key;

        // cant be inicialized - after cs is set initialized to true and skip english
        //initialized = true;

        var path = Path.Combine(basePath, "MultilingualResources");

        

        //throw new System.Exception("jinak");

        //Type type = typeof(Resources.ResourcesDuo);

        // ResourcesHelper rm = ResourcesHelper.Create("Resources.ResourcesDuo", type.Assembly);

        // #region 1) Loading direct from resources
        //var xlfContentCs =  rm.GetByteArray(Fn(Langs.cs));
        //var xlfContentEn = rm.GetByteArray(Fn(Langs.en));

        ProcessXlfContent(Langs.cs, ll.Cs);
        ProcessXlfContent(Langs.en, ll.En);
        // #endregion

        #region 2) Loading from files - obsolete
        //var exists = false;

        //exists = MyPc.Instance.IsMyComputerOrVps();


        //if (appData == null)
        //{
        //    // Is web app
        //    exists = true;
        //}

        ////exists = false;

        //// Pokud je můj PC nebo appData existují, neukládám na disk abych z něho mohl číst. v opačném případě ukládám

        //// This is totally important
        //// Otherwise is loading in non UWP apps from resx
        //if (!exists)
        //{
        //    var fn = "sunamo_cs_CZ";

        //    // Always true, in all apps I use _min. NEVER CHANGE IT!!!
        //    if (true) // PlatformInteropHelperXlf.IsSellingApp())
        //    {
        //        fn += "_min";
        //    }

        //    var file = appData.GetFileCommonSettings(fn + ".xlf");

        //    // Cant use StorageFile.ToString - get only name of method
        //    //pathFile = file.ToString();

        //    var enc = Encoding.GetEncoding(65001);

        //    string xlfContentCs = rm.GetByteArrayAsString(fn);

        //    FS.CreateUpfoldersPsysicallyUnlessThere(file);
        //    //xlfContent = xlfContent.Skip(3);
        //    TF.WriteAllText(file, xlfContentCs, enc);
        //    TFXlf.RemoveDoubleBomUtf8(file);

        //    fn = "sunamo_en_US";

        //    // Always true, in all apps I use _min. NEVER CHANGE IT!!!

        //    if (true) //PlatformInteropHelperXlf.IsSellingApp())
        //    {
        //        fn += "_min";
        //    }

        //    var file2 = appData.GetFileCommonSettings(fn + ".xlf");

        //    string xlfContentEn = rm.GetByteArrayAsString(fn);
        //    TF.WriteAllText(file2, xlfContentEn, enc);
        //    TFXlf.RemoveDoubleBomUtf8(file2);

        //    path = Path.Combine(appData.RootFolderCommon(true), "Settings");
        //}

        //ProcessXlfFiles(path); 
        #endregion

        return key;
    }

    public static Dictionary<string, string> GetTransUnits(XlfDocument doc)
    {
        Dictionary<string, string> result = new Dictionary<string, string>();

        var xlfFiles = doc.Files;
        if (xlfFiles.Count() != 0)
        {
            // Win every xlf will be t least two WPF.TESTS/PROPERTIES/RESOURCES.RESX and WPF.TESTS/RESOURCES/EN-US.RESX
            foreach (var item in xlfFiles)
            {
                // like WPF.TESTS/PROPERTIES/
                if (item.Original.EndsWith("/RESOURCES.RESX"))
                {
                    if (item.TransUnits.Count() > 0)
                    {

                        Debugger.Break();
                    }
                }

                foreach (var tu in item.TransUnits)
                {
                    if (!result.ContainsKey(tu.Id))
                    {
                        result.Add(tu.Id, tu.Target);
                    }
                }
            }
        }

        return result;
    }

    private static void ProcessXlfContent(Langs lang2, string content)
    {
        bool isCzech = lang2 == Langs.cs;
        bool isEnglish = lang2 == Langs.en;

        var doc = new XlfDocument();
        doc.LoadXml(content);
        var lang = lang2.ToString().ToLower();

        var xlfFiles = doc.Files.Where(d => d.Original.ToLower().Contains(lang));
        if (xlfFiles.Count() != 0)
        {
            var xlfFile = xlfFiles.First();

            foreach (var u in xlfFile.TransUnits)
            {
                if (isCzech)
                {
                    if (!RLData.cs.ContainsKey(u.Id))
                    {
                        RLData.cs.Add(u.Id, u.Target);
                    }
                }
                else if (isEnglish)
                {
                    if (!RLData.en.ContainsKey(u.Id))
                    {
                        RLData.en.Add(u.Id, u.Target);
                    }
                }
                else
                {
                    //ThrowEx.Custom("Invalid file " + file + ", please delete it");
                }
            }
        }
    }

    private static string Fn(Langs cs)
    {
        string fn = null;
        switch (cs)
        {
            case Langs.cs:
                fn = "sunamo_cs_CZ_min";
                break;
            case Langs.en:
                fn = "sunamo_en_US_min";
                break;
            default:
                ThrowEx.NotImplementedCase(cs);
                break;
        }

        return fn;
    }

    #region Obsolete - loading from files
    //private static void ProcessXlfFiles(string path)
    //{
    //    var files = FS.GetFiles(path, "*.xlf", SearchOption.TopDirectoryOnly);
    //    files.RemoveAll(d => d.EndsWith(".min.xlf"));
    //    foreach (var file3 in files)
    //    {
    //        var lang = XmlLocalisationInterchangeFileFormatXlf.GetLangFromFilename(file3);
    //        ProcessXlfFile(path, lang, file3);
    //    }
    //}
    //private static void ProcessXlfFile(string basePath, Langs lang, string file)
    //{
    //}

    //public static Dictionary<string, string> LoadXlfDocument(string file)
    //{
    //    var doc = new XlfDocument(file);
    //    return GetTransUnits(doc);
    //} 
    #endregion

    #endregion
    #endregion

    public static string SaveResouresToRL(string VpsHelperSunamo_SunamoProject, LocalizationLanguages ll)
    {
        return XlfResourcesH.SaveResouresToRL<string, string>(null, VpsHelperSunamo_SunamoProject, ll);
    }



}
