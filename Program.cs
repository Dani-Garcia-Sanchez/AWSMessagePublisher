using System;
using Amazon.SQS;
using Amazon.SQS.Model;
using Amazon.Runtime;
internal class Program
{
    
    static void Main(string[] args)
    {
        var accessKeyId = "";
        var secretAccessKey = "";
        var sessionToken = "";
        var credentials = new SessionAWSCredentials(accessKeyId, secretAccessKey, sessionToken);
        var sqsClient = new AmazonSQSClient(credentials, Amazon.RegionEndpoint.EUCentral1);
        var queueUrl = "";

        for (int i = 0; i < 30000; i++)
        {
            Console.WriteLine($"Iteration {i}");
            string message = $"{{\"Id\":\"{Guid.NewGuid()}\",\"Topic\":\"TruncateUserDataRequestChangeEvent\",\"Content\":{{\"Details\":{{\"Id\":\"{Guid.NewGuid()}\",\"UserId\":\"{Guid.NewGuid()}\",\"AccountId\":\"{Guid.NewGuid()}\",\"PrincipalId\":\"auth0|4587df320764d32b51883a5925bf494100fb3b3ccd49b36a\",\"ProviderType\":null,\"Platform\":null}},\"Namespace\":\"v1/OrgService/DataTruncation/User\",\"Type\":\"Created\",\"Timestamp\":638646081926874303,\"Correlation\":null,\"BootstrapId\":\"{Guid.NewGuid()}\",\"Platform\":\"stdbchmark\",\"ProductCode\":\"sbc_orghub\"}},\"PublishedAt\":null,\"Correlation\":{{\"Trace\":\"829eaab66f1bcb4bf5ff39c5883bfe5c\",\"Request\":\"00-829eaab66f1bcb4bf5ff39c5883bfe5c-2f7d8cbc2fc90754-00\",\"Span\":\"2f7d8cbc2fc90754\",\"Session\":\"00-829eaab66f1bcb4bf5ff39c5883bfe5c-2f7d8cbc2fc90754-00\",\"SourceId\":\"7fb50ce6-db85-4058-9182-6233f4747e71\"}},\"RetryAttempts\":0,\"ActiveEcosystemServiceName\":6,\"Headers\":{{\"Platform\":\"stdbchmark\", \"Product\":\"sbc_orghub\"}},\"MessageId\":\"{Guid.NewGuid()}\",\"CorrelationId\":\"{Guid.NewGuid()}\",\"RabbitMqTopology\":1}}";
            SendMessage(sqsClient, queueUrl, message).Wait();
        }
        
    }

    static async Task SendMessage(IAmazonSQS sqsClient, string queueUrl, string messageBody)
    {
        try
        {
            var sendMessageRequest = new SendMessageRequest(queueUrl, messageBody);

            var response = await sqsClient.SendMessageAsync(sendMessageRequest);

            Console.WriteLine($"Mensaje enviado correctamente. ID del mensaje: {response.MessageId}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al enviar el mensaje: {ex.Message}");
        }
    }
}