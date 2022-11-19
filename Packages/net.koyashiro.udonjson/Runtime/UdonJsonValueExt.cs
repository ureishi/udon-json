namespace Koyashiro.UdonJson
{
    using Koyashiro.UdonList;
    using Koyashiro.UdonDictionary;

    public static class UdonJsonValueExt
    {
        private const string ERR_INVALID_KIND = "Invalid kind";

        public static UdonJsonValueKind GetKind(this UdonJsonValue value)
        {
            return (UdonJsonValueKind)(value.AsRawArray()[0]);
        }

        public static string AsString(this UdonJsonValue v)
        {
            if (v.GetKind() != UdonJsonValueKind.String)
            {
                UdonException.ThrowArgumentException(ERR_INVALID_KIND);
            }

            return (string)v.GetValueUnchecked();
        }

        public static double AsNumber(this UdonJsonValue v)
        {
            if (v.GetKind() != UdonJsonValueKind.Number)
            {
                UdonException.ThrowArgumentException(ERR_INVALID_KIND);
            }

            return (double)v.GetValueUnchecked();
        }

        public static bool AsBool(this UdonJsonValue v)
        {
            if (v.GetKind() != UdonJsonValueKind.True && v.GetKind() != UdonJsonValueKind.False)
            {
                UdonException.ThrowArgumentException(ERR_INVALID_KIND);
            }

            return (bool)v.GetValueUnchecked();
        }

        public static object AsNull(this UdonJsonValue v)
        {
            if (v.GetKind() != UdonJsonValueKind.Null)
            {
                UdonException.ThrowArgumentException(ERR_INVALID_KIND);
            }

            return null;
        }

        public static int GetCount(this UdonJsonValue v)
        {
            if (v.GetKind() != UdonJsonValueKind.Object && v.GetKind() != UdonJsonValueKind.Array)
            {
                UdonException.ThrowArgumentException(ERR_INVALID_KIND);
            }

            return v.AsList().GetCount();
        }

        public static string GetKey(this UdonJsonValue v, int index)
        {
            if (v.GetKind() != UdonJsonValueKind.Object)
            {
                UdonException.ThrowArgumentException(ERR_INVALID_KIND);
            }

            return (string)v.AsDictionary().GetKey(index);
        }

        public static UdonJsonValue GetValue(this UdonJsonValue v, string key)
        {
            if (v.GetKind() != UdonJsonValueKind.Object)
            {
                UdonException.ThrowArgumentException(ERR_INVALID_KIND);
            }

            return (UdonJsonValue)v.AsDictionary().GetValue(key);
        }

        public static UdonJsonValue GetValue(this UdonJsonValue v, int key)
        {
            if (v.GetKind() != UdonJsonValueKind.Array)
            {
                UdonException.ThrowArgumentException(ERR_INVALID_KIND);
            }

            return (UdonJsonValue)(v.AsList().GetItem(key));
        }

        public static void SetValue(this UdonJsonValue v, string key, UdonJsonValue value)
        {
            if (v.GetKind() != UdonJsonValueKind.Object)
            {
                UdonException.ThrowArgumentException(ERR_INVALID_KIND);
            }

            v.AsDictionary().SetValue(key, value);
        }

        public static void SetValue(this UdonJsonValue v, int key, UdonJsonValue value)
        {
            if (v.GetKind() != UdonJsonValueKind.Array)
            {
                UdonException.ThrowArgumentException(ERR_INVALID_KIND);
            }

            v.AsList().SetItem(key, value);
        }

        public static void AddValue(this UdonJsonValue v, UdonJsonValue value)
        {
            if (v.GetKind() != UdonJsonValueKind.Array)
            {
                UdonException.ThrowArgumentException(ERR_INVALID_KIND);
            }

            v.AsList().Add(value);
        }

        private static UdonDictionary AsDictionary(this UdonJsonValue v)
        {
            return (UdonDictionary)v.GetValueUnchecked();
        }

        private static UdonList AsList(this UdonJsonValue v)
        {

            return (UdonList)v.GetValueUnchecked();
        }

        private static object GetValueUnchecked(this UdonJsonValue v)
        {
            return (UdonJsonValueKind)(v.AsRawArray()[1]);
        }

        private static object[] AsRawArray(this UdonJsonValue v)
        {
            return (object[])(object)v;
        }
    }
}
