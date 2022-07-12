

using Refit;

public interface IBackJavaApi {

    [Get("/long")]
    Task<string> SomeQuery();
}