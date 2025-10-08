using Microsoft.Identity.Client;
using MiniAdmin.Entity;


namespace MiniAdmin.Apps;


[App(icon: Icons.PartyPopper, title: "AdminPanels")]
public class Mainpage : ViewBase
{
    public override object? Build()
    {
        var customers = new[]
        {
            new Customer { CustomerId = "C001", FirstName = "Ahmet", LastName = "Yılmaz", Email = "ysf.ckrl.02@outlook.com", PhoneNumber = "555-1234", Address = "İstanbul" },
            new Customer { CustomerId = "C002", FirstName = "Mehmet", LastName = "Demir", Email = ""  , PhoneNumber = "555-5678", Address = "Ankara" },
            new Customer { CustomerId = "C003", FirstName = "Ayşe", LastName = "Kara", Email = "", PhoneNumber = "555-8765", Address = "İzmir" },
            new Customer { CustomerId = "C004", FirstName = "Fatma", LastName = "Çelik", Email = "", PhoneNumber = "555-4321", Address = "Bursa" },
            new Customer { CustomerId = "C005", FirstName = "Ali", LastName = "Şahin", Email = "", PhoneNumber = "555-6789", Address = "Antalya" }

        };
        var orders = new[]
        {
            new order { OrderId = "O001", CustomerId = "C001", OrderDate = DateTime.Now.AddDays(-10), TotalAmount = 150.75m, Status = "Completed" },
            new order { OrderId = "O002", CustomerId = "C002", OrderDate = DateTime.Now.AddDays(-5), TotalAmount = 200.00m, Status = "Pending" },
            new order { OrderId = "O003", CustomerId = "C003", OrderDate = DateTime.Now.AddDays(-2), TotalAmount = 75.50m, Status = "Shipped" },
            new order { OrderId = "O004", CustomerId = "C004", OrderDate = DateTime.Now.AddDays(-1), TotalAmount = 300.20m, Status = "Completed" },
            new order { OrderId = "O005", CustomerId = "C005", OrderDate = DateTime.Now, TotalAmount = 120.00m, Status = "Processing" }
        };

             var Sku = UseState("");
    var Name = UseState("");
    var desc = UseState("");
    var Price = UseState("");
    var Url = UseState("");
    var Stock = UseState("");
    var products = new[]
        {
            new Product { Sku = "001", Name = "Elma", desc = "Taze kırmızı elma", Price = 3.5m, Url = "https://i.hizliresim.com/tmaf5ym.jpeg", Stock = 100 },
            new Product { Sku = "002", Name = "Armut", desc = "Lezzetli yeşil armut", Price = 4.0m, Url = "https://i.hizliresim.com/tmaf5ym.jpeg", Stock = 80 },
            new Product { Sku = "003", Name = "Muz", desc = "Olgun sarı muz", Price = 2.5m, Url = "https://i.hizliresim.com/tmaf5ym.jpeg", Stock = 150 },
            new Product { Sku = "004", Name = "Çilek", desc = "Tatlı kırmızı çilek", Price = 6.0m, Url = "https://i.hizliresim.com/tmaf5ym.jpeg", Stock = 60 },
            new Product { Sku = "005", Name = "Karpuz", desc = "Büyük ve sulu karpuz", Price = 10.0m, Url = "https://i.hizliresim.com/tmaf5ym.jpeg", Stock = 30 }
        };
        var password = UseState("");
        var showPanel = UseState(false);
        var client = UseService<IClientProvider>();
        var selectedItem = UseState("");

        var card1 = new Card(
            Layout.Vertical().Gap(2)
                | new Image("https://i.hizliresim.com/tmaf5ym.jpeg")
                | new Button("Sign Me Up", _ => client.Toast("You have signed up!"))
        )
        .Title("Card App")
        .Description("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam nec purus nec nunc")
        .Width(50)
        .Height(100);

        MenuItem[] menuItems = new[]
        {
            MenuItem.Default("Stok İşlemleri")
                .Icon(Icons.Play).Children(
                    MenuItem.Default("Ürün Listesi").Icon(Icons.Store).Tag("stock"),
                    MenuItem.Default("Hızlı Ürün Girişi").Icon(Icons.Zap).Tag("quickstock"),
                    MenuItem.Default("Ürün Raporları").Icon(Icons.Database).Tag("stock-report")
                ),
            MenuItem.Default("Müşteriler İşlemleri")
                .Icon(Icons.User).Children(
                    MenuItem.Default("Müşteri Listesi").Icon(Icons.User).Tag("customer"),
                    MenuItem.Default("Siparişler").Icon(Icons.FileText).Tag("forms"),
                    MenuItem.Default("Ödeme kayıtlar").Icon(Icons.CreditCard).Tag("payment-report"),
                    MenuItem.Default("Sipariş Raporları").Icon(Icons.Table).Tag("order-report")
                ),
            MenuItem.Default("Gelişmiş")
                .Icon(Icons.Cpu).Children(
                    MenuItem.Default("Seo Optimasyonu Ai Destekli").Icon(Icons.Link).Tag("Seo-Ai"),
                    MenuItem.Default("Fatura Entegrasyonu").Icon(Icons.Inbox).Tag("invoice"),
                    MenuItem.Default("Destek").Icon(Icons.HandHelping).Tag("help")
                )
        };

        var menu = new SidebarMenu(
            onSelect: evt =>
            {
                selectedItem.Value = evt.Value?.ToString() ?? "";
                client.Toast($"Selected: {evt.Value}");
            },
            items: menuItems
        );

        object GetMainContent()
        {
            switch (selectedItem.Value)
            {
                case "stock":
                    return new Card(
                        Layout.Vertical().Gap(3)
                            | Text.Large("Ürün Listesi")
                            | products.ToTable()
                            .Width(Size.Full())
                    ).Title("Stok Yönetimi");

                case "quickstock":
                    return Layout.Vertical().Gap(3)
                            | Text.Block("Sku").Width(Size.Fraction(0.15f))
                            | Sku.ToTextInput().Placeholder("SKU")
                    | Text.Block("Name").Width(Size.Fraction(0.15f))
                            | Name.ToTextInput().Placeholder("Name")
                            | Text.Block("desc").Width(Size.Fraction(0.15f))
                            | desc.ToTextInput().Placeholder("desc")
                            | Text.Block("Price").Width(Size.Fraction(0.15f))
                            | Price.ToTextInput().Placeholder("Price")
                            | Text.Block("Url").Width(Size.Fraction(0.15f))
                            | Url.ToTextInput().Placeholder("Url")
                            | Text.Block("Stock").Width(Size.Fraction(0.15f))
                            | Stock.ToTextInput().Placeholder("Stock")
                            | new Button("Ürün Ekle", _ => client.Toast($"Ürün eklendi: {Name.Value }"));

                case "stock-report":
                    return new Card(
                        Layout.Vertical().Gap(3)
                            | Text.Large("Ürün Raporları")
                            | products.ToTable().Width(Size.Full())
                            ).Title("ürün Raporları");

                case "customer":
                    return new Card(
                        Layout.Vertical().Gap(3)
                            | Text.Large("Müşteri Listesi")
                            | customers.ToTable()
                            .Width(Size.Full())
                            )
                            .Title("Müşteri bilgilerini görüntüleyin ve yönetin.");

                case "forms":
                    return new Card(
                        Layout.Vertical().Gap(3)
                            | Text.Large("Siparişler")
                            | orders.ToTable()
                            .Width(Size.Full())
                            ).Title("Sipariş Yönetimi");

                case "payment-report":
                    return new Card(
                        Layout.Vertical().Gap(3)
                            | Text.Large("Ödeme Kayıtları")
                            | orders.ToTable()
                            .Width(Size.Full())
                            ).Title("Sipariş Yönetimi");

                case "order-report":
                    return new Card(
                        Layout.Vertical().Gap(3)
                            | Text.Large("Sipariş Raporları")
                            | orders.ToTable()
                            .Width(Size.Full())
                            ).Title("Sipariş Yönetimi");


                case "Seo-Ai":
                    return new Card(
                        Layout.Vertical().Gap(3)
                            | Text.Large("Seo Optimizasyonu Ai Destekli")
                            | new Button("Analiz Başlat", _ => client.Toast("AI SEO analizi başlatıldı"))
                    ).Title("Gelişmiş Araçlar");

                default:
                    return new Card(
                        Layout.Vertical().Gap(4)
                            | Text.Large("Welcome to Mini-AdminPanel!")
                            | Text.Large("💡 Sadece C# ile Arayüz Geliştirmek Mümkün mü? Evet! 🚀\r\n\r\nBugün tamamen C# kullanarak bir kullanıcı arayüzü tasarladım —\r\nhiçbir HTML, CSS veya JavaScript kodu yazmadan 👀\r\n\r\nHer bileşen (Card, Button, TextInput, SidebarLayout, Image) doğrudan C# kodu içinde tanımlanıyor.\r\nYani klasik div, class, style etiketleri yok.\r\nTüm UI, code-first bir yaklaşımla sadece C# kodundan oluşuyor.\r\n\r\n💻 Framework: Ivy Interactive (C# UI Framework)\r\n\U0001f9e9 Yaklaşım: Tamamen Component-Based (Code-Only)\r\n📱 Sonuç: Responsive, temiz ve modüler bir arayüz\r\n\r\nBu yapı sayesinde:\r\n✅ HTML/CSS karmaşası olmadan saf C# ile UI geliştirilebiliyor\r\n✅ Kod okunabilirliği artıyor\r\n✅ Görsel düzen bileşenler üzerinden yönetiliyor\r\n\r\n✨ Sadece C# ile modern, responsive bir arayüz oluşturmak —\r\n“frontend yazmadan frontend yapmak” gibi hissettiriyor 😄\r\n\r\n#CSharp #Dotnet #IvyFramework #CodeFirst #CleanCode #SoftwareDevelopment #UIUX #DeveloperJourney #NoHTML #NoCSS")
                    | Text.P("Sol menüden bir seçenek belirleyin."));
                            
            }
        }
        
   

        return new SidebarLayout(
            mainContent: GetMainContent(),
            sidebarContent: menu,
            sidebarHeader: Text.Lead("Kategoriler")
        );
    }
}
