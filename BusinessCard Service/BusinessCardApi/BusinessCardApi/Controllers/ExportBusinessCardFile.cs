using BusinessCardApi.Enum;
using BusinessCardApi.Model;
using BusinessCardApi.Service.ExportBusinessCardFileService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace BusinessCardApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportBusinessCardFile : ControllerBase
    {
        private readonly IExportFileService _exportFileService;
        public ExportBusinessCardFile(IExportFileService exportFileService)
        {
            _exportFileService = exportFileService;
        }

        [HttpPost("convert")]
        public IActionResult ConvertToFormat([FromBody] BusinessCard businessCard, [FromQuery] string format)
        {
            if (businessCard == null || string.IsNullOrEmpty(format))
            {
                return BadRequest("Invalid input");
            }

            if (format.ToLower() == TYPEFILE.csv.ToString())
            {
                var csvData = _exportFileService.ConvertToCsv(businessCard);
                var fileName = "business_card.csv";
                return File(Encoding.UTF8.GetBytes(csvData), "text/csv", fileName);
            }
            else if (format.ToLower() == TYPEFILE.xml.ToString())
            {
                var xmlData = _exportFileService.ConvertToXml(businessCard);
                var fileName = "business_card.xml";
                return File(Encoding.UTF8.GetBytes(xmlData), "application/xml", fileName);
            }

            return BadRequest("Unsupported format. Use 'csv' or 'xml'.");
        }
    }
}
