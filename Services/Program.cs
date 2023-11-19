using Services;

Console.WriteLine("Starting REST Apis - for library system");
var srv = new LibraryService();
srv.StartUp();

Console.WriteLine("Stopping REST Apis - for librry system");

Console.ReadLine();

