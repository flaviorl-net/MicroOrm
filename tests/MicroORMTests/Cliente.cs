using System;

namespace MicroORMTests
{
    public class Cliente
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public DateTime DtNascimento { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public int IDVinculo { get; set; }
    }
}
