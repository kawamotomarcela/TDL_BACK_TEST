﻿namespace TDLembretes.Models
{
    public class Usuario
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int Pontos { get; set; } = 0;
        public string Telefone { get; set; }
        public List<UsuarioTarefasPersonalizadas> TarefasPersonalizadas { get; set; } = new();
        public List<UsuarioTarefasOficiais> TarefasOficiais { get; set; } = new();

        private Usuario() { }

        public Usuario (string id, string nome, string email, string senha, int pontos, string telefone, List<UsuarioTarefasPersonalizadas> tarefasPersonalizadas, List<UsuarioTarefasOficiais> tarefasOficiais)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Senha = senha;
            Pontos = pontos;
            Telefone = telefone;
            TarefasPersonalizadas = tarefasPersonalizadas;
            TarefasOficiais = tarefasOficiais;
        }
    }
}
