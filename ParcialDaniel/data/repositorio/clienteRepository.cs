﻿using Dapper;
using model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.repositorio
{
    public class clienteRepository : iClienteRepository
    {
        public readonly mysqlConfig _connection;
        public clienteRepository(mysqlConfig connection)
        {
            _connection = connection;
        }
        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connection._connectionString);
        }

        public async Task<bool> deleteCliente(int id)
        {
            var db = dbConnection();
            var sql = @"delete from cliente where idCliente=@Id";
            var result = await db.ExecuteAsync(sql, new { id });
            return result > 0;
        }

        public Task<cliente> getClienteById(int id)
        {
            var db = dbConnection();
            var consulta = @"select * from cliente where idCliente=@Id";
            return db.QueryFirstOrDefaultAsync<cliente>(consulta, new { Id = id });
        }

        public Task<IEnumerable<cliente>> getClientes()
        {
            var db = dbConnection();
            var consulta = @"select * from cliente";
            return db.QueryAsync<cliente>(consulta);
        }

        public async Task<bool> insertCliente(cliente cliente)
        {
            var db = dbConnection();
            var sql = @"insert into cliente(Nombre, Documento) values(@Nombre, @Documento)";
            var result = await db.ExecuteAsync(sql, new { cliente.Nombre, cliente.Documento });
            return result > 0;
        }

        public async Task<bool> updateCliente(cliente cliente)
        {
            var db = dbConnection();
            var sql = @"update cliente set
                        Nombre=@Nombre,
                        Documento=@Documento
                        where (idCliente=@IdCliente)";
            var result = await db.ExecuteAsync(sql, new { cliente.Nombre, cliente.Documento, cliente.idCliente});
            return result > 0;
        }
        
    }
}
