using Microsoft.AspNetCore.Mvc;
namespace PRIORI_SERVICES_API.Controllers;
using System.Text.Json;

[ApiController]
[Route("api/[controller]")]
public class MarkdownController : ControllerBase
{
    private readonly ILogger<MarkdownController> _logger;

    private readonly String MarkdownFolder = "./Assets/Markdown";

    public MarkdownController(ILogger<MarkdownController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetMarkdownFile")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Get(int? id = null)
    {
        if (id == null) {
            string[] rootdir_filepath = Directory.GetFiles($"{MarkdownFolder}", "*.json");
            foreach (string file in rootdir_filepath) {
                
            }

            return Ok(
                
            );
        }

        try {
            Model.MarkdownMetadata? Metadata 
                = JsonSerializer.Deserialize<Model.MarkdownMetadata>
                    (System.IO.File.ReadAllText($"{MarkdownFolder}/{id}.json"));
            
            if (Metadata == null) {
                throw new FileNotFoundException();
            }
            
            Metadata.Conteudo = System.IO.File.ReadAllText($"{MarkdownFolder}/{id}.md");
            return Ok(Metadata);
        } catch (FileNotFoundException) {
            return NotFound();
        } catch (IOException) {
            return NotFound();
        }
    }

    [HttpPost(Name = "PostMarkdownFile")]
    public string Post() {
        return "";
    }
}
