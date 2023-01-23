// See https://aka.ms/new-console-template for more information
using JsonReceiptToReceipt;
using JsonReceiptToReceipt.Model;
using Newtonsoft.Json;

using (StreamReader r = new StreamReader(Directory.GetCurrentDirectory() + @"/Resources/response.json"))
{
    string json = r.ReadToEnd();
    var receipts = JsonConvert.DeserializeObject<List<PartOfReceipt>>(json);
    var parts = JsonReceiptConverter.Convert(receipts);
    var lines = JsonReceiptConverter.ReceiptBeautifiler(parts);
    await File.WriteAllLinesAsync(@"..\..\..\Output\receipt.txt", lines);
}