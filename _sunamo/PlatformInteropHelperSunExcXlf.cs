namespace SunamoXlf._sunamo;

internal partial class PlatformInteropHelperXlf
{
    #region For easy copy
    internal static bool IsSellingApp()
    {
        return RHSunExcXlf.ExistsClass("SellingHelper");
    }
    #endregion
}
