namespace APICatalogo.Logging;

public class CustomLoggerProviderConfiguration
{
    public LogLevel LogLevel { get; set; } = LogLevel.Warning; //Nível min de log a ser registrado 
    public int EnventId { get; set; } = 0; //Define o ID do evento de log
}

//Classe que define a config do provedor de Log personalizado