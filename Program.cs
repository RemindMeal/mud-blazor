using MudBlazorTest.Data;
using MudBlazor.Services;
using Microsoft.EntityFrameworkCore;
using MudBlazorTest.Services;
using MudBlazorTest.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddDbContext<MudBlazorTestDbContext>(opt =>
    {
        opt.UseSqlite(builder.Configuration.GetConnectionString("db"));
        opt.EnableDetailedErrors();
    },
    ServiceLifetime.Transient);

builder.Services.AddTransient<IAsyncRepository<Recipe>, RecipeRepository>();
builder.Services.AddTransient<IAsyncRepository<Category>, CategoryRepository>();
builder.Services.AddTransient<IAsyncRepository<Friend>, FriendRepository>();
// builder.Services.AddTransient<IAsyncRepository<Ingredient>, IngredientRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();