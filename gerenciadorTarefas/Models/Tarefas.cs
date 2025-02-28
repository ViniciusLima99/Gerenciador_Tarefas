using System;

namespace gerenciadorTarefas.Models
{
    public class Tarefas
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataConclusao { get; set; } = DateTime.Now; 
        public bool Concluida { get; set; }
    }
}
