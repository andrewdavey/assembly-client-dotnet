using System;

namespace AssemblyClient
{

    public static class StringExtensions
    {
        public static byte[] GetBytes(this string me)
        {
            byte[] bytes = new byte[me.Length * sizeof(char)];
            System.Buffer.BlockCopy(me.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string ToProperty(this string me)
        {
            var result =  me.Replace("_", " ").TitleCase().Replace(" ", "");
            return result;
        }
        public static String TitleCase(this string me)
        {
            if (me == null) return me;

            String[] words = me.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length == 0) continue;

                Char firstChar = Char.ToUpper(words[i][0]); 
                String rest = "";
                if (words[i].Length > 1)
                {
                    rest = words[i].Substring(1).ToLower();
                }
                words[i] = firstChar + rest;
            }
            return String.Join(" ", words);
        }
    }
}