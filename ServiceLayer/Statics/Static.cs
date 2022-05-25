
namespace ServiceLayer.Statics
{
    public static class Static
    {
        #region Strings

        public static readonly string DLL = ".dll";
        public static readonly string WRONG_LENGHT = " Wrong Lenght !";
        public static readonly string WRONG_WHILEUPDATING = " Eroor wile updating data  !";
        public static readonly string WRONG_REQUEST_FORMAT = " Wrong Request Format !";
        public static readonly string WRONG_ROW_NUMBER = " Wrong number of rows !";
        public static readonly string WRONG_DATAROWS = " Exchange DataRows don't match the number below or are invalid !";
        public static readonly string WRONG_REDFILE = "Could not read file !";
        public static readonly string REG_REQUEST_FORMAT = "[A-Z]{3};[0-9]+;[A-Z]{3}$";
        public static readonly string REG_REQUEST_COUNT = @"^[1-9]\d*$";
        public static readonly string REG_REQUEST_DATALINE = @"[A-Z]{3};[A-Z]{3};[0-9]+\.[0-9]{4}$";

        #endregion

        #region Numeric

        public static readonly ushort MINROWSCOUNT = 3;
        public static readonly ushort CURRENCYLENGHT = 3;



        #endregion
    }
}
