using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

static class MyDataBase
{
    private const string fileName = "dice.db";
    private static string DBPath;
    private static SqliteConnection connection;
    private static SqliteCommand command;

    static MyDataBase()
    {
        DBPath = GetDatabasePath();
    }

    /// <summary> Возвращает путь к БД. Если её нет в нужной папке на Андроиде, то копирует её с исходного apk файла. </summary>
    private static string GetDatabasePath()
    {
    #if UNITY_EDITOR
        return Path.Combine(Application.streamingAssetsPath, fileName);
    #endif
    #if UNITY_STANDALONE
        string filePath = Path.Combine(Application.dataPath, fileName);
        if(!File.Exists(filePath)) UnpackDatabase(filePath);
        return filePath;
    #elif UNITY_ANDROID
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        if(!File.Exists(filePath)) UnpackDatabase(filePath);
        return filePath;
    #endif
    }

    public static void CheckedDate()
    {
        string sql_is_anim = "SELECT count(*) FROM \"settings\" WHERE name_setting = 'is_anim'";
        string sql_speed = "SELECT count(*) FROM \"settings\" WHERE name_setting = 'speed'";
        string sql_type_res_out = "SELECT count(*) FROM \"settings\" WHERE name_setting = 'type_res_out'";
        string sql_selected_them = "SELECT count(*) FROM \"settings\" WHERE name_setting = 'selected_them'";

        if (GetStrQuery(sql_is_anim) == "0")
        {
            ExecuteQueryWithoutAnswer("INSERT INTO \"settings\" (\"name_setting\", \"value\") VALUES ('is_anim', 't')");
        }
        if (GetStrQuery(sql_speed) == "0")
        {
            ExecuteQueryWithoutAnswer("INSERT INTO \"settings\" (\"name_setting\", \"value\") VALUES ('speed', '6')");
        }
        if (GetStrQuery(sql_type_res_out) == "0")
        {
            ExecuteQueryWithoutAnswer("INSERT INTO \"settings\" (\"name_setting\", \"value\") VALUES ('type_res_out', '0')");
        }
        if (GetStrQuery(sql_selected_them) == "0")
        {
            ExecuteQueryWithoutAnswer("INSERT INTO \"settings\" (\"name_setting\", \"value\") VALUES ('selected_them', '0')");
        }
    }

    /// <summary> Распаковывает базу данных в указанный путь. </summary>
    /// <param name="toPath"> Путь в который нужно распаковать базу данных. </param>
    private static void UnpackDatabase(string toPath)
    {
        string fromPath = Path.Combine(Application.streamingAssetsPath, fileName);

        WWW reader = new WWW(fromPath);
        while (!reader.isDone) { }

        File.WriteAllBytes(toPath, reader.bytes);
    }

    /// <summary> Этот метод открывает подключение к БД. </summary>
    private static void OpenConnection()
    {
        connection = new SqliteConnection("Data Source=" + DBPath);
        command = new SqliteCommand(connection);
        connection.Open();
    }

    /// <summary> Этот метод закрывает подключение к БД. </summary>
    public static void CloseConnection()
    {
        connection.Close();
        command.Dispose();
    }

    /// <summary> Этот метод выполняет запрос query. </summary>
    /// <param name="query"> Собственно запрос. </param>
    public static void ExecuteQueryWithoutAnswer(string query)
    {
        OpenConnection();
        command.CommandText = query;
        command.ExecuteNonQuery();
        CloseConnection();
    }

    /// <summary> Этот метод выполняет запрос query и возвращает ответ запроса. </summary>
    /// <param name="query"> Собственно запрос. </param>
    /// <returns> Возвращает значение 1 строки 1 столбца, если оно имеется. </returns>
    public static string ExecuteQueryWithAnswer(string query)
    {
        OpenConnection();
        command.CommandText = query;
        var answer = command.ExecuteScalar();
        CloseConnection();

        if (answer != null) return answer.ToString();
        else return null;
    }

    /// <summary> Этот метод возвращает таблицу, которая является результатом выборки запроса query. </summary>
    /// <param name="query"> Собственно запрос. </param>
    public static DataTable GetTable(string query)
    {
        OpenConnection();

        SqliteDataAdapter adapter = new SqliteDataAdapter(query, connection);

        DataSet DS = new DataSet();
        adapter.Fill(DS);
        adapter.Dispose();

        CloseConnection();

        return DS.Tables[0];
    }

    public static string GetStrQuery(string query)
    {
        OpenConnection();
        command.CommandText = query;
        var answer = command.ExecuteScalar();
        CloseConnection();

        if (answer != null) return answer.ToString();
        else return null;
    }

    public static string GetParamSetting(string nameField)
    {
        OpenConnection();
        command.CommandText = $"SELECT value FROM \"settings\" WHERE name_setting = '{nameField}'";
        var answer = command.ExecuteScalar();
        CloseConnection();

        if (answer != null) return answer.ToString();
        else return null;
    }

    public static void SetParamBoolSetting(string nameField, bool bvalue)
    {
        string value = bvalue ? "t" : "f";

        OpenConnection();
        command.CommandText = $"UPDATE \"settings\" SET \"value\" = '{value}' WHERE name_setting = '{nameField}'";
        command.ExecuteNonQuery();
        CloseConnection();
    }

    public static void SetParamIntSetting(string nameField, int bvalue)
    {
        OpenConnection();
        command.CommandText = $"UPDATE \"settings\" SET \"value\" = '{bvalue}' WHERE name_setting = '{nameField}'";
        command.ExecuteNonQuery();
        CloseConnection();
    }

    public static void InsertResult(string sreRes)
    {
        OpenConnection();
        command.CommandText = $"INSERT INTO \"history\" (\"date\", \"result\") VALUES ('{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', '{sreRes}')";
        command.ExecuteNonQuery();
        CloseConnection();
    }

    public static DataTable GetListHistory()
    {
        try
        {
            string query = "SELECT date, result FROM \"history\" ORDER BY \"date\" DESC LIMIT 0,20";

            OpenConnection();

            SqliteDataAdapter adapter = new SqliteDataAdapter(query, connection);

            DataSet DS = new DataSet();
            adapter.Fill(DS);
            adapter.Dispose();

            CloseConnection();

            return DS.Tables[0];
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }

        return new DataTable();
    }
}