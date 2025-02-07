
public class RoomServices
{
    public static List<RoomVM> lstRoom = new List<RoomVM>();

    static RoomServices()
    {
        for (int i = 0; i < 10; i++)
        {
            RoomVM room = new RoomVM();
            lstRoom.Add(room);
        }
    }

    public void AddRoom()
    {
        RoomVM room = new RoomVM();
        lstRoom.Add(room);
    }

    public void DeleteRoom(string roomId)
    {
        lstRoom = lstRoom.Where(r => r.roomId == roomId).ToList();
    }
}