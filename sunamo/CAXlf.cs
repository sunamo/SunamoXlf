    /// <summary>
    /// FSXlf - postfixy jsou píčovina. volám v tom metody stejné třídy. Můžu nahradit FS. v SunExc ale musel bych to zkopírovat zpět. to nese riziko že jsem přidal novou metodu kterou bych překopírováním ztratil. Krom toho to nedrží konvenci. V názvu souboru to nechám ať vidím na první dobrou co je co. 
    /// </summary>
    public class CAXlf
    {
        #region Only in *Xlf.cs
        public static List<T> ToList<T>(params T[] t)
        {
            return new List<T>(t);
        }
        #endregion
   }
