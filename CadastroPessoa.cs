using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Projeto_8
{
    [DataContract]
    internal class CadastroPessoa
    {
        //Atributos
        [DataMember]
        private string nome;
        [DataMember]
        private string numeroDoc;
        [DataMember]
        private DateTime data;
        [DataMember]
        private string nomeRua;
        [DataMember]
        private UInt32 nDoc;

        public CadastroPessoa(string nome, string numeroDoc, DateTime data, string nomeRua, uint nDoc)
        {
            this.nome = nome;
            this.numeroDoc = numeroDoc;
            this.data = data;
            this.nomeRua = nomeRua;
            this.nDoc = nDoc;
        }

        public string Nome
        {
            get{ return nome; }
            set { nome = value; }
        }

        public string NumeroDoc { get {  return numeroDoc; } set {  numeroDoc = value; } }
        public DateTime Data { get { return data; } set {  data = value; } }
        public string NomeRua { get { return nomeRua; } set { nomeRua = value; } }
        public UInt32 NDoc { get { return nDoc; } set {  nDoc = value; } }
    }
}
