using Amazon.S3;
using Amazon.S3.Model;


namespace Library.Entity.S3Bucket
{
  public class S3File {
    public string Url { get; set; }

    public static S3File Upload(IFormFile file) {
      var ret = new S3File();
      var bucketName = "my-bucket";
      var credentials = new Amazon.Runtime.BasicAWSCredentials("empty", "empty");
      var extension = file.FileName.Split(".")[1];
      var key = $"{Guid.NewGuid()}.{extension}";
      var config = new AmazonS3Config
      {
        RegionEndpoint = Amazon.RegionEndpoint.USEast1, // e.g., Amazon.RegionEndpoint.USWest2
        UseHttp = true,
        ServiceURL = "http://localhost:4566",
        ForcePathStyle = true
      };

      using (var client = new Amazon.S3.AmazonS3Client(credentials, config))
      {
        using (var newMemoryStream = new MemoryStream())
        {
          // 1. Put object-specify only key name for the new object.
          var putRequest1 = new PutObjectRequest
          {
              BucketName = bucketName,
              Key = key,
              InputStream = newMemoryStream
          };

          file.CopyTo(newMemoryStream);
          var res = client.PutObjectAsync(putRequest1);
          res.Wait();
        }

        GetPreSignedUrlRequest preSignedUrlRequest = new GetPreSignedUrlRequest
        {
          BucketName = bucketName,
          Key = key,
          Expires = DateTime.UtcNow.AddHours(1),
        };

        ret.Url = client.GetPreSignedURL(preSignedUrlRequest);
      }
      return ret;
    }
  }

}