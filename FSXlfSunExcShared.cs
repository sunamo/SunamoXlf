namespace SunamoXlf;

/// <summary>
/// FSXlf - postfixy jsou píčovina. volám v tom metody stejné třídy. Můžu nahradit FS. v SunExc ale musel bych to zkopírovat zpět. to nese riziko že jsem přidal novou metodu kterou bych překopírováním ztratil. Krom toho to nedrží konvenci. V názvu souboru to nechám ať vidím na první dobrou co je co.
/// </summary>
public partial class FSXlf
{
    #region For easy shared
    public static string GetFullPath(string vr)
    {
        var result = Path.GetFullPath(vr);
        return result;
    }

    public static void FileToDirectory(ref string dir)
    {
        if (!dir.EndsWith(AllStringsSE.bs))
        {
            dir = Path.GetDirectoryName(dir);
        }
    }

    #endregion
}
