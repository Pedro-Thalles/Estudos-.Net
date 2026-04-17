using Microsoft.VisualBasic;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<Inotificador, NotificadorSMS>();
builder.Services.AddScoped<OcorrenciaService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/emergencia/{crime}", (OcorrenciaService ocorrencia, string crime) => {

    ocorrencia.ProcessarOcorrencia(crime);
    return Results.Ok($"Ocorrencia de {crime} processada e alerta enviado!");

    
});


app.Run();

public interface Inotificador
{
    void EnviarAlerta(string mensagem);
}

public class NotificadorSMS : Inotificador
{
    public void EnviarAlerta(string mensagem)
    {
        Console.WriteLine($"[SMS] Alerta Enviado: {mensagem}");
    }
}

public class NotificadorWPP : Inotificador
{
    public void EnviarAlerta(string mensagem)
    {
        Console.WriteLine($"[WhatsApp] Alerta Enviado: {mensagem}");
    }
}


public class  OcorrenciaService 
{
    private readonly Inotificador _notificador;

    public OcorrenciaService(Inotificador notificador)
    {
        _notificador = notificador;
    }


    public void ProcessarOcorrencia(string crime)
    {

        Console.WriteLine($"Registrando Crime: {crime}");
        _notificador.EnviarAlerta($"Viatura despachada para: {crime}");

    }

}


