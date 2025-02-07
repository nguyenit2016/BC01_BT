using Microsoft.AspNetCore.SignalR;

public class RoomHubs : Hub
{
    RoomServices _roomServices;
    ProductServices _productServices;
    public RoomHubs(RoomServices roomServices, ProductServices productServices){
        _roomServices = roomServices;
        _productServices = productServices;
    }

    public override async Task OnConnectedAsync(){
        Console.WriteLine($"Connected clientId: {Context.ConnectionId}");
        await base.OnConnectedAsync();

        // sau khi kết nối thì gửi dữ liệu cho tất cả các client ds room
        await Clients.All.SendAsync("GetAllRoom", RoomServices.lstRoom);

        //Product
        await _productServices.GetProducts();
        await Clients.All.SendAsync("GetProducts", _productServices.products);
    }

    public async Task AddRoom(){
        RoomVM rom = new RoomVM();
        RoomServices.lstRoom.Add(rom);
        // broadcast
        await Clients.All.SendAsync("GetAllRoom", RoomServices.lstRoom);
    }

    public async Task DeleteProduct(double id){
        Console.WriteLine("Product id: ", id);
        await _productServices.DeleteProduct(id);
        await _productServices.GetProducts();
        await Clients.All.SendAsync("GetProducts", _productServices.products);
    }

    public override async Task OnDisconnectedAsync(Exception? ex){
        Console.WriteLine($"Dis connected clientId: {Context.ConnectionId}");
        await base.OnDisconnectedAsync(ex);
    }
}