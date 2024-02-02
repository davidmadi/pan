using System;
using Library.Envelope;
using Library.Back.Calculator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Library.Entity.Access;
using Library.Entity.S3Bucket;

namespace BackApi.Controllers.v1;

[ApiController]
[Route("v1/")]

public class FileController : ControllerBase
{
    [HttpPost("upload/image")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<Response<S3File>> Upload(IFormFile file)
    {
        try
        {
            var result = S3File.Upload(file);
            return new Response<S3File>(){
                Success = true,
                Result = result
            };
        }
        catch (Exception e)
        {
            Library.Logging.LogManager.EnqueueException(e, null);
            return NotFound(new Response<IncomeTaxResult>()
            {
                Success = false,
                Message = e.Message
            });
        }
    }
    
}