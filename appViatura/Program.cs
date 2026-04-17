var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();



public class Viatura
{
    public int Id { get; set; }
    public string Prefixo { get; set; } // Ex: "RP-01"
    public string Modelo { get; set; }  // Ex: "Chevrolet Onix"
    public bool EmServico { get; set; }
}

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/viaturas")] // Rota base: localhost:xxxx/api/viaturas
public class ViaturasController : ControllerBase
{
    // Simulação de banco de dados estático
    private static List<Viatura> _frota = new()
    {
        new Viatura { Id = 1, Prefixo = "FORCA-01", Modelo = "Onix", EmServico = true }
    };

    [HttpGet] // GET: api/viaturas
    public IActionResult ListarFrota()
    {
        return Ok(_frota);
    }

    [HttpPost] // POST: api/viaturas
    public IActionResult AdicionarViatura(Viatura novaViatura)
    {
        _frota.Add(novaViatura);
        return CreatedAtAction(nameof(ListarFrota), novaViatura);
    }
}
