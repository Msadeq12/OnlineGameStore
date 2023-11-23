using Microsoft.EntityFrameworkCore;
using PROG3050_HMJJ.Models.DataAccess;
using PROG3050_HMJJ.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using PROG3050_HMJJ.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<GameStoreDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("GameStoreCNN")));

builder.Services.AddDefaultIdentity<User>(options => {
    //Register The Account and Validate the email
    options.SignIn.RequireConfirmedAccount = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    // The maximum number of failed access attempts before a user is locked out.
    options.Lockout.MaxFailedAccessAttempts = 3;
}).AddDefaultTokenProviders().AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<GameStoreDbContext>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IEmailSender>(provider =>
{
    return new EmailSender(
        smtpServer: "sandbox.smtp.mailtrap.io",
        smtpPort: 587,                  // Mailtrap thavraniharshal07@gmail.com 12e948e8dfd69f, 570529c4e4c0ed
        smtpUsername: "7b86f4686ecb5d", // Mail trap hthavrani3610@conestogac.on.ca 7b86f4686ecb5d, 0073ca91f744cb
        smtpPassword: "0073ca91f744cb"
    );
});
builder.Services.AddRazorPages();



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
app.UseAuthentication();;

app.UseAuthorization();
app.MapRazorPages();

// adds a default admin
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    // Call the CreateAdminUser method to create the admin user.
    await GameStoreDbContext.CreateAdminUser(serviceProvider);

    // Call the CreateMemberUser method to create the member user.
    await GameStoreDbContext.DeleteTestMemberUser(serviceProvider);
}

// area route for admin panel
app.MapAreaControllerRoute(
    name: "admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

// area route for Member panel
app.MapAreaControllerRoute(
    name: "member",
    areaName: "Member",
    pattern: "Member/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
