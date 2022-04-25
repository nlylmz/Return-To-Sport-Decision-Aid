using System;
using System.ComponentModel;
using System.Security;

namespace ReturnToSport.CustomLibrary
{
    public static class CustomExtensions
    {
        public static decimal? SifirOlmasin(this Decimal? sayi)
        {
            if (sayi == 0)
            {
                return 1;
            }
            return sayi;
        }

        public static decimal SifirOlmasin(this Decimal sayi)
        {
            if (sayi == 0)
            {
                return 1;
            }
            return sayi;
        }

        public static decimal? SifirOlmasin(this Decimal? sayi, decimal? buOlsun)
        {
            if (sayi == 0)
            {
                return buOlsun;
            }
            return sayi;
        }

        public static decimal NullVeyaSifirOlmasin(this Decimal? sayi, decimal buOlsun)
        {
            if (sayi == null)
            {
                return buOlsun;
            }

            if (sayi == 0)
            {
                return buOlsun;
            }
            return (decimal)sayi;
        }

        public static long NullVeyaSifirOlmasin(this long? sayi, long buOlsun)
        {
            if (sayi == null)
            {
                return buOlsun;
            }

            if (sayi == 0)
            {
                return buOlsun;
            }
            return (long)sayi;
        }

        public static int NullVeyaSifirOlmasin(this int? sayi, int buOlsun)
        {
            if (sayi == null)
            {
                return buOlsun;
            }

            if (sayi == 0)
            {
                return buOlsun;
            }
            return (int)sayi;
        }

        public static long SifirOlmasin(this long sayi, long buOlsun)
        {
            if (sayi == 0)
            {
                return buOlsun;
            }
            return sayi;
        }

        public static string BosIseSifirYaz(this string yazi)
        {
            if (yazi == "" || yazi == null)
            {
                return "0";
            }

            return yazi;
        }

        public static bool NullIseFalse(this bool? boolean)
        {
            if (boolean == null || boolean == false)
            {
                return false;
            }

            return true;
        }

        public static DateTime NullIseBinDokuyuzYaz(this DateTime? tarih)
        {
            if (tarih == null)
            {
                return new DateTime(1900, 1, 1);
            }

            return (DateTime)tarih;
        }

        public static int BasamakToplaminiAl(this int gelenSayi)
        {
            int sum = 0;
            while (gelenSayi != 0)
            {
                sum += gelenSayi % 10;
                gelenSayi /= 10;
            }

            return sum;
        }

        public static long BasamakToplaminiAl(this long gelenSayi)
        {
            long sum = 0;
            while (gelenSayi != 0)
            {
                sum += gelenSayi % 10;
                gelenSayi /= 10;
            }

            return sum;
        }

        public static int CharNullOlmasin(this char? multilateralHarfi)
        {
            if (multilateralHarfi == null || multilateralHarfi < 64 || multilateralHarfi > 73)
            {
                return 64;
            }
            return (int)multilateralHarfi;
        }

        public static SecureString SecureStringeDonustur(this string gelenString)
        {
            SecureString secureString = new SecureString();
            foreach (char c in gelenString)
            {
                secureString.AppendChar(c);
            }
            secureString.MakeReadOnly();

            return secureString;
        }

        public static string GetDescription(this Enum enumerationValue)
        {
            var type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException($"{nameof(enumerationValue)} must be of Enum type", nameof(enumerationValue));
            }
            var memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo.Length > 0)
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return enumerationValue.ToString();
        }

        public static string ToKullaniciAdi(this String str)  //first paramter specifies which type the method operates on preceded by this modifier
        {
            //string[] names = str.Split('/');
            string name = str.Replace("TPAO\\", "");
            return name;
        }
    }
}