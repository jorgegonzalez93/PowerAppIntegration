using System.Text;

namespace Migration.Domain.Domain.Helpers
{
    public static class ReplaceCharacterHelper
    {
        public class CharacterConversion
        {
            public string InvalidCharacter { get; set; } = default!;
            public string ValidCharacter { get; set; } = default!;
        }

        private static readonly List<CharacterConversion> _CHARACTER_REPLACE_LIST = new() {

                new()
                {
                    InvalidCharacter = "?'",
                    ValidCharacter ="ñ"
                },
                new()
                {
                    InvalidCharacter = "�'",
                    ValidCharacter ="ñ"
                },
                new()
                {
                    InvalidCharacter = "Ã³",
                    ValidCharacter ="Ó"
                },
                new()
                {
                    InvalidCharacter = "Ã©",
                    ValidCharacter ="é"
                },
                new()
                {
                    InvalidCharacter = "Ãš",
                    ValidCharacter ="Ú"
                },
                new()
                {
                    InvalidCharacter = "Ã‰",
                    ValidCharacter ="É"
                },
                new()
                {
                    InvalidCharacter = "�?",
                    ValidCharacter ="É"
                } ,
                new()
                {
                    InvalidCharacter = "�s",
                    ValidCharacter ="Ú"
                },
                new()
                {
                    InvalidCharacter = "�\"",
                    ValidCharacter ="Ó"
                },
                new()
                {
                    InvalidCharacter = "�?",
                    ValidCharacter ="Á"
                },
                new()
                {
                    InvalidCharacter = "�",
                    ValidCharacter ="Ó"
                }
        };

        public static List<CharacterConversion> CHARACTER_REPLACE_LIST => _CHARACTER_REPLACE_LIST;

        public static string CleanDataChangeCharacters(string inputData)
        {
            inputData = inputData.Trim();
            //inputData = Encoding.UTF8.GetString(Encoding.GetEncoding("iso-8859-1").GetBytes(inputData));

            //if (CHARACTER_REPLACE_LIST.Any(lst => inputData.Contains(lst.InvalidCharacter)))
            //{
            //    foreach (CharacterConversion replaceCharacter in CHARACTER_REPLACE_LIST)
            //    {
            //        inputData = inputData.Replace(replaceCharacter.InvalidCharacter, replaceCharacter.ValidCharacter);
            //    }
            //}

            return inputData.ToUpperInvariant();
        }
    }
}
