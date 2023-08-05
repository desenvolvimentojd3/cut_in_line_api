
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CutInLine.Models.Class;

namespace ApesWebPcp.Services
{
    public class Utils
    {
        public static string FirstLetterLowerCase(string text)
        {
            if (text == null || text.Trim() == "") return "";

            var texts = text.Split(" ");
            var result = "";

            var count = 1;

            foreach (var name in texts)
            {
                if (name.Trim() != "")
                {
                    if (count == texts.Length)
                        result += $"{char.ToUpper(name[0])}{name[1..].ToLower()}";
                    else
                        result += $"{char.ToUpper(name[0])}{name[1..].ToLower()} ";
                }

                count++;

            }

            return result;
        }

        public static string[] MonthsName()
        {
            string[] months = {
                "Janeiro",
                "Fevereiro",
                "Março",
                "Abril",
                "Maio",
                "Junho",
                "Julho",
                "Agosto",
                "Setembro",
                "Outubro",
                "Novembro",
                "Dezembro" };

            return months;
        }

        public static string FormatPhone(string text)
        {
            if (text == null || text.Trim() == "") return "";

            string result = "";

            if (text.Trim().Replace(" ", "").Length == 10)
            {
                result = $"{Convert.ToUInt64(text.Trim()):(##) ####-####}";
            }

            return result;
        }

        public static (string, string) ExtractPhoneAndDDD(string text)
        {
            if (text != null && text != "" && text.Trim().Replace(" ", "").Length == 10)
            {
                var number = text.Substring(2);
                var ddd = text.Substring(0, 2);

                return (ddd, number);
            }

            return ("00", "00000000");
        }

        public static string FormatCellPhone(string text)
        {
            if (text == null || text.Trim() == "") return "";

            string result = "";

            if (text.Trim().Length == 11)
            {
                try
                {
                    result = $"{Convert.ToUInt64(text.Trim()):(##) #####-####}";
                }
                catch { }
            }

            return result;
        }

        public static string FormatCpfCnpj(string text)
        {
            if (text == null || text.Trim() == "") return "";

            string result = "";

            if (text.Trim().Length == 14)
            {
                result = Convert.ToUInt64(text.Trim()).ToString(@"00\.000\.000\/0000\-00");
            }
            if (text.Trim().Length == 11)
            {
                result = Convert.ToUInt64(text.Trim()).ToString(@"000\.000\.000\-00");
            }

            return result;
        }


        public static string[] ImageExtensions()
        {
            string[] extensions = {
                ".jpeg",
                ".jpg",
                ".bmp",
                ".jiff",
                ".png",
                ".tiff",
                ".svg" };

            return extensions;
        }

        public static string OnlyNumbers(string text)
        {
            if (text == null) return "";

            return string.Join("", text.ToCharArray().Where(Char.IsDigit));
        }

        public static List<string> ExtractStringIntoDelimiters(string str, string initial, string final)
        {
            var Strings = new List<string>();
            var phrase = "";
            var foundDelimiter = false;

            for (int index = 0; index < str.Length; index++)
            {
                if (str[index] == char.Parse(final))
                {
                    Strings.Add(phrase);

                    foundDelimiter = false;
                    phrase = "";
                }

                if (foundDelimiter) phrase += str[index];

                if (str[index] == char.Parse(initial))
                    foundDelimiter = true;
            }

            return Strings;
        }

        public static string RemoveAccents(string input)
        {
            string normalizedString = input.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string ConvertToBase64(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(bytes);
        }

        public static string ConvertFromBase64(string text)
        {
            byte[] data = Convert.FromBase64String(text);
            return Encoding.UTF8.GetString(data);
        }

        public static string OnlyNumbersAndLetters(string text)
        {
            var regex = new Regex("[^a-zA-Z0-9]");
            return regex.Replace(text, "");
        }

        public static string Saudacao()
        {
            TimeSpan horaAtual = DateTime.Now.TimeOfDay;
            TimeSpan hora3 = new TimeSpan(3, 0, 0);
            TimeSpan hora12 = new TimeSpan(12, 0, 0);
            TimeSpan hora18 = new TimeSpan(18, 0, 0);

            if (horaAtual >= hora3 && horaAtual < hora12)
            {
                return "Bom dia";
            }
            else if (horaAtual >= hora12 && horaAtual < hora18)
            {
                return "Boa tarde";
            }
            else
            {
                return "Boa noite";
            }
        }

        public static string ReplaceDepositAddressesSql()
        {
            var sql = "";

            sql += "regexp_replace(regexp_replace(regexp_replace(regexp_replace(Smascara, ";
            sql += "'{Lado}',slado),                                                      ";
            sql += "'{Corredor}',scorredor),                                              ";
            sql += "'{Nível}', CAST(iandar as Varchar)),                                  ";
            sql += "'{Prédio}', CAST(ipredio as Varchar))                                 ";

            return sql;
        }

        public static string ReturnsWordsInArray(string[] keyWords, string text)
        {
            string padrao = @"\b(" + string.Join("|", keyWords) + @")\b";

            MatchCollection matches = Regex.Matches(text.ToUpper(), padrao, RegexOptions.IgnoreCase);

            foreach (Match match in matches)
            {
                return match.Value;
            }

            return "";
        }

        public static string[] TypesOfAddress()
        {
            string[] types = {"AEROPORTO",
                            "ALAMEDA",
                            "ÁREA",
                            "AVENIDA",
                            "CAMPO",
                            "CHÁCARA",
                            "COLÔNIA",
                            "CONDOMÍNIO",
                            "CONJUNTO",
                            "DISTRITO",
                            "ESPLANADA",
                            "ESTAÇÃO",
                            "ESTRADA",
                            "FAVELA",
                            "FAZENDA",
                            "FEIRA",
                            "JARDIM",
                            "LADEIRA",
                            "LAGO",
                            "LAGOA",
                            "LARGO",
                            "LOTEAMENTO",
                            "MORRO",
                            "NÚCLEO",
                            "PARQUE",
                            "PASSARELA",
                            "PÁTIO",
                            "PRAÇA",
                            "QUADRA",
                            "RECANTO",
                            "RESIDENCIAL",
                            "RODOVIA",
                            "RUA",
                            "SETOR",
                            "SÍTIO",
                            "TRAVESSA",
                            "TRECHO",
                            "TREVO",
                            "VALE",
                            "VEREDA",
                            "VIA",
                            "VIADUTO",
                            "VIELA",
                            "VILA"};
            return types;
        }

        public static bool IsValidEmail(string email)
        {
            string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

            // Verifica se o e-mail corresponde ao padrão
            Match match = Regex.Match(email, pattern);

            return match.Success;
        }

        public static string RemoveEmptyLines(string input)
        {
            string pattern = @"^\s*$"; // Expressão regular para identificar linhas vazias
            string replacement = string.Empty; // Substituir linhas vazias por uma string vazia

            string result = Regex.Replace(input, pattern, replacement, RegexOptions.Multiline);

            return result;
        }

        public static MessageHelper CheckStrongPassword(string password)
        {
            var message = new MessageHelper
            {
                Message = "",
                Success = true
            };

            if (password.Length < 8)
            {
                message.Success = false;
                message.Message = "A senha deve ter 8 caracteres ou mais";
            }

            if (!password.Any(char.IsUpper))
            {
                message.Success = false;
                message.Message = "Deve possuir pelo menos 1 letra maiúscula";
            }

            if (password.Contains(" "))
            {
                message.Success = false;
                message.Message = "Não é permitido espaços em branco na senha";
            }

            string specialChar = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
            char[] chars = specialChar.ToCharArray();

            bool foundChar = false;

            foreach (char ch in chars)
            {
                if (password.Contains(ch))
                    foundChar = true;
            }

            if (!foundChar)
            {
                message.Success = false;
                message.Message = "A senha deve possuir pelo menos 1 caracter especial";
            }

            return message;
        }
    }
}
