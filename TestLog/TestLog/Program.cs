using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DZ_1
{


    public interface IExceptionLogger
    {
        void LogException(Exception ex);
        void Log(string message);
    }
    public class Program
    {
        public static void LogError(Exception ex)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ErrorXML));
            ErrorXML error = new ErrorXML();
            error.Time = DateTime.Now;
            error.Type = ex.GetType().Name;
            error.Message = ex.Message;
            error.Stack = ex.StackTrace;
            using (StreamWriter stream = new StreamWriter("log.xml"))
            {
                serializer.Serialize(stream, error);
            }
        }
        public class Drav0 : Exception, IExceptionLogger
        {
            public Drav0(string message) : base(message)
            {
            }

            public void Log(string message)
            {
                Console.WriteLine(message);
            }

            public void LogException(Exception ex)
            {
                this.Log($"ошибка: дискриминант равен нулю");
                LogError(ex);
            }
        }

        public class Dmin0 : Exception, IExceptionLogger
        {
            public Dmin0(string message) : base(message)
            {
            }

            public void Log(string message)
            {
                Console.WriteLine(message);
            }

            public void LogException(Exception ex)
            {
                this.Log($"ошибка: дискриминант меньше нуля");
                LogError(ex);
            }
        }
        public class ErrorXML
        {
            public DateTime Time { get; set; }
            public string Type { get; set; }
            public string Message { get; set; }
            public string Stack { get; set; }
        }
        static void Main(string[] args)
        {

            IExceptionLogger dmin0Logger = new Dmin0("ошибка: дискриминант меньше нуля");
            IExceptionLogger drav0Logger = new Drav0("ошибка: дискриминант равен нулю");

            try
            {
                using (TextReader reader = File.OpenText("test.txt"))
                {
                    string text = reader.ReadLine();
                    string[] bits = text.Split(' ');
                    try
                    {
                        try
                        {
                            int a1 = int.Parse(bits[0]);
                            int b1 = int.Parse(bits[1]);
                            int c1 = int.Parse(bits[2]);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Неверные данные");
                            LogAsync(ex.Message);
                            LogError(ex);
                        }
                        int a = int.Parse(bits[0]);
                        int b = int.Parse(bits[1]);
                        int c = int.Parse(bits[2]);
                        double D = b * b - 4 * a * c;
                        Console.WriteLine($"D={b}*{b}-4*{a}*{c}");
                        Console.WriteLine("Дискриминант = " + D);
                        if (D < 0)
                        {
                            dmin0Logger.LogException(new Exception("ошибка: дискриминант меньше нуля"));
                            throw new Dmin0("ошибка: дискриминант меньше нуля");
                        }
                        if (D == 0)
                        {
                            drav0Logger.LogException(new Exception("ошибка: дискриминант равен нулю"));
                            throw new Drav0("ошибка: дискриминант равен нулю");
                        }
                    }
                    catch (Exception ex)
                    {
                        LogAsync(ex.Message);
                        LogError(ex);
                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Файл отсутствует");
                LogAsync(ex.Message);
                LogError(ex);
            }
            finally
            {
                Console.ReadLine();
            }

            void LogError(Exception ex)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ErrorXML));
                ErrorXML error = new ErrorXML();
                error.Time = DateTime.Now;
                error.Type = ex.GetType().Name;
                error.Message = ex.Message;
                error.Stack = ex.StackTrace;
                using (StreamWriter stream = new StreamWriter("log.xml"))
                {
                    serializer.Serialize(stream, error);
                }
            }


        }
        static async Task LogAsync(string message)
        {
            string path = "log.txt";
            string text = $"дата: {DateTime.Now} \t {message} \t версия приложения:{Assembly.GetExecutingAssembly().GetName().Version}\n";
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                await writer.WriteLineAsync(text);
            }
        }

    }
}