namespace KFC.UseCases.Interactor;

public class FilePathGenerator
{
    public static string GetFilePath(string baseDirectory, string ext)
    {
        DateTime localDate = DateTime.Now;

        var fullPath = Path.Combine(baseDirectory, String.Format("{0:yyyy}_{0:MM}_{0:dd}/{1}.{2}", localDate, Guid.NewGuid().ToString(), ext));

        return fullPath;
    }

}