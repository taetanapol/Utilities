﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utilities.Shared;
using Oracle.ManagedDataAccess.Client;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace Utilities
{
    /// <summary>
    /// Collections of SQL Connection for SQL Server and Oracle Database
    /// </summary>
    public static class SQL
    {
        /// <summary>
        /// Provide wrapper access to SQL Server with basic operation like ExecuteReader,ExecuteNonQuery and ExecuteScalar
        /// </summary>
        public static class SQLServer
        {
            /// <summary>
            /// Execute SELECT SQL query and return IEnumerable of specified POCO that is matching with the query columns
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="objectBuilder">How the POCO should build with each giving row of SqlDataReader</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns>IEnumerable of POCO</returns>
            /// <exception cref="Exception"/>
            public static IEnumerable<T> ExecuteReader<T>(string connectionString, string sql, IEnumerable<SqlParameter> parameters, Func<DbDataReader, T> objectBuilder, System.Data.CommandType commandType = System.Data.CommandType.Text) where T : new()
            {
                return DbConnectionBase.ExecuteReader<T, SqlConnection>(connectionString, sql, parameters, objectBuilder, commandType);
            }
            /// <summary>
            /// Execute SELECT SQL query and return IEnumerable of specified POCO that is matching with the query columns
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns>IEnumerable of POCO</returns>
            /// <exception cref="Exception"/>
            public static IEnumerable<T> ExecuteReader<T>(string connectionString, string sql, IEnumerable<SqlParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text) where T : new()
            {
                return DbConnectionBase.ExecuteReader<T, SqlConnection>(connectionString, sql, parameters, (cursor) => Data.RowBuilder<T>(cursor), commandType);
            }
            /// <summary>
            /// Execute SELECT SQL query and return IEnumerable of dynamic object
            /// </summary>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns>IEnumerable of dynamic object</returns>
            /// <exception cref="Exception"/>
            public static IEnumerable<dynamic> ExecuteReader(string connectionString, string sql, IEnumerable<SqlParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text)
            {
                return DbConnectionBase.ExecuteReader<SqlConnection>(connectionString, sql, parameters, commandType);
            }
            /// <summary>
            /// Execute SELECT SQL query and return a scalar object
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns></returns>
            /// <exception cref="Exception"/>
            public static T ExecuteScalar<T>(string connectionString, string sql, IEnumerable<SqlParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text)
            {
                return DbConnectionBase.ExecuteScalar<T, SqlConnection>(connectionString, sql, parameters, commandType);
            }
            /// <summary>
            /// Execute any non-DML SQL Query
            /// </summary>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns></returns>
            /// <exception cref="Exception"/>
            public static int ExecuteNonQuery(string connectionString, string sql, IEnumerable<SqlParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text)
            {
                return DbConnectionBase.ExecuteNonQuery<SqlConnection>(connectionString, sql, parameters, commandType);
            }
            /// <summary>
            /// Execute SELECT SQL query and return IEnumerable of specified POCO that is matching with the query columns
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="objectBuilder">How the POCO should build with each giving row of SqlDataReader</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns>IEnumerable of POCO</returns>
            /// <exception cref="Exception"/>
            public static async Task<IEnumerable<T>> ExecuteReaderAsync<T>(string connectionString, string sql, IEnumerable<SqlParameter> parameters, Func<DbDataReader, T> objectBuilder, System.Data.CommandType commandType = System.Data.CommandType.Text) where T : new()
            {
                return await DbConnectionBase.ExecuteReaderAsync<T, SqlConnection>(connectionString, sql, parameters, objectBuilder, commandType);
            }
            /// <summary>
            /// Execute SELECT SQL query and return IEnumerable of specified POCO that is matching with the query columns
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns>IEnumerable of POCO</returns>
            /// <exception cref="Exception"/>
            public static async Task<IEnumerable<T>> ExecuteReaderAsync<T>(string connectionString, string sql, IEnumerable<SqlParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text) where T : new()
            {
                return await ExecuteReaderAsync(connectionString, sql, parameters, (cursor) => Data.RowBuilder<T>(cursor), commandType);
            }
            /// <summary>
            /// Execute SELECT SQL query and return IEnumerable of dynamic object
            /// </summary>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns>IEnumerable of dynamic object</returns>
            /// <exception cref="Exception"/>
            public static async Task<IEnumerable<dynamic>> ExecuteReaderAsync(string connectionString, string sql, IEnumerable<SqlParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text)
            {
                return await DbConnectionBase.ExecuteReaderAsync<SqlConnection>(connectionString, sql, parameters, commandType);
            }
            /// <summary>
            /// Execute SELECT SQL query and return a scalar object
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns></returns>
            /// <exception cref="Exception"/>
            public static async Task<T> ExecuteScalarAsync<T>(string connectionString, string sql, IEnumerable<SqlParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text)
            {
                return await DbConnectionBase.ExecuteScalarAsync<T, SqlConnection>(connectionString, sql, parameters, commandType);
            }
            /// <summary>
            /// Execute any non-DML SQL Query
            /// </summary>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns></returns>
            /// <exception cref="Exception"/>
            public static async Task<int> ExecuteNonQueryAsync(string connectionString, string sql, IEnumerable<SqlParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text)
            {
                return await DbConnectionBase.ExecuteNonQueryAsync<SqlConnection>(connectionString, sql, parameters, commandType);
            }

        }
        /// <summary>
        /// Provide wrapper access to Oracle Database with basic operation like ExecuteReader,ExecuteNonQuery and ExecuteScalar
        /// </summary>
        public static class Oracle
        {
            /// <summary>
            /// Execute SELECT SQL query and return IEnumerable of specified POCO that is matching with the query columns
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="objectBuilder">How the POCO should build with each giving row of SqlDataReader</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns>IEnumerable of POCO</returns>
            /// <exception cref="Exception"/>
            public static IEnumerable<T> ExecuteReader<T>(string connectionString, string sql, IEnumerable<OracleParameter> parameters, Func<DbDataReader, T> objectBuilder, System.Data.CommandType commandType = System.Data.CommandType.Text) where T : new()
            {
                return DbConnectionBase.ExecuteReader<T, OracleConnection>(connectionString, sql, parameters, objectBuilder, commandType);
            }
            /// <summary>
            /// Execute SELECT SQL query and return IEnumerable of specified POCO that is matching with the query columns
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns>IEnumerable of POCO</returns>
            /// <exception cref="Exception"/>
            public static IEnumerable<T> ExecuteReader<T>(string connectionString, string sql, IEnumerable<OracleParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text) where T : new()
            {
                return DbConnectionBase.ExecuteReader<T, OracleConnection>(connectionString, sql, parameters, (cursor) => Data.RowBuilder<T>(cursor), commandType);
            }
            /// <summary>
            /// Execute SELECT SQL query and return IEnumerable of dynamic object
            /// </summary>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns>IEnumerable of dynamic object</returns>
            /// <exception cref="Exception"/>
            public static IEnumerable<dynamic> ExecuteReader(string connectionString, string sql, IEnumerable<OracleParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text)
            {
                return DbConnectionBase.ExecuteReader<OracleConnection>(connectionString, sql, parameters, commandType);
            }
            /// <summary>
            /// Execute SELECT SQL query and return a scalar object
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns></returns>
            /// <exception cref="Exception"/>
            public static T ExecuteScalar<T>(string connectionString, string sql, IEnumerable<OracleParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text)
            {
                return DbConnectionBase.ExecuteScalar<T, OracleConnection>(connectionString, sql, parameters, commandType);
            }
            /// <summary>
            /// Execute any non-DML SQL Query
            /// </summary>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns></returns>
            /// <exception cref="Exception"/>
            public static int ExecuteNonQuery(string connectionString, string sql, IEnumerable<OracleParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text)
            {
                return DbConnectionBase.ExecuteNonQuery<OracleConnection>(connectionString, sql, parameters, commandType);
            }
            /// <summary>
            /// Execute SELECT SQL query and return IEnumerable of specified POCO that is matching with the query columns
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="objectBuilder">How the POCO should build with each giving row of SqlDataReader</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns>IEnumerable of POCO</returns>
            /// <exception cref="Exception"/>
            public static async Task<IEnumerable<T>> ExecuteReaderAsync<T>(string connectionString, string sql, IEnumerable<OracleParameter> parameters, Func<DbDataReader, T> objectBuilder, System.Data.CommandType commandType = System.Data.CommandType.Text) where T : new()
            {
                return await DbConnectionBase.ExecuteReaderAsync<T, OracleConnection>(connectionString, sql, parameters, objectBuilder, commandType);
            }
            /// <summary>
            /// Execute SELECT SQL query and return IEnumerable of specified POCO that is matching with the query columns
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns>IEnumerable of POCO</returns>
            /// <exception cref="Exception"/>
            public static async Task<IEnumerable<T>> ExecuteReaderAsync<T>(string connectionString, string sql, IEnumerable<OracleParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text) where T : new()
            {
                return await ExecuteReaderAsync(connectionString, sql, parameters, (cursor) => Data.RowBuilder<T>(cursor), commandType);
            }
            /// <summary>
            /// Execute SELECT SQL query and return IEnumerable of dynamic object
            /// </summary>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns>IEnumerable of dynamic object</returns>
            /// <exception cref="Exception"/>
            public static async Task<IEnumerable<dynamic>> ExecuteReaderAsync(string connectionString, string sql, IEnumerable<OracleParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text)
            {
                return await DbConnectionBase.ExecuteReaderAsync<OracleConnection>(connectionString, sql, parameters, commandType);
            }
            /// <summary>
            /// Execute SELECT SQL query and return a scalar object
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns></returns>
            /// <exception cref="Exception"/>
            public static async Task<T> ExecuteScalarAsync<T>(string connectionString, string sql, IEnumerable<OracleParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text)
            {
                return await DbConnectionBase.ExecuteScalarAsync<T, OracleConnection>(connectionString, sql, parameters, commandType);
            }
            /// <summary>
            /// Execute any non-DML SQL Query
            /// </summary>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns></returns>
            /// <exception cref="Exception"/>
            public static async Task<int> ExecuteNonQueryAsync(string connectionString, string sql, IEnumerable<OracleParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text)
            {
                return await DbConnectionBase.ExecuteNonQueryAsync<OracleConnection>(connectionString, sql, parameters, commandType);
            }

        }
        /// <summary>
        /// Provide wrapper access to PostgreSQL Database with basic operation like ExecuteReader,ExecuteNonQuery and ExecuteScalar
        /// </summary>
        public static class PostgreSQL
        {
            /// <summary>
            /// Execute SELECT SQL query and return IEnumerable of specified POCO that is matching with the query columns
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="objectBuilder">How the POCO should build with each giving row of SqlDataReader</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns>IEnumerable of POCO</returns>
            /// <exception cref="Exception"/>
            public static IEnumerable<T> ExecuteReader<T>(string connectionString, string sql, IEnumerable<Npgsql.NpgsqlParameter> parameters, Func<DbDataReader, T> objectBuilder, System.Data.CommandType commandType = System.Data.CommandType.Text) where T : new()
            {
                return DbConnectionBase.ExecuteReader<T, Npgsql.NpgsqlConnection>(connectionString, sql, parameters, objectBuilder, commandType);
            }
            /// <summary>
            /// Execute SELECT SQL query and return IEnumerable of specified POCO that is matching with the query columns
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns>IEnumerable of POCO</returns>
            /// <exception cref="Exception"/>
            public static IEnumerable<T> ExecuteReader<T>(string connectionString, string sql, IEnumerable<Npgsql.NpgsqlParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text) where T : new()
            {
                return DbConnectionBase.ExecuteReader<T, Npgsql.NpgsqlConnection>(connectionString, sql, parameters, (cursor) => Data.RowBuilder<T>(cursor), commandType);
            }
            /// <summary>
            /// Execute SELECT SQL query and return IEnumerable of dynamic object
            /// </summary>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns>IEnumerable of dynamic object</returns>
            /// <exception cref="Exception"/>
            public static IEnumerable<dynamic> ExecuteReader(string connectionString, string sql, IEnumerable<Npgsql.NpgsqlParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text)
            {
                return DbConnectionBase.ExecuteReader<Npgsql.NpgsqlConnection>(connectionString, sql, parameters, commandType);
            }
            /// <summary>
            /// Execute SELECT SQL query and return a scalar object
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns></returns>
            /// <exception cref="Exception"/>
            public static T ExecuteScalar<T>(string connectionString, string sql, IEnumerable<Npgsql.NpgsqlParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text)
            {
                return DbConnectionBase.ExecuteScalar<T, Npgsql.NpgsqlConnection>(connectionString, sql, parameters, commandType);
            }
            /// <summary>
            /// Execute any non-DML SQL Query
            /// </summary>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns></returns>
            /// <exception cref="Exception"/>
            public static int ExecuteNonQuery(string connectionString, string sql, IEnumerable<Npgsql.NpgsqlParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text)
            {
                return DbConnectionBase.ExecuteNonQuery<Npgsql.NpgsqlConnection>(connectionString, sql, parameters, commandType);
            }
            /// <summary>
            /// Execute SELECT SQL query and return IEnumerable of specified POCO that is matching with the query columns
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="objectBuilder">How the POCO should build with each giving row of SqlDataReader</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns>IEnumerable of POCO</returns>
            /// <exception cref="Exception"/>
            public static async Task<IEnumerable<T>> ExecuteReaderAsync<T>(string connectionString, string sql, IEnumerable<Npgsql.NpgsqlParameter> parameters, Func<DbDataReader, T> objectBuilder, System.Data.CommandType commandType = System.Data.CommandType.Text) where T : new()
            {
                return await DbConnectionBase.ExecuteReaderAsync<T, Npgsql.NpgsqlConnection>(connectionString, sql, parameters, objectBuilder, commandType);
            }
            /// <summary>
            /// Execute SELECT SQL query and return IEnumerable of specified POCO that is matching with the query columns
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns>IEnumerable of POCO</returns>
            /// <exception cref="Exception"/>
            public static async Task<IEnumerable<T>> ExecuteReaderAsync<T>(string connectionString, string sql, IEnumerable<Npgsql.NpgsqlParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text) where T : new()
            {
                return await ExecuteReaderAsync(connectionString, sql, parameters, (cursor) => Data.RowBuilder<T>(cursor), commandType);
            }
            /// <summary>
            /// Execute SELECT SQL query and return IEnumerable of dynamic object
            /// </summary>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns>IEnumerable of dynamic object</returns>
            /// <exception cref="Exception"/>
            public static async Task<IEnumerable<dynamic>> ExecuteReaderAsync(string connectionString, string sql, IEnumerable<Npgsql.NpgsqlParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text)
            {
                return await DbConnectionBase.ExecuteReaderAsync<Npgsql.NpgsqlConnection>(connectionString, sql, parameters, commandType);
            }
            /// <summary>
            /// Execute SELECT SQL query and return a scalar object
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns></returns>
            /// <exception cref="Exception"/>
            public static async Task<T> ExecuteScalarAsync<T>(string connectionString, string sql, IEnumerable<Npgsql.NpgsqlParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text)
            {
                return await DbConnectionBase.ExecuteScalarAsync<T, Npgsql.NpgsqlConnection>(connectionString, sql, parameters, commandType);
            }
            /// <summary>
            /// Execute any non-DML SQL Query
            /// </summary>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns></returns>
            /// <exception cref="Exception"/>
            public static async Task<int> ExecuteNonQueryAsync(string connectionString, string sql, IEnumerable<Npgsql.NpgsqlParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text)
            {
                return await DbConnectionBase.ExecuteNonQueryAsync<Npgsql.NpgsqlConnection>(connectionString, sql, parameters, commandType);
            }

        }
        /// <summary>
        /// Provide wrapper access to MySQL Database with basic operation like ExecuteReader,ExecuteNonQuery and ExecuteScalar
        /// </summary>
        public static class MySQL
        {
            /// <summary>
            /// Execute SELECT SQL query and return IEnumerable of specified POCO that is matching with the query columns
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="objectBuilder">How the POCO should build with each giving row of SqlDataReader</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns>IEnumerable of POCO</returns>
            /// <exception cref="Exception"/>
            public static IEnumerable<T> ExecuteReader<T>(string connectionString, string sql, IEnumerable<MySqlParameter> parameters, Func<DbDataReader, T> objectBuilder, System.Data.CommandType commandType = System.Data.CommandType.Text) where T : new()
            {
                return DbConnectionBase.ExecuteReader<T, MySql.Data.MySqlClient.MySqlConnection>(connectionString, sql, parameters, objectBuilder, commandType);
            }
            /// <summary>
            /// Execute SELECT SQL query and return IEnumerable of specified POCO that is matching with the query columns
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns>IEnumerable of POCO</returns>
            /// <exception cref="Exception"/>
            public static IEnumerable<T> ExecuteReader<T>(string connectionString, string sql, IEnumerable<MySqlParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text) where T : new()
            {
                return DbConnectionBase.ExecuteReader<T, MySql.Data.MySqlClient.MySqlConnection>(connectionString, sql, parameters, (cursor) => Data.RowBuilder<T>(cursor), commandType);
            }
            /// <summary>
            /// Execute SELECT SQL query and return IEnumerable of dynamic object
            /// </summary>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns>IEnumerable of dynamic object</returns>
            /// <exception cref="Exception"/>
            public static IEnumerable<dynamic> ExecuteReader(string connectionString, string sql, IEnumerable<MySqlParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text)
            {
                return DbConnectionBase.ExecuteReader<MySql.Data.MySqlClient.MySqlConnection>(connectionString, sql, parameters, commandType);
            }
            /// <summary>
            /// Execute SELECT SQL query and return a scalar object
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns></returns>
            /// <exception cref="Exception"/>
            public static T ExecuteScalar<T>(string connectionString, string sql, IEnumerable<MySqlParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text)
            {
                return DbConnectionBase.ExecuteScalar<T, MySql.Data.MySqlClient.MySqlConnection>(connectionString, sql, parameters, commandType);
            }
            /// <summary>
            /// Execute any non-DML SQL Query
            /// </summary>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns></returns>
            /// <exception cref="Exception"/>
            public static int ExecuteNonQuery(string connectionString, string sql, IEnumerable<MySqlParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text)
            {
                return DbConnectionBase.ExecuteNonQuery<MySql.Data.MySqlClient.MySqlConnection>(connectionString, sql, parameters, commandType);
            }
            /// <summary>
            /// Execute SELECT SQL query and return IEnumerable of specified POCO that is matching with the query columns
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="objectBuilder">How the POCO should build with each giving row of SqlDataReader</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns>IEnumerable of POCO</returns>
            /// <exception cref="Exception"/>
            public static async Task<IEnumerable<T>> ExecuteReaderAsync<T>(string connectionString, string sql, IEnumerable<MySqlParameter> parameters, Func<DbDataReader, T> objectBuilder, System.Data.CommandType commandType = System.Data.CommandType.Text) where T : new()
            {
                return await DbConnectionBase.ExecuteReaderAsync<T, MySql.Data.MySqlClient.MySqlConnection>(connectionString, sql, parameters, objectBuilder, commandType);
            }
            /// <summary>
            /// Execute SELECT SQL query and return IEnumerable of specified POCO that is matching with the query columns
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns>IEnumerable of POCO</returns>
            /// <exception cref="Exception"/>
            public static async Task<IEnumerable<T>> ExecuteReaderAsync<T>(string connectionString, string sql, IEnumerable<MySqlParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text) where T : new()
            {
                return await ExecuteReaderAsync(connectionString, sql, parameters, (cursor) => Data.RowBuilder<T>(cursor), commandType);
            }
            /// <summary>
            /// Execute SELECT SQL query and return IEnumerable of dynamic object
            /// </summary>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns>IEnumerable of dynamic object</returns>
            /// <exception cref="Exception"/>
            public static async Task<IEnumerable<dynamic>> ExecuteReaderAsync(string connectionString, string sql, IEnumerable<MySqlParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text)
            {
                return await DbConnectionBase.ExecuteReaderAsync<MySql.Data.MySqlClient.MySqlConnection>(connectionString, sql, parameters, commandType);
            }
            /// <summary>
            /// Execute SELECT SQL query and return a scalar object
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns></returns>
            /// <exception cref="Exception"/>
            public static async Task<T> ExecuteScalarAsync<T>(string connectionString, string sql, IEnumerable<MySqlParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text)
            {
                return await DbConnectionBase.ExecuteScalarAsync<T, MySql.Data.MySqlClient.MySqlConnection>(connectionString, sql, parameters, commandType);
            }
            /// <summary>
            /// Execute any non-DML SQL Query
            /// </summary>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns></returns>
            /// <exception cref="Exception"/>
            public static async Task<int> ExecuteNonQueryAsync(string connectionString, string sql, IEnumerable<MySqlParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text)
            {
                return await DbConnectionBase.ExecuteNonQueryAsync<MySql.Data.MySqlClient.MySqlConnection>(connectionString, sql, parameters, commandType);
            }

        }
        /// <summary>
        /// Provide wrapper access to Any Database with basic operation like ExecuteReader,ExecuteNonQuery and ExecuteScalar
        /// </summary>
        private static class DbConnectionBase
        {
            /// <summary>
            /// Execute SELECT SQL query and return IEnumerable of specified POCO that is matching with the query columns
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="objectBuilder">How the POCO should build with each giving row of SqlDataReader</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns>IEnumerable of POCO</returns>
            /// <exception cref="Exception"/>
            public static IEnumerable<T> ExecuteReader<T, TDatabaseType>(string connectionString, string sql, IEnumerable<DbParameter> parameters, Func<DbDataReader, T> objectBuilder, System.Data.CommandType commandType = System.Data.CommandType.Text)
                where T : new()
                where TDatabaseType : DbConnection, new()
            {
                try
                {
                    List<T> result = new List<T>();
                    using (var connection = new TDatabaseType())
                    {
                        connection.ConnectionString = connectionString;
                        connection.Open();
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = sql;
                            command.CommandType = commandType;
                            if (parameters != null)
                            {
                                foreach (var parameter in parameters)
                                {
                                    command.Parameters.Add(parameter);
                                }
                            }
                            using (var cursor = command.ExecuteReader())
                            {
                                while (cursor.Read())
                                {
                                    result.Add(objectBuilder(cursor));
                                }
                            }
                        }
                        connection.Close();
                    }
                    return result;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            /// <summary>
            /// Execute SELECT SQL query and return IEnumerable of specified POCO that is matching with the query columns
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns>IEnumerable of POCO</returns>
            /// <exception cref="Exception"/>
            public static IEnumerable<T> ExecuteReader<T, TDatabaseType>(string connectionString, string sql, IEnumerable<DbParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text)
                where T : new()
                where TDatabaseType : DbConnection, new()
            {
                return ExecuteReader<T, TDatabaseType>(connectionString, sql, parameters, (cursor) => Data.RowBuilder<T>(cursor), commandType);
            }
            /// <summary>
            /// Execute SELECT SQL query and return IEnumerable of dynamic object
            /// </summary>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns>IEnumerable of dynamic object</returns>
            /// <exception cref="Exception"/>
            public static IEnumerable<dynamic> ExecuteReader<TDatabaseType>(string connectionString, string sql, IEnumerable<DbParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text)
            where TDatabaseType : DbConnection, new()
            {
                try
                {
                    List<dynamic> result = new List<dynamic>();
                    using (var connection = new TDatabaseType())
                    {
                        connection.ConnectionString = connectionString;
                        connection.Open();
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = sql;
                            command.CommandType = commandType;
                            if (parameters != null)
                            {
                                foreach (var parameter in parameters)
                                {
                                    command.Parameters.Add(parameter);
                                }
                            }
                            using (var cursor = command.ExecuteReader())
                            {
                                var columns = Enumerable.Range(0, cursor.FieldCount).Select(cursor.GetName).ToList();
                                while (cursor.Read())
                                {
                                    result.Add(Data.RowBuilder(cursor, columns));
                                }
                            }
                        }
                        connection.Close();
                    }
                    return result;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            /// <summary>
            /// Execute SELECT SQL query and return a scalar object
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns></returns>
            /// <exception cref="Exception"/>
            public static T ExecuteScalar<T, TDatabaseType>(string connectionString, string sql, IEnumerable<DbParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text)
            where TDatabaseType : DbConnection, new()
            {
                try
                {
                    T result = default;
                    using (var connection = new TDatabaseType())
                    {
                        connection.ConnectionString = connectionString;
                        connection.Open();
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = sql;
                            command.CommandType = commandType;
                            if (parameters != null)
                            {
                                foreach (var parameter in parameters)
                                {
                                    command.Parameters.Add(parameter);
                                }
                            }
                            result = (T)command.ExecuteScalar();
                        }
                        connection.Close();
                    }
                    return result;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            /// <summary>
            /// Execute any non-DML SQL Query
            /// </summary>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns></returns>
            /// <exception cref="Exception"/>
            public static int ExecuteNonQuery<TDatabaseType>(string connectionString, string sql, IEnumerable<DbParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text)
                where TDatabaseType : DbConnection, new()
            {
                try
                {
                    int result = -1;
                    using (var connection = new TDatabaseType())
                    {
                        connection.ConnectionString = connectionString;
                        connection.Open();
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = sql;
                            command.CommandType = commandType;
                            if (parameters != null)
                            {
                                foreach (var parameter in parameters)
                                {
                                    command.Parameters.Add(parameter);
                                }
                            }
                            result = command.ExecuteNonQuery();
                        }
                        connection.Close();
                    }
                    return result;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            /// <summary>
            /// Execute SELECT SQL query and return IEnumerable of specified POCO that is matching with the query columns
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="objectBuilder">How the POCO should build with each giving row of SqlDataReader</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns>IEnumerable of POCO</returns>
            /// <exception cref="Exception"/>
            public static async Task<IEnumerable<T>> ExecuteReaderAsync<T, TDatabaseType>(string connectionString, string sql, IEnumerable<DbParameter> parameters, Func<DbDataReader, T> objectBuilder, System.Data.CommandType commandType = System.Data.CommandType.Text) where T : new()
            where TDatabaseType : DbConnection, new()
            {
                try
                {
                    List<T> result = new List<T>();
                    using (var connection = new TDatabaseType())
                    {
                        connection.ConnectionString = connectionString;
                        await connection.OpenAsync();
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = sql;
                            command.CommandType = commandType;
                            if (parameters != null)
                            {
                                foreach (var parameter in parameters)
                                {
                                    command.Parameters.Add(parameter);
                                }
                            }
                            using (var cursor = await command.ExecuteReaderAsync())
                            {
                                while (await cursor.ReadAsync())
                                {
                                    result.Add(objectBuilder(cursor));
                                }
                            }
                        }
                        connection.Close();
                    }
                    return result;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            /// <summary>
            /// Execute SELECT SQL query and return IEnumerable of specified POCO that is matching with the query columns
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns>IEnumerable of POCO</returns>
            /// <exception cref="Exception"/>
            public static async Task<IEnumerable<T>> ExecuteReaderAsync<T, TDatabaseType>(string connectionString, string sql, IEnumerable<DbParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text) where T : new()
            where TDatabaseType : DbConnection, new()
            {
                return await ExecuteReaderAsync<T, TDatabaseType>(connectionString, sql, parameters, (cursor) => Data.RowBuilder<T>(cursor), commandType);
            }
            /// <summary>
            /// Execute SELECT SQL query and return IEnumerable of dynamic object
            /// </summary>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns>IEnumerable of dynamic object</returns>
            /// <exception cref="Exception"/>
            public static async Task<IEnumerable<dynamic>> ExecuteReaderAsync<TDatabaseType>(string connectionString, string sql, IEnumerable<DbParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text)
            where TDatabaseType : DbConnection, new()
            {
                try
                {
                    List<dynamic> result = new List<dynamic>();
                    using (var connection = new TDatabaseType())
                    {
                        connection.ConnectionString = connectionString;
                        await connection.OpenAsync();
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = sql;
                            command.CommandType = commandType;
                            if (parameters != null)
                            {
                                foreach (var parameter in parameters)
                                {
                                    command.Parameters.Add(parameter);
                                }
                            }
                            using (var cursor = await command.ExecuteReaderAsync())
                            {
                                var columns = Enumerable.Range(0, cursor.FieldCount).Select(cursor.GetName).ToList();
                                while (await cursor.ReadAsync())
                                {
                                    result.Add(Data.RowBuilder(cursor, columns));
                                }
                            }
                        }
                        connection.Close();
                    }
                    return result;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            /// <summary>
            /// Execute SELECT SQL query and return a scalar object
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns></returns>
            /// <exception cref="Exception"/>
            public static async Task<T> ExecuteScalarAsync<T, TDatabaseType>(string connectionString, string sql, IEnumerable<DbParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text)
            where TDatabaseType : DbConnection, new()
            {
                try
                {
                    T result = default;
                    using (var connection = new TDatabaseType())
                    {
                        connection.ConnectionString = connectionString;
                        await connection.OpenAsync();
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = sql;
                            command.CommandType = commandType;
                            if (parameters != null)
                            {
                                foreach (var parameter in parameters)
                                {
                                    command.Parameters.Add(parameter);
                                }
                            }
                            result = (T)(await command.ExecuteScalarAsync());
                        }
                        connection.Close();
                    }
                    return result;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            /// <summary>
            /// Execute any non-DML SQL Query
            /// </summary>
            /// <param name="connectionString">Connection string to database</param>
            /// <param name="sql">Any SELECT SQL that you want to perform with/without parameterized parameters (Do not directly put sql parameter in this parameter)</param>
            /// <param name="parameters">SQL parameters according to the sql parameter</param>
            /// <param name="commandType">Type of SQL Command</param>
            /// <returns></returns>
            /// <exception cref="Exception"/>
            public static async Task<int> ExecuteNonQueryAsync<TDatabaseType>(string connectionString, string sql, IEnumerable<DbParameter> parameters = null, System.Data.CommandType commandType = System.Data.CommandType.Text)
            where TDatabaseType : DbConnection, new()
            {
                try
                {
                    int result = -1;
                    using (var connection = new TDatabaseType())
                    {
                        connection.ConnectionString = connectionString;
                        await connection.OpenAsync();
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = sql;
                            command.CommandType = commandType;
                            if (parameters != null)
                            {
                                foreach (var parameter in parameters)
                                {
                                    command.Parameters.Add(parameter);
                                }
                            }
                            result = await command.ExecuteNonQueryAsync();
                        }
                        connection.Close();
                    }
                    return result;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
