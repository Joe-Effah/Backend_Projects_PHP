using Microsoft.AspNetCore.Mvc;


using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoToMp3.Mp3ConvertorService;

namespace VideoToMp3.Pages;

public class VideoModel : PageModel
{
    // private string? James;

    public bool Converted { get;  set; }
    public string? ConvertedDownloadPath { get;  set; }

    public void OnPost(IFormFile fileData, string fileFormat)
    {
              // Directory.CreateDirectory()
        string fileUserPath = $@"./Upload/User-{Random.Shared.Next()}";
        Directory.CreateDirectory(fileUserPath);

        string fileInformation = $@"{fileUserPath}/Upload-{Random.Shared.Next()}{Path.GetExtension(fileData!.FileName)}";
        // System.Console.WriteLine(dat.Length);
        //  System.Console.WriteLine(fileInformation);
        // James = "File Was Uploaded";
        try
        {
            using (var data = System.IO.File.Create(fileInformation))
            {
                fileData.CopyTo(data);
            }
        }
        catch (System.Exception Message)
        {

            System.Console.WriteLine(Message.Message);
        }


    }

    public ActionResult OnGetVideoDownload(string path)
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