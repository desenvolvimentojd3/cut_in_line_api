
using ApesWebPcp.Services;
using CutInLine.Models.Class;

public class Search
{
    public static string GetSearchString(SearchHelper searchHelper)
    {
        var Where = "";
        var SearchStringdividida = "";

        searchHelper.Where = searchHelper.Where.OrderBy(x => !x.FastSearch ? 0 : 1).ToList();

        var firstValue = true;
        var hasFastSearch = false;

        foreach (var search in searchHelper.Where)
        {
            search.Value = Utils.RemoveAccents(search.Value.ToUpper());
            search.Field = search.Field.ToLower();
            search.Condition = search.Condition != null ? search.Condition.ToLower() : "";

            //Sql Injection
            search.Value = search.Value.Replace("DELETE", "");
            search.Value = search.Value.Replace("UPDATE", "");

            if (search.Condition == "contendo")
            {
                if (search.Field.Substring(0, 1) == "s")
                {
                    //Se houver separar, escrever o código com 'or' para buscar mais de uma informação na mesma coluna
                    var iseparador = 0;

                    //Procurar Espaços em braco e substitui por '%'
                    var SearchString = search.Value.Replace(" ", "%") + ";";

                    var SearchStringcompleta = SearchString;

                    for (int indice = 0; indice <= SearchStringcompleta.Length - 1; indice++)
                    {
                        if (SearchStringcompleta[indice] == ';')
                        {
                            if (iseparador == 0)
                            {
                                SearchString = " and (UPPER(remove_accents(" + search.Field + ")) like '%" + SearchStringdividida + "%'";
                            }
                            else if (iseparador > 0)
                            {
                                SearchString =
                                    SearchString + " or UPPER(remove_accents(" + search.Field + ")) like '%" + SearchStringdividida + "%'";
                            }

                            iseparador = 1;
                            SearchStringdividida = "";
                        }
                        else if (SearchStringcompleta[indice] != ';')
                        {
                            SearchStringdividida += SearchStringcompleta[indice];
                        }
                    }

                    Where = Where + SearchString + ")";
                }
                else if (search.Field.Substring(0, 1) == "i")
                {
                    //Procurar Espaços em braco e substitui por '%'
                    var SearchString = search.Value.Replace(" ", "%") + ";";

                    //Se houver separar, escrever o código com 'or' para buscar mais de uma informação na mesma coluna
                    var iseparador = 0;

                    var SearchStringcompleta = SearchString;

                    for (int indice = 0; indice <= SearchStringcompleta.Length - 1; indice++)
                    {
                        if (SearchStringcompleta[indice] == ';')
                        {
                            if (iseparador == 0)
                            {
                                SearchString =
                                    " and (CAST(" +
                                    search.Field +
                                    " AS VARCHAR)" +
                                    " like '%" +
                                    SearchStringdividida +
                                    "%'";
                            }
                            else if (iseparador > 0)
                            {
                                SearchString =
                                    SearchString +
                                    " or CAST(" +
                                    search.Field +
                                    " AS VARCHAR)" +
                                    " like '%" +
                                    SearchStringdividida +
                                    "%'";
                            }

                            iseparador = 1;
                            SearchStringdividida = "";
                        }
                        else if (SearchStringcompleta[indice] != ';')
                        {
                            SearchStringdividida += SearchStringcompleta[indice];
                        }
                    }

                    Where = Where + SearchString + ")";
                }
                else if (search.Field.Substring(0, 1) == "n")
                {
                    //Procura virgula e substituiu por ponto para pesquisa
                    search.Value = search.Value.Replace(",", ".");

                    //Procurar Espaços em braco e substitui por '%'
                    var SearchString = search.Value.Replace(" ", "%");

                    SearchString += ";";

                    //Se houver separar, escrever o código com 'or' para buscar mais de uma informação na mesma coluna
                    var iseparador = 0;

                    var SearchStringcompleta = SearchString;

                    for (int indice = 0; indice <= SearchStringcompleta.Length - 1; indice++)
                    {
                        if (SearchStringcompleta[indice] == ';')
                        {
                            if (iseparador == 0)
                            {
                                SearchString =
                                    " and (CAST(" +
                                    search.Field +
                                    " AS VARCHAR)" +
                                    " like '%" +
                                    SearchStringdividida +
                                    "%'";
                            }
                            else if (iseparador > 0)
                            {
                                SearchString =
                                    SearchString +
                                    " or CAST(" +
                                    search.Field +
                                    " AS VARCHAR)" +
                                    " like '% " +
                                    SearchStringdividida +
                                    "%'";
                            }

                            iseparador = 1;
                            SearchStringdividida = "";
                        }
                        else if (SearchStringcompleta[indice] != ';')
                        {
                            SearchStringdividida += SearchStringcompleta[indice];
                        }
                    }

                    Where = Where + SearchString + ")";
                }
                else if (search.Field.Substring(0, 9) == "ddatahora")
                {
                    var iseparador = 0;
                    var SearchString = search.Value + ";";

                    var SearchStringcompleta = SearchString.Trim();

                    for (int indice = 0; indice <= SearchStringcompleta.Length - 1; indice++)
                    {
                        if (SearchStringcompleta[indice] == ';')
                        {
                            if (iseparador == 0)
                            {
                                SearchString =
                                    " and (" +
                                    search.Field +
                                    " between '" +
                                    SearchStringdividida +
                                    " 00:00' and '" +
                                    SearchStringdividida +
                                    " 23:59'";
                            }
                            else if (iseparador > 0)
                            {
                                SearchString =
                                    SearchString +
                                    " or " +
                                    search.Field +
                                    " between '" +
                                    SearchStringdividida +
                                    " 00:00' and " +
                                    SearchStringdividida +
                                    " 23:59'";
                            }

                            iseparador = 1;
                            SearchStringdividida = "";
                        }
                        else if (SearchStringcompleta[indice] != ';')
                        {
                            SearchStringdividida += SearchStringcompleta[indice];
                        }
                    }

                    Where = Where + SearchString + ")";
                }
                else if (search.Field.Substring(0, 1) == "d" || search.Field.Substring(0, 1) == "h")
                {
                    var iseparador = 0;
                    var SearchString = search.Value + ";";

                    var SearchStringcompleta = SearchString;

                    for (int indice = 0; indice <= SearchStringcompleta.Length - 1; indice++)
                    {
                        if (SearchStringcompleta[indice] == ';')
                        {
                            if (iseparador == 0)
                            {
                                SearchString = " and " + search.Field + " in ('" + SearchStringdividida + "'";
                            }
                            else if (iseparador > 0)
                            {
                                SearchString = SearchString + "," + "'" + SearchStringdividida + " '";
                            }

                            iseparador = 1;
                            SearchStringdividida = "";
                        }
                        else if (SearchStringcompleta[indice] != ';')
                        {
                            SearchStringdividida += SearchStringcompleta[indice];
                        }
                    }

                    Where = Where + SearchString + ")";
                }
            }
            else if (search.Condition == "não contendo")
            {
                if (search.Field.Substring(0, 1) == "s")
                {
                    //Procurar Espaços em braco e substitui por '%'
                    var SearchString = search.Value.Replace(" ", "%") + ";";

                    //Se houver separar, escrever o código com 'or' para buscar mais de uma informação na mesma coluna
                    var iseparador = 0;
                    var SearchStringcompleta = SearchString;

                    for (int indice = 0; indice <= SearchStringcompleta.Length - 1; indice++)
                    {
                        if (SearchStringcompleta[indice] == ';')
                        {
                            if (iseparador == 0)
                            {
                                SearchString = " and UPPER(remove_accents(" + search.Field + ")) not like '%" + SearchStringdividida + "%'";
                            }
                            else if (iseparador > 0)
                            {
                                SearchString =
                                    SearchString +
                                    " and UPPER(" +
                                    search.Field +
                                    ") not like '%" +
                                    SearchStringdividida +
                                    "%'";
                            }

                            iseparador = 1;
                            SearchStringdividida = "";
                        }
                        else if (SearchStringcompleta[indice] != ';')
                        {
                            SearchStringdividida += SearchStringcompleta[indice];
                        }
                    }

                    Where += SearchString;
                }
                else if (search.Field.Substring(0, 1) == "i")
                {
                    //Procurar Espaços em braco e substitui por '%'
                    var SearchString = search.Value.Replace(" ", "%") + ";";

                    //Se houver separar, escrever o código com 'or' para buscar mais de uma informação na mesma coluna
                    var iseparador = 0;

                    var SearchStringcompleta = SearchString;

                    for (int indice = 0; indice <= SearchStringcompleta.Length - 1; indice++)
                    {
                        if (SearchStringcompleta[indice] == ';')
                        {
                            if (iseparador == 0)
                            {
                                SearchString =
                                    " and CAST(" +
                                    search.Field +
                                    " AS VARCHAR) not like '%" +
                                    SearchStringdividida +
                                    "%' ";
                            }
                            else if (iseparador > 0)
                            {
                                SearchString =
                                    SearchString +
                                    " and CAST(" +
                                    search.Field +
                                    " AS VARCHAR) not like '%" +
                                    SearchStringdividida +
                                    "%' ";
                            }

                            iseparador = 1;
                            SearchStringdividida = "";
                        }
                        else if (SearchStringcompleta[indice] != ';')
                        {
                            SearchStringdividida += SearchStringcompleta[indice];
                        }
                    }

                    Where += SearchString;
                }
                else if (search.Field.Substring(0, 1) == "n")
                {
                    //Procura virgula e substituiu por ponto para pesquisa
                    search.Value = search.Value.Replace(",", ".");

                    //Procurar Espaços em braco e substitui por '%'
                    var SearchString = search.Value.Replace(" ", "%");

                    //Se houver separar, escrever o código com 'or' para buscar mais de uma informação na mesma coluna
                    var iseparador = 0;
                    SearchString += ";";

                    var SearchStringcompleta = SearchString;

                    for (int indice = 0; indice <= SearchStringcompleta.Length - 1; indice++)
                    {
                        if (SearchStringcompleta[indice] == ';')
                        {
                            if (iseparador == 0)
                            {
                                SearchString =
                                    " and CAST(" +
                                    search.Field +
                                    " AS VARCHAR) not like '%" +
                                    SearchStringdividida +
                                    "%' ";
                            }
                            else if (iseparador > 0)
                            {
                                SearchString =
                                    SearchString +
                                    " and CAST(" +
                                    search.Field +
                                    " AS VARCHAR) not like '%" +
                                    SearchStringdividida +
                                    "%' ";
                            }

                            iseparador = 1;
                            SearchStringdividida = "";
                        }
                        else if (SearchStringcompleta[indice] != ';')
                        {
                            SearchStringdividida += SearchStringcompleta[indice];
                        }
                    }

                    Where += SearchString;
                }
                else if (search.Field.Substring(0, 1) == "d" || search.Field.Substring(0, 1) == "h")
                {
                    var iseparador = 0;
                    var SearchString = search.Value + ";";

                    var SearchStringcompleta = SearchString;

                    for (int indice = 0; indice <= SearchStringcompleta.Length - 1; indice++)
                    {
                        if (SearchStringcompleta[indice] == ';')
                        {
                            if (iseparador == 0)
                            {
                                SearchString = " and " + search.Field + " not in ('" + SearchStringdividida + "'";
                            }
                            else if (iseparador > 0)
                            {
                                SearchString = SearchString + ",'" + SearchStringdividida + "'";
                            }

                            iseparador = 1;
                            SearchStringdividida = "";
                        }
                        else if (SearchStringcompleta[indice] != ';')
                        {
                            SearchStringdividida += SearchStringcompleta[indice];
                        }
                    }

                    Where = Where + SearchString + ")";
                }
            }
            else if (search.Condition == "pertence" || search.Condition == "igual")
            {
                if (
                    search.Field.Substring(0, 1) == "s" ||
                    search.Field.Substring(0, 1) == "d" ||
                    search.Field.Substring(0, 1) == "h"
                )
                {
                    var iseparador = 0;
                    var SearchString = search.Value + ";";

                    var SearchStringcompleta = SearchString;

                    for (int indice = 0; indice <= SearchStringcompleta.Length - 1; indice++)
                    {
                        if (SearchStringcompleta[indice] == ';')
                        {
                            if (iseparador == 0)
                            {
                                if (search.Field.Substring(0, 1) == "s")
                                    SearchString = " and UPPER(remove_accents(" + search.Field + ")) in ('" + SearchStringdividida + "'";
                                else
                                    SearchString = " and " + search.Field + " in ('" + SearchStringdividida + "'";
                            }
                            else if (iseparador > 0)
                            {
                                SearchString = SearchString + ", '" + SearchStringdividida + "'";
                            }

                            iseparador = 1;
                            SearchStringdividida = "";
                        }
                        else if (SearchStringcompleta[indice] != ';')
                        {
                            SearchStringdividida += SearchStringcompleta[indice];
                        }
                    }

                    Where = Where + SearchString + ")";
                }
                else if (search.Field.Substring(0, 1) == "i")
                {
                    //Procura separador ';' e substituiu com ',' para permitir busca de vários itens
                    var SearchString = search.Value.Replace(";", ",");

                    Where = Where + " and " + search.Field + " in (" + SearchString + ")";
                }
                else if (search.Field.Substring(0, 1) == "n")
                {
                    //Procura virgula e substituiu por ponto para pesquisa
                    search.Value = search.Value.Replace(",", ".");

                    //Procura separador ';' e substituiu com ',' para permitir busca de vários itens
                    var SearchString = search.Value.Replace(";", ",");

                    Where = Where + " and " + search.Field + " in (" + SearchString + ")";
                }
                else if (search.Field.Substring(0, 1) == "b")
                {
                    //Procura virgula e substituiu por ponto para pesquisa
                    search.Value = search.Value.Replace(",", ".");

                    //Procura separador ';' e substituiu com ',' para permitir busca de vários itens
                    var SearchString = search.Value.Replace(";", ",");

                    Where = Where + " and " + search.Field + " = " + SearchString + " ";
                }
            }
            else if (search.Condition == "não pertence")
            {
                if (
                    search.Field.Substring(0, 1) == "s" ||
                    search.Field.Substring(0, 1) == "d" ||
                    search.Field.Substring(0, 1) == "h"
                )
                {
                    var iseparador = 0;
                    var SearchString = search.Value + ";";

                    var SearchStringcompleta = SearchString;

                    for (int indice = 0; indice <= SearchStringcompleta.Length - 1; indice++)
                    {
                        if (SearchStringcompleta[indice] == ';')
                        {
                            if (iseparador == 0)
                            {
                                if (search.Field.Substring(0, 1) == "s")
                                    SearchString = " and UPPER(remove_accents(" + search.Field + ")) not in ('" + SearchStringdividida + "'";
                                else
                                    SearchString = " and " + search.Field + " not in ('" + SearchStringdividida + "'";
                            }
                            else if (iseparador > 0)
                            {
                                SearchString = SearchString + ", '" + SearchStringdividida + "'";
                            }

                            iseparador = 1;
                            SearchStringdividida = "";
                        }
                        else if (SearchStringcompleta[indice] != ';')
                        {
                            SearchStringdividida += SearchStringcompleta[indice];
                        }
                    }

                    Where = Where + SearchString + ")";
                }
                else if (search.Field.Substring(0, 1) == "i")
                {
                    //Procura separador ';' e substituiu com ',' para permitir busca de vários itens
                    var SearchString = search.Value.Replace(";", ",");

                    Where = Where + " and " + search.Field + " not in (" + SearchString + ")";
                }
                else if (search.Field.Substring(0, 1) == "n")
                {
                    //Procura virgula e substituiu por ponto para pesquisa
                    search.Value = search.Value.Replace(",", ".");

                    //Procura separador ';' e substituiu com ',' para permitir busca de vários itens
                    var SearchString = search.Value.Replace(";", ",");

                    Where = Where + " and " + search.Field + " not in (" + SearchString + ")";
                }
            }
            else if (search.Condition == "maior")
            {
                var SearchString = search.Value;

                if (
                    search.Field.Substring(0, 1) == "s" ||
                    search.Field.Substring(0, 1) == "d" ||
                    search.Field.Substring(0, 1) == "h"
                )
                {
                    if (search.Field.Substring(0, 1) == "s")
                        Where = Where + " and UPPER(" + search.Field + ") > '" + SearchString + "'";
                    else
                        Where = Where + " and " + search.Field + " > '" + SearchString + "'";
                }
                else if (search.Field.Substring(0, 1) == "i")
                {
                    Where = Where + " and " + search.Field + " > " + SearchString;
                }
                else if (search.Field.Substring(0, 1) == "n")
                {
                    //Procura virgula e substituiu por ponto para pesquisa
                    SearchString = search.Value.Replace(",", ".");

                    Where = Where + " and " + search.Field + " > " + SearchString;
                }
            }
            else if (search.Condition == "menor")
            {
                var SearchString = search.Value;

                if (
                    search.Field.Substring(0, 1) == "s" ||
                    search.Field.Substring(0, 1) == "d" ||
                    search.Field.Substring(0, 1) == "h"
                )
                {
                    if (search.Field.Substring(0, 1) == "s")
                        Where = Where + " and UPPER(" + search.Field + ") <  '" + SearchString + "'";
                    else
                        Where = Where + " and " + search.Field + " <  '" + SearchString + "'";
                }
                else if (search.Field.Substring(0, 1) == "i")
                {
                    Where = Where + " and " + search.Field + " < '" + SearchString + "'";
                }
                else if (search.Field.Substring(0, 1) == "n")
                {
                    //Procura virgula e substituiu por ponto para pesquisa
                    SearchString = search.Value.Replace(",", ".");

                    Where = Where + " and " + search.Field + " < " + SearchString;
                }
            }
            else if (search.Condition == "entre")
            {
                if (search.Field.Length >= 9 && search.Field.Substring(0, 9) == "ddatahora")
                {
                    var SearchString = search.Value + ";";
                    var iseparador = 0;

                    var SearchStringcompleta = SearchString.Trim();

                    for (int indice = 0; indice <= SearchStringcompleta.Length - 1; indice++)
                    {
                        if (SearchStringcompleta[indice] == ';')
                        {
                            if (iseparador == 0)
                            {
                                SearchString = " and " + search.Field + " between '" + SearchStringdividida + " 00:00'";
                            }
                            else if (iseparador > 0)
                            {
                                SearchString = SearchString + " and '" + SearchStringdividida + " 23:59'";
                            }

                            iseparador = 1;
                            SearchStringdividida = "";
                        }
                        else if (SearchStringcompleta[indice] != ';')
                        {
                            SearchStringdividida += SearchStringcompleta[indice];
                        }
                    }

                    Where += SearchString;
                }
                else if (
                    search.Field.Substring(0, 1) == "s" ||
                    search.Field.Substring(0, 1) == "d" ||
                    search.Field.Substring(0, 1) == "h"
                )
                {
                    var SearchString = search.Value + ";";
                    var iseparador = 0;

                    var SearchStringcompleta = SearchString.Trim();

                    for (int indice = 0; indice <= SearchStringcompleta.Length - 1; indice++)
                    {
                        if (SearchStringcompleta[indice] == ';')
                        {
                            if (iseparador == 0)
                            {
                                if (search.Field.Substring(0, 1) == "s")
                                    SearchString = " and UPPER(" + search.Field + ") between '" + SearchStringdividida + "'";
                                else
                                    SearchString = " and " + search.Field + " between '" + SearchStringdividida + "'";
                            }
                            else if (iseparador > 0)
                            {
                                SearchString = SearchString + " and '" + SearchStringdividida + "'";
                            }

                            iseparador = 1;
                            SearchStringdividida = "";
                        }
                        else if (SearchStringcompleta[indice] != ';')
                        {
                            SearchStringdividida += SearchStringcompleta[indice];
                        }
                    }

                    Where += SearchString;
                }
                else if (search.Field.Substring(0, 1) == "n" || search.Field.Substring(0, 1) == "i")
                {
                    var iseparador = 0;
                    var SearchString = search.Value + ";";

                    var SearchStringcompleta = SearchString.Trim();

                    for (int indice = 0; indice <= SearchStringcompleta.Length - 1; indice++)
                    {
                        if (SearchStringcompleta[indice] == ';')
                        {
                            if (iseparador == 0)
                            {
                                SearchString = " and " + search.Field + " between '" + SearchStringdividida + "'";
                            }
                            else if (iseparador > 0)
                            {
                                SearchString = SearchString + " and " + SearchStringdividida;
                            }

                            iseparador = 1;
                            SearchStringdividida = "";
                        }
                        else if (SearchStringcompleta[indice] != ';')
                        {
                            SearchStringdividida += SearchStringcompleta[indice];
                        }
                    }

                    Where += SearchString;
                }
            }

            //Monta Type Or
            if (search.FastSearch)
            {
                var SearchString = "";

                if (search.Field.Substring(0, 1) == "n" || search.Field.Substring(0, 1) == "i")
                {
                    if (firstValue)
                        SearchString = $" and (CAST({search.Field} as Varchar) like '%{search.Value}%'";
                    else
                        SearchString = $" or CAST({search.Field} as Varchar) like '%{search.Value}%'";
                }
                else
                {
                    if (firstValue)
                        SearchString = $" and (UPPER(remove_accents({search.Field})) like '%{search.Value}%'";
                    else
                        SearchString = $" or UPPER(remove_accents({search.Field})) like '%{search.Value}%'";
                }

                Where += SearchString;

                if (firstValue)
                    firstValue = false;

                hasFastSearch = true;
            }
        }

        var closedBlock = false;

        if (searchHelper.Ordination != null && searchHelper.Ordination.Field != null && searchHelper.Ordination.Field != "")
        {
            if (hasFastSearch)
            {
                closedBlock = true;
                Where += $") order by {searchHelper.Ordination.Field} {searchHelper.Ordination.Order}";
            }
            else
                Where += $" order by {searchHelper.Ordination.Field} {searchHelper.Ordination.Order}";
        }

        //Seta Where OffSet
        if (searchHelper.Pagination != null)
        {
            var limit = searchHelper.Pagination.RowsPerPage;
            var activePage = searchHelper.Pagination.ActivePage;

            if (limit == 0) limit = 100;

            if (activePage == 0) activePage = 1;

            var oFFSet = (activePage * limit) - limit;

            if (hasFastSearch && !closedBlock)
                Where += ") LIMIT " + limit + " OFFSET " + oFFSet;
            else
                Where += " LIMIT " + limit + " OFFSET " + oFFSet;
        }
        else
        {
            if (hasFastSearch && !closedBlock)
                Where += ")";
        }

        return Where;
    }
}
