namespace back_dotnet;


using System.Diagnostics;

public class CustomActivitySource {
    public static readonly string Name = "Demo.CustomActivitySource";

    public static readonly ActivitySource source = new ActivitySource(Name);

    
}