using HW_3_Console;
using System.Text;
using System;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Channels;

var client = new HttpClient();
client.BaseAddress = new Uri("https://localhost:7234/");
//var res = await client.GetStringAsync("http://fd");

while (true) {
    Console.Clear();
    Console.WriteLine("Показать данные клиента: 1;   Созадть рандомного клиента: 2;");
    Console.Write("->");
    var command = Console.ReadKey().KeyChar;
    switch (command) {
        case '1':
            Console.Clear();
            var id = GetInt64("castomer id: ");
            await ShowClientDataAsync(id);
            Task.WaitAll();
            Console.WriteLine("кликните любую кнопку");
            Console.ReadKey();
            break;
        case '2':
            Console.Clear();
            await CreateCustomerAsyn();
            Task.WaitAll();
            Console.WriteLine("кликните любую кнопку");
            Console.ReadKey();
            break;
        default:
            Console.Clear();
            break;
    }
}

async Task CreateCustomerAsyn() {
    try {
        Random rnd = new Random();
        var customer = new Customer() {
            Firstname = rnd.Next().ToString(),
            Lastname = rnd.Next().ToString(),
        };
        var json = JsonConvert.SerializeObject(customer);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/customers", data);
        var result = await response.Content.ReadAsStringAsync();
        await Console.Out.WriteLineAsync("Был создан клиент:");
        await ShowClientDataAsync(Int64.Parse(result));
    } catch (Exception e) { await Console.Out.WriteLineAsync(e.Message); }
}
async Task ShowClientDataAsync(long id) {
    try {
        var response = await client.GetAsync($"/customers/{id}");
        if(response.StatusCode != HttpStatusCode.OK) {
            await Console.Out.WriteLineAsync(response.StatusCode.ToString());
            return;
        }
        var res = await response.Content.ReadAsStringAsync();
        await Console.Out.WriteLineAsync(res);
        await Console.Out.WriteLineAsync();
        await Console.Out.WriteLineAsync("готово!");
    } catch (Exception e) { await Console.Out.WriteLineAsync(e.Message); }
}

long GetInt64(string message) {
    string res;
    while (true) {
        Console.Write(message);
        res = Console.ReadLine();
        if (Int64.TryParse(res, out long x))
            break;
        Console.WriteLine("введите число!!!");
    }
    return Int64.Parse(res);
}