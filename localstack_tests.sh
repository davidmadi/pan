awslocal s3api create-bucket --bucket my-bucket --region us-east-1
awslocal s3api list-buckets
awslocal s3api put-object --bucket my-bucket --key favicon.ico --body ./ang/src/favicon.ico
awslocal s3api list-objects --bucket my-bucket
awslocal s3 presign s3://my-bucket/favicon.ico
awslocal s3 presign s3://my-bucket/theoffice.jpeg