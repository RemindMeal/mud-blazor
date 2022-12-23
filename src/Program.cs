using RemindMeal.Data;
using MudBlazor.Services;
using Microsoft.EntityFrameworkCore;
using RemindMeal.Services;
using RemindMeal.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

builder.Services.AddDbContextFactory<RemindMealDbContext>(opt =>
    {
        opt.UseSqlite(builder.Configuration.GetConnectionString("db"));
        opt.EnableDetailedErrors();
    });

builder.Services.AddScoped<IAsyncRepository<Recipe>, RecipeRepository>();
builder.Services.AddScoped<IAsyncRepository<Category>, CategoryRepository>();
builder.Services.AddScoped<IAsyncRepository<Friend>, FriendRepository>();
builder.Services.AddScoped<IAsyncRepository<Meal>, MealRepository>();
// builder.Services.AddTransient<IAsyncRepository<Ingredient>, IngredientRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();