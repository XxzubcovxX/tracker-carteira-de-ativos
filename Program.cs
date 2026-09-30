using Spectre.Console;
using trecker.API.api;
using trecker.API.api.Data;
using trecker.func;
using trecker.func.login;



using (var db = new AppDbContext())
{
    // Cria o arquivo trecker.db e as tabelas se ainda não existirem
    db.Database.EnsureCreated();
}

_ = Task.Run(() => Api.IniciarApi(args));

   
    Menu menu = new Menu();
    await menu.MostrarMenu();

