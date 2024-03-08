namespace apiSipinna.Utils;

public class Logger{
    bool isOn;

    public Logger(bool isOn){
        this.isOn = isOn;
    }

    public bool IsOn{
        get{return this.isOn;}
        set{this.isOn = value;}
    }

    public void log(string message){
        if(isOn){ // imprime mensajes si el log está encendido (implementación arcaica)
            Console.WriteLine(message);
        }else{
            // do nothing....
        }
    }

    public void MostrarExceptionInfo(Exception e){
        Console.WriteLine($"Ha ocurrido una excepción vvvv\nNombre: {e.GetType().Name}\nMensaje de la excepción: {e.Message}\nLocalización del error: {e.Source}");
    }
}