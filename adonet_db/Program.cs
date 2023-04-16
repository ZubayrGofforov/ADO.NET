using adonet_db.Interfaces.Repositories;
using adonet_db.Models;
using adonet_db.Repositories;

class Program
{
    public static async Task Main()
    {
        //    Word word = new Word()
        //    {
        //        Word_ = "Book",
        //        Translate = "kitob",
        //        Level = Level.Hard,
        //        Count = 1
        //    };

        //    IWordRepository wordRepository = new WordRepository();
        //    var res = await wordRepository.CreateAsync(word);

        //    Console.WriteLine(res);

        User user = new User()
        {
            FullName = "Normadjon G''offorov",
            Username = "normadjon2003",
            Password = "1234",
            Address = "Qo''qon"
        };

        IUserRepository userRepository = new UserRepository();
        var users = await userRepository.GetAllAsync(0, 3);
        while (true)
            Console.WriteLine(users);

    }
}
