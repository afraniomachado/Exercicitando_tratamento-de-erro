public class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Exercitando sobre tratamento de erro com a classe HttpClient");
        try
        {
            Console.WriteLine("Acessando o arquivo poesia.txt em https://macoratti.net/dados");
            Console.WriteLine("Informe o nome do arquivo: ");
            string? arquivo = Console.ReadLine();
            Console.WriteLine("Informe a URL do site: ");
            string? url = Console.ReadLine();
            Console.WriteLine("\nAguarde..");

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(url + "/" + arquivo).Result;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Acesso do arquivo feito com sucesso");
                Console.WriteLine("Código do status: " + response.StatusCode);
            }
            else
            {
                throw new HttpRequestException("Erro: " + (int)response.StatusCode);
            }
        }

        catch (HttpRequestException ex) when (ex.Message.Contains("400"))
        {
            Console.WriteLine("Requisição inválida");
            Console.WriteLine(ex.Message);
        }
        catch (HttpRequestException ex) when (ex.Message.Contains("401"))
        {
            Console.WriteLine("Acesso não autorizado");
            Console.WriteLine(ex.Message);
        }
        catch (HttpRequestException ex) when (ex.Message.Contains("404"))
        {
            Console.WriteLine("Página não encontrada");
            Console.WriteLine(ex.Message);
        }

        catch (HttpRequestException ex) when (ex.Message.Contains("500"))
        {
            Console.WriteLine("Erro interno do servidor");
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            
            Console.WriteLine(ex.Message);
        }
        finally
        {
            Console.WriteLine("Processamento Concluído");
        }
        Console.ReadKey();
    }
}