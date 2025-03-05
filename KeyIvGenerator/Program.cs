using Infrastructure.Services;

var service = new SecurityService();

var keyIv = service.GenerateKeyAndIV();

Console.WriteLine($"Key: {keyIv.Key}");
Console.WriteLine($"IV: {keyIv.Iv}");
Console.WriteLine("Press any key to exit...");
Console.ReadLine();