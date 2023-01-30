// See https://aka.ms/new-console-template for more information

//
using System;
using System.Drawing;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;
using System.Threading;
using System.IO.Pipes;
using System.Reflection;

namespace redimensionador
{
    internal class Program
    {
        
        public static object imagem;
        private static object altura;

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("iniciando o redimensionador");
                System.Threading.Thread thread = new System.Threading.Thread(redimensionar);
                thread.Start();
            }
            catch (Exception erro)
            {
                Console.WriteLine("deu pau1");
            }
        }
        static void redimensionar()
        {
            try
            {
                #region "Diretórios"
                string entradaimagens = @"C:\Users\abel alves de souza\Documents\C# aprendendo\testes\imagem - alterar tamanho\entradaimagens";
                string redimensionadas = @"C:\Users\abel alves de souza\Documents\C# aprendendo\testes\imagem - alterar tamanho\redimensionadas";
                string finalizadas = @"C:\Users\abel alves de souza\Documents\C# aprendendo\testes\imagem - alterar tamanho\finalizadas";


                if (!Directory.Exists(entradaimagens))
                {
                    Console.WriteLine($"pasta {entradaimagens} criada");
                    Directory.CreateDirectory(entradaimagens);

                }
                if (!Directory.Exists(redimensionadas))
                {
                    Console.WriteLine($"pasta {redimensionadas} criada");
                    Directory.CreateDirectory(redimensionadas);

                }
                if (!Directory.Exists(finalizadas))
                {
                    Console.WriteLine($"pasta {finalizadas} criada");
                    Directory.CreateDirectory(finalizadas);

                }
                #endregion
                while (true)
                {
                    //tamanho ao qual vai dimensionar a altura
                    int novaAltura = 200;
                    //olhar para pasta de entrada
                    //tendo arquivos, irá redimensionar
                    var imagensnapasta = Directory.EnumerateFiles(entradaimagens);

                    foreach (var arquivo in imagensnapasta)
                    {
                        FileStream fileStream = new FileStream(arquivo, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                        FileInfo fileinfo = new FileInfo(arquivo);

                        //da nome e caminho para salvar os arquivos
                        //string caminho = Environment.CurrentDirectory + @"\" + redimensionadas + fileinfo.Name + DateTime.Now.Millisecond.ToString();
                        //redimensiona
                        //Redimensionador(Image.FromStream(fileStream), int altura, string redimensionadas);
                        redimensionador(Image.FromStream(fileStream), novaAltura, redimensionadas);
                        //fecha o arquivo

                        fileStream.Close();
                        //cria o arquivo e salva com o nome desejado 
                        //string caminho = Environment.CurrentDirectory + @"\" + fileinfo.Name+"_"+ DateTime.Now.Millisecond;
                        string caminho = Environment.CurrentDirectory + @"\" + redimensionadas + @"\" + fileinfo.Name + "_" + DateTime.Now.Millisecond;
                        //move o arquivo para o diretorio finalizadas. redimensionadas
                        fileinfo.MoveTo(finalizadas);
                        

                        //aqui vai ler cada imagem



                    }




                    Thread.Sleep(new TimeSpan(0, 0, 15));
                }
            }
            catch (Exception erro)
            {
                Console.WriteLine("deu pau2");
            }
            
            
        }


        static void redimensionador(Image imagem, int altura, string caminho)
        {
            try
            {
                double ratio = (double)altura / imagem.Height;
                int novaLargura = (int)(imagem.Width * ratio);
                int novaAltura = (int)(imagem.Height * ratio);

                Bitmap novaImage = new Bitmap(novaLargura, novaAltura);

                File.Delete();

                using (Graphics g = Graphics.FromImage(novaImage))
                {
                    g.DrawImage(imagem, 0, 0, novaLargura, novaAltura);

                    imagem.Dispose();
                    novaImage.Save(caminho);
                    
                }
            }
            
                
            catch (Exception erro)
            {
                Console.WriteLine("deu pau3");
            }
            
        
        }
    }
}










