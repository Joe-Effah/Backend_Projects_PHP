using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoToMp3.Mp3ConvertorService;
namespace VideoToMp3.Pages;

public class IndexModel : PageModel
{
    public string? convertedDownloadPath {get; set;}

    public bool fileUploadStatus;

    public bool converted;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;

    }
    

  
    public void OnPost(IFormFile? fileData)
    {
        
        string fileUserPath = $@"./Upload/User-{Random.Shared.Next()}";
        
        Directory.CreateDirectory(fileUserPath);

        string fileInformation = $@"{fileUserPath}/Upload-{Random.Shared.Next()}{Path.GetExtension(fileData!.FileName)}";
        
        
        try
        {
            using (var data = System.IO.File.Create(fileInformation))
            {
                fileData.CopyTo(data);
            }

            fileUploadStatus = true;

        }
        catch (System.Exception Message)
        {

            System.Console.WriteLine(Message.Message);
        }

        string ConvertedFilePath = $"{fileUserPath}/Converted-{Random.Shared.Next()}.mp3";

        Convertor convertor = new Convertor();

        bool State = convertor.ExtractAudio(fileInformation,ConvertedFilePath);

        if (State)
        {
            converted = true;
            convertedDownloadPath = ConvertedFilePath;
            
          
        }
        else
        {
            System.Console.WriteLine("Convertion Failed ");
        }


    }

    //Download Method For Audio File
    public IActionResult OnGetAudioDownload(string path)
    {

      try
        {
            
        System.Console.WriteLine(path);
            var streamData = System.IO.File.ReadAllBytes(path);
        
            return File(streamData,"audio/mp3");
        
                       

        }
        catch (System.Exception e)
        {
           System.Console.WriteLine(e.Message); 

             
            return NotFound();
        }
           
    }



}
