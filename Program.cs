using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);

///the application is storing keys in a directory that might be lost when the container is stopped or removed. 
///This could be an issue if you need persistent encryption keys.
//*Important*
///In a containerized production environment, HTTPS is often terminated at a reverse proxy (e.g., Nginx or a load balancer).
///Ensure that production environment is correctly configured for HTTPS if needed.
///*Important*
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo("/app/DataProtection-Keys"));

// Add services to the container.
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
