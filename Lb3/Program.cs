using Lb3.Services;

var builder = WebApplication.CreateBuilder(args);

// Визначаємо шляхи
string modelPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "MLModels", "CarEvaluationModel.zip");
string dataPath = Path.Combine(Directory.GetCurrentDirectory(), "DataSet", "car.data");

// Реєструємо сервіс
builder.Services.AddSingleton(sp => new CarEvaluationService(modelPath, dataPath));
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
