using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Threading;

namespace Projeto_8
{
    [DataContract]
    internal class DataBase
    {
        [DataMember]
        private List<CadastroPessoa> pessoaList;
        private string DataPath;
        private Mutex mFile;
        private Mutex mList;
        private bool mLock = true;

        public void addInfo(CadastroPessoa pPessoa)
        {
            mList.WaitOne();
            pessoaList.Add(pPessoa);
            mList.ReleaseMutex();
            new Thread(() =>
            {
                mLock = false;
                mFile.WaitOne();
                Serializador.Serializar(DataPath, this);
                mFile.ReleaseMutex();
                mLock = true;
            }).Start();
           
        }

        public List<CadastroPessoa> Search(string pValor)
        {
            mList.WaitOne();
            List<CadastroPessoa> temp = pessoaList.Where(x => x.NumeroDoc == pValor).ToList();
            mList.ReleaseMutex();
            if(temp.Count > 0)
            {
                return temp;
                }
            else
            {
                return null;
            }
            
        }

        public List<CadastroPessoa> Remove(string pValor)
        {
            mList.WaitOne();
            List<CadastroPessoa> temp = pessoaList.Where(x => x.NumeroDoc == pValor).ToList();
            mList.ReleaseMutex();
            if (temp.Count > 0)
            {
                foreach (CadastroPessoa pessoa in temp)
                {
                    mList.WaitOne();
                    pessoaList.Remove(pessoa);
                    mList.ReleaseMutex();
                }
                new Thread(() =>
                {
                    mLock = false;
                    mFile.WaitOne();
                    Serializador.Serializar(DataPath, this);
                    mFile.ReleaseMutex();
                    mLock = true;
                }).Start();
                return temp;
            }
            else
            {
                return null;
            }
        }

        public DataBase(string dataPath)
        {
            DataPath = dataPath;
            mFile = new Mutex();
            mList = new Mutex();
            
            new Thread(() => {
                mLock = false;
                mFile.WaitOne();
                DataBase DataTemp = Serializador.unSerialize(DataPath);
                mFile.ReleaseMutex();
                mList.WaitOne();
                if (DataTemp != null)
                {
                    pessoaList = DataTemp.pessoaList;
                }
                else
                {
                    pessoaList = new List<CadastroPessoa>();
                }
                mLock = true;
                mList.ReleaseMutex();
            }).Start();
            
            
        }

        public bool Lock()
        {
            return mLock;
        }
    }
}
