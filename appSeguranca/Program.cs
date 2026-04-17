var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
/*
// --- NOSSO MIDDLEWARE DE SEGURANCA ---
app.Use(async (context, next) =>
{
    // 1. Tenta pegar a "Chave-Secreta" que vem no cabecalho (Header) da requisisao
    var chavePublicada = context.Request.Headers["Chave-Secreta"].ToString();

    // 2. Verifica se a chave eh a correta
    if (chavePublicada == "ssp-sergipe-2026")
    {
        // Se estiver correta, chamamos o 'next' para ir para o proximo da fila
        await next();
    }
    else
    {
        // Se estiver errada ou vazia, barramos aqui mesmo!
        context.Response.StatusCode = 401; // Codigo HTTP para "Nao Autorizado"
        await context.Response.WriteAsync("Acesso Negado: Chave invalida ou ausente.");
    }
});*/

// Nossa rota final (so sera alcancada se passar pelo middleware acima)
app.MapGet("/", () => "Bem-vindo ao Banco de Dados da Seguranca Publica!");

var placasRoubadas = new List<string> { "ABC-1234", "XYZ-9999", "SSP-2026" };



app.MapGet("/verificar/{placa}", (string placa) => 
{
    if (placasRoubadas.Contains(placa.ToUpper()))
    {
        return $"ALERTA: O veículo {placa} consta como ROUBADO!";
    }
    return $"Veículo {placa} está regularizado.";
});


app.MapGet("/produtos", () => new { Nome = "Teclado", Preco = 150.00 });





// Simulação de Banco de Dados
var detentos = new List<string> { "João Silva", "Maria Oliveira" };

// 1. GET - Listar todos
app.MapGet("/detentos", () => detentos);

// 2. POST - Adicionar um novo (Recebe via Query String para facilitar seu teste)
app.MapPost("/detentos", (string nome) =>
{
    detentos.Add(nome);
    return Results.Created($"/detentos/{nome}", $"Detento {nome} registrado com sucesso.");
});

// 3. PUT - Substituir um nome existente por outro
app.MapPut("/detentos/{id}", (int id, string novoNome) =>
{
    if (id < 0 || id >= detentos.Count) return Results.NotFound("Detento não encontrado.");

    detentos[id] = novoNome;
    return Results.Ok($"Registro {id} atualizado para {novoNome}.");
});

// 4. DELETE - Remover um registro
app.MapDelete("/detentos/{id}", (int id) =>
{
    if (id < 0 || id >= detentos.Count) return Results.NotFound("Registro inexistente.");

    detentos.RemoveAt(id);
    return Results.Ok("Registro removido do sistema.");
});

app.Run();
