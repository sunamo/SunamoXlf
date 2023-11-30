public partial class DefaultPaths
{
    public const string eVs2 = @"E:\vs2\";
    public const string eVs = @"E:\vs\";

    #region For easy copy
    #region Non code => no platform dependent
    public const string BackupSunamosAppData = @"E:\Sync\Develop of Future\Backups\";
    public const string pathPa = @"D:\pa\";
    public const string pathPaSync = @"D:\paSync\";

    public const string capturedUris = @"C:\Users\Administrator\AppData\Roaming\sunamo\SunamoCzAdmin\Data\SubsSignalR\CapturedUris.txt";
    public const string capturedUris_backup = @"C:\Users\Administrator\AppData\Roaming\sunamo\SunamoCzAdmin\Data\SubsSignalR\CapturedUris_backup.txt";

    public const string rootVideos0Kb = @"D:\Documents\Videos0kb\";
    public static string Documents = @"D:\Documents\";
    public static string eDocuments = @"E:\Documents\";
    public static string Docs = @"D:\Docs\";
    public static string Downloads = @"D:\Downloads\";
    public static string Music2 = @"D:\Music2\";
    public static string Backup = @"D:\Documents\Backup\";
    public static string Streamline = @"D:\Pictures\Streamline_All_Icons_PNG\PNG Icons\";
    #endregion

    public static string actualPlatform => BasePathsHelper.actualPlatform;

    /// <summary>
    /// For all is here sczRootPath
    /// edn with bs
    /// </summary>
    public static string sczPath = Path.Combine(actualPlatform, @"Projects\sunamo.cz\sunamo.cz\");
    public static string sczOldPath = Path.Combine(actualPlatform, @"Projects\sunamo.cz\sunamo.cz-old\");
    public static string sczNsnPath = Path.Combine(actualPlatform, @"Projects\sunamo.cz\sunamo.cz-nsn\");
    /// <summary>
    /// Ended with backslash
    /// </summary>
    public static string sczRootPath = Path.Combine(actualPlatform, @"Projects\sunamo.cz\");
    public const string ProjectsFolderNameSlash = "Projects\\";

    #region vs

    public const string cRepos = @"C:\repos";

    public const string bpMb = @"E:\vs\";
    public const string bpQ = @"C:\repos\_\";
    public const string bpVps = @"C:\_\";

    public const string bpBb = @"D:\Documents\BitBucket\";

    public static string bp = null;

    static DefaultPaths()
    {
        bp = actualPlatform;

        sunamo = bp + @"Projects\sunamo\";
        sunamoProject = bp + @"Projects\sunamo\sunamo\";
        vsProjects = bp + @"Projects\";
        vs = bp + @"Projects\";
        KeysXlf = bp + @"Projects\sunamo\sunamo\Enums\KeysXlf.cs";
        DllSunamo = bp + @"Projects\sunamo\dll\";
        VisualStudio2017 = bp;
        VisualStudio2017WoSlash = bp.Substring(0, bp.Length - 1);

        AllPathsToProjects = new[]
           { Test_MoveClassElementIntoSharedFileUC, vs, vsDocuments, vs17 + ProjectsFolderNameSlash, vs17Documents + ProjectsFolderNameSlash, NormalizePathToFolder}.ToList();
    }

    ///// <summary>
    ///// Solution, not project
    ///// </summary>
    //public static string sunamo = null;
    ///// <summary>
    ///// Cant be used also VpsHelperSunamo.SunamoProject()
    ///// </summary>
    //public static string sunamoProject = null;
    ///// <summary>
    ///// E:\Documents\vs\Projects\
    ///// </summary>
    //public static string vsProjects = null;
    ///// <summary>
    ///// E:\Documents\vs\Projects\
    ///// </summary>
    //public static string vs = null;
    //public static string KeysXlf = null;
    //public static string DllSunamo = null;
    //public static string VisualStudio2017 = null;
    //public static string VisualStudio2017WoSlash = null;


    #endregion

    [Obsolete("Not adapting to platform, do not use")]
    public static string vsDocuments = Path.Combine(DefaultPaths.eDocuments, @"vs\");
    [Obsolete("Not adapting to platform, do not use")]
    /// <summary>
    /// Use vs for non shortcuted folder
    /// D:\vs17\
    /// </summary>
    public static string vs17 = @"E:\vs17\";
    [Obsolete("Not adapting to platform, do not use")]
    public static string vs17Documents = Path.Combine(DefaultPaths.eDocuments, @"vs17\");
    public static string NormalizePathToFolder = Path.Combine(actualPlatform, @"Projects\");
    public static string Test_MoveClassElementIntoSharedFileUC = "D:\\_Test\\AllProjectsSearch\\AllProjectsSearch\\MoveClassElementIntoSharedFileUC\\";

    //public static List<string> AllPathsToProjects = null;

    public const string SyncArchived = @"E:\SyncArchived\";
    public const string SyncArchivedText = @"E:\SyncArchived\Text\";
    public const string SyncArchivedDrive = @"E:\SyncArchived\Drive\";

    public static List<string> All = new List<string> { Documents, Docs, Downloads, Music2 };
    public static string XnConvert = @"D:\Pictures\XnConvert\";
    public const string PhotosScz = @"D:\Pictures\photos.sunamo.cz\";
    #endregion

    //public const string BackupSunamosAppData = @"E:\Sync\Develop of Future\Backups\";
    //public const string pathPa = @"D:\pa\";
    //public const string pathPaSync = @"D:\paSync\";

    //public const string capturedUris = @"C:\Users\Administrator\AppData\Roaming\sunamo\SunamoCzAdmin\Data\SubsSignalR\CapturedUris.txt";
    //public const string capturedUris_backup = @"C:\Users\Administrator\AppData\Roaming\sunamo\SunamoCzAdmin\Data\SubsSignalR\CapturedUris_backup.txt";

    //public const string rootVideos0Kb = @"D:\Documents\Videos0kb\";
    //public static string Documents = @"D:\Documents\";
    //public static string eDocuments = @"E:\Documents\";
    //public static string Docs = @"D:\Docs\";
    //public static string Downloads = @"D:\Downloads\";
    //public static string Music2 = @"D:\Music2\";
    //public static string Backup = @"D:\Documents\Backup\";

    public static void InitAllPathsToProjects()
    {
        if (AllPathsToProjects == null)
        {
            AllPathsToProjects = new[] { Test_MoveClassElementIntoSharedFileUC, vs, vsDocuments, vs17 + ProjectsFolderNameSlash, vs17Documents + ProjectsFolderNameSlash, NormalizePathToFolder }.ToList();
        }
    }





    #region 
    //public const string bpMb = @"E:\Documents\vs\";
    //public const string bpQ = @"C:\repos\_\";
    //public const string bpVps = @"C:\_\";

    //public const string bpBb = @"D:\Documents\BitBucket\";

    //public static string bp = null;

    //static DefaultPaths()
    //{
    //    bp = bpMb;

    //    if (VpsHelperSunamo.IsQ)
    //    {
    //        bp = bpQ;
    //    }

    //    sunamo = bp + @"Projects\sunamo\";
    //    sunamoProject = bp + @"Projects\sunamo\sunamo\";
    //    vsProjects = bp + @"Projects\";
    //    vs = bp + @"Projects\";
    //    KeysXlf = bp + @"Projects\sunamo\sunamo\Enums\KeysXlf.cs";
    //    DllSunamo = bp + @"Projects\sunamo\dll\";
    //    VisualStudio2017 = bp;
    //    VisualStudio2017WoSlash = bp.Substring(0, bp.Length - 1);


    //}

    public static List<string> AllPathsToProjects = null;
    //public static string Test_MoveClassElementIntoSharedFileUC = "D:\\_Test\\AllProjectsSearch\\AllProjectsSearch\\MoveClassElementIntoSharedFileUC\\";

    /// <summary>
    /// Solution, not project
    /// </summary>
    public static string sunamo = null;
    /// <summary>
    /// Cant be used also VpsHelperSunamo.SunamoProject()
    /// </summary>
    public static string sunamoProject = null;
    /// <summary>
    /// E:\Documents\vs\Projects\
    /// </summary>
    public static string vsProjects = null;
    /// <summary>
    /// E:\Documents\vs\Projects\
    /// </summary>
    public static string vs = null;
    public static string KeysXlf = null;
    public static string DllSunamo = null;
    public static string VisualStudio2017 = null;
    public static string VisualStudio2017WoSlash = null;
    #endregion
}
