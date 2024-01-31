namespace SunamoXlf;

//public class BasePathsHelperXlf
//{
//    public static string vs = null;
//}


public class BasePathsHelper
{
    static Dictionary<string, bool> exists = new Dictionary<string, bool>();
    public static string actualPlatform = null;

    public static string actualPlatformParent
    {
        get
        {
            if (actualPlatform == @"C:\repos\_\")
            {
                return @"C:\repos\";
            }
            return actualPlatform;
        }
    }

    public static Platforms platform = Platforms.Mb;
    /// <summary>
    /// E:\Documents\vs\Projects\
    /// </summary>
    public static string vs = null;

    static string bpMb => DefaultPaths.bpMb;
    static string bpQ => DefaultPaths.bpQ;
    static string bpVps => DefaultPaths.bpVps;

    static string bpBb => DefaultPaths.bpBb;

    static BasePathsHelper()
    {
        Init();
    }

    public static Func<string, bool> IsJunctionPoint = null;
    public static Func<string, string, int, bool> isCountOfFilesMoreThan = null;

    public static void Init()
    {
        if (vs == null)
        {
            Add(bpMb);
            Add(bpQ);
            Add(bpVps);

            /*
zde jsem měl nějkaou strašně divnou chybu .NETu
            musel jsem dát breakpoint až do static BasePathsHelper(), kdybych ho dal jen to této metody tam se mi to tu nezastaví i když to tu prochází
            přes F10,11 jsem došel až na řádek ThrowEx.Custom
            do ní už mě F11 nepustilo, místo toho mi debugger zase skočil o řádek výše na Where(d => d.Value)
            a pak už jen StackOverflowException

            naštěstí zde je oprava snadná, stačí smazat složku E:\Documents\vs\ jež je pouze link
            */
            var where = exists.Where(d => d.Value).Select(d => d.Key);

            var exQ = exists[bpQ];
            var exMb = exists[bpMb];
            var exVps = exists[bpVps];

            if (exQ && exMb)
            {
                var cf = !isCountOfFilesMoreThan(bpMb, "*", 200);
                //var np = FS.CreateDirectory(bpMb, DirectoryCreateCollisionOption.AddSerie, SerieStyle.Underscore, false);

                if (cf)
                {
                    exists[bpMb] = false;
                    where = exists.Where(d => d.Value).Select(d => d.Key);
                }
            }

            if (exVps && exMb)
            {
                exists[bpVps] = false;
                where = exists.Where(d => d.Value).Select(d => d.Key);
            }

            if (where.Count() > 1)
            {
                // TODO: Udělat tu aby se mi při mb a q a v E:\ to byl jen junction nebo neúplná složka to smazalo/přejmenovalo
                //if(FS.ExistsDirectory(bpMb) && FS.ExistsDirectory(bpQ))
                //{
                //    if (JunctionPoint.)
                //    {

                //    }
                //})));
                //}

                ThrowEx.Custom("Can't identify platform on which app run, more folders found: " + string.Join(",", where.ToArray()));
            }
            else
            {
                actualPlatform = where.First();
                if (actualPlatform != bpMb && actualPlatform != bpQ)
                {
                    if (actualPlatform == bpVps)
                    {
                        platform = Platforms.Vps;
                    }
                    else
                    {
                        ThrowEx.NotImplementedCase(platform);
                    }
                    vs = actualPlatform;
                }
                else
                {
                    if (actualPlatform == bpQ)
                    {
                        platform = Platforms.Q;
                    }
                    vs = actualPlatform + "Projects\\";
                }
            }
        }
    }

    public static bool IsIgnored(string p)
    {
        if (p.StartsWith(bpBb))
        {
            return true;
        }
        return false;
    }

    public static string ConvertToActualPlatform(string s)
    {
        if (s.StartsWith(actualPlatform))
        {
            return s;
        }

        if (s.StartsWith(bpMb))
        {
            return s.Replace(bpMb, actualPlatform);
        }
        else if (s.StartsWith(bpQ))
        {
            return s.Replace(bpMb, actualPlatform);
        }
        else if (s.StartsWith(bpVps))
        {
            return s.Replace(bpVps, actualPlatform);
        }
        else
        {
            ThrowEx.NotImplementedCase(s);
        }
        return null;
    }

    private static void Add(string bpMb)
    {
        exists.Add(bpMb, Directory.Exists(bpMb));
    }
}
