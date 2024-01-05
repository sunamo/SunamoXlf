using SunamoValues;

namespace SunamoXlf._sunamo;

/// <summary>
/// FSXlf - postfixy jsou píčovina. volám v tom metody stejné třídy. Můžu nahradit FS. v SunExc ale musel bych to zkopírovat zpět. to nese riziko že jsem přidal novou metodu kterou bych překopírováním ztratil. Krom toho to nedrží konvenci. V názvu souboru to nechám ať vidím na první dobrou co je co.
/// </summary>
internal partial class FSXlf
{
    #region For easy shared
    internal static string GetFullPath(string vr)
    {
        var result = Path.GetFullPath(vr);
        return result;
    }

    internal static void FileToDirectory(ref string dir)
    {
        if (!dir.EndsWith(AllStringsSE.bs))
        {
            dir = Path.GetDirectoryName(dir);
        }
    }

    #endregion
}
