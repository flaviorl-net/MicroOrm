using FluentAssertions;
using MicroORM;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Xunit;

namespace MicroORMTests
{
    public class MicroORMTests
    {
        private readonly string strConn = string.Empty;

        public MicroORMTests()
        {
            strConn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Flavio\source\Repos\MicroORM\tests\MicroORMTests\Database1.mdf;Integrated Security=True";
        }

        [Fact]
        public void GetTest()
        {
            Cliente cliente;
            var param = new { Nome = "mel" };

            using SqlConnection sqlConnection = new SqlConnection(strConn);
            cliente = sqlConnection.Get<Cliente>(param);

            cliente.Should().NotBeNull();
            cliente.Nome.Should().Be("mel");
        }

        [Fact]
        public void GetAllTest()
        {
            List<Cliente> clienteList;
            
            using SqlConnection sqlConnection = new SqlConnection(strConn);
            clienteList = sqlConnection.GetAll<Cliente>().ToList();

            clienteList.Should().NotBeNull();
            clienteList.Should().NotBeEmpty();
            clienteList.Should().HaveCount(7);
        }

        [Fact]
        public void GetAllWithParamTest()
        {
            List<Cliente> clienteList;
            var param = new { Ativo = true };

            using SqlConnection sqlConnection = new SqlConnection(strConn);
            clienteList = sqlConnection.GetAll<Cliente>(param).ToList();

            clienteList.Should().NotBeNull();
            clienteList.Should().NotBeEmpty();
            clienteList.Should().HaveCount(4);
        }

        [Fact]
        public void QueryTest()
        {
            List<Cliente> clienteList;

            using SqlConnection sqlConnection = new SqlConnection(strConn);
            clienteList = sqlConnection.Query<Cliente>("select * from Cliente with(nolock)").ToList();

            clienteList.Should().NotBeNull();
            clienteList.Should().NotBeEmpty();
            clienteList.Should().HaveCount(7);
        }

        [Fact]
        public void QueryWithParamTest()
        {
            List<Cliente> clienteList;
            var param = new { Ativo = true };

            using SqlConnection sqlConnection = new SqlConnection(strConn);
            clienteList = sqlConnection.Query<Cliente>("select * from Cliente with(nolock) where Ativo = @Ativo", param).ToList();

            clienteList.Should().NotBeNull();
            clienteList.Should().NotBeEmpty();
            clienteList.Should().HaveCount(4);
        }

        [Fact]
        public void QueryFirstTest()
        {
            var param = new { Nome = "mel" };

            Cliente cliente;
            
            using (SqlConnection sqlConnection = new SqlConnection(strConn))
            {
                cliente = sqlConnection.QueryFirst<Cliente>("select * from Cliente with(nolock) where Nome = @nome", param);
            }

            cliente.Should().NotBeNull();
            cliente.Nome.Should().Be("mel");
        }

        [Fact]
        public void InsertTest()
        {
            var cliente = new Cliente
            {
                CPF = "12345678901",
                Nome = "Novo Cliente",
                DtNascimento = System.DateTime.Now.AddYears(-30),
                Celular = "11987654321",
                Email = "novo.cliente@example.com",
                Ativo = true,
                IDVinculo = 1
            };
            
            int rowsAffected;
            
            using (SqlConnection sqlConnection = new SqlConnection(strConn))
            {
                rowsAffected = sqlConnection.Insert(cliente);
            }
            
            rowsAffected.Should().Be(1);
        }
    }
}



